using System;
using System.Collections.Generic;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Services;

public class ProntuarioService
{
    private readonly IProntuarioRepository _prontuarioRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly ILogAcessoRepository _logAcessoRepository;

    public ProntuarioService(
        IProntuarioRepository prontuarioRepository,
        IPacienteRepository pacienteRepository,
        ILogAcessoRepository logAcessoRepository)
    {
        _prontuarioRepository = prontuarioRepository ?? throw new ArgumentNullException(nameof(prontuarioRepository));
        _pacienteRepository = pacienteRepository ?? throw new ArgumentNullException(nameof(pacienteRepository));
        _logAcessoRepository = logAcessoRepository ?? throw new ArgumentNullException(nameof(logAcessoRepository));
    }

    public SessaoProntuario RegistrarSessao(
        int id,
        int pacienteId,
        int agendamentoId,
        string anotacoesClinicas,
        int usuarioId)
    {
        // RN16: Valida se o paciente existe e está ativo
        var paciente = _pacienteRepository.ObterPorId(pacienteId);
        if (paciente == null || !paciente.Ativo)
            throw new InvalidOperationException("Paciente inválido ou inativo no sistema.");

        // RF15, RN05: Cria a sessão de prontuário vinculada ao paciente e consulta
        var sessao = new SessaoProntuario(id, pacienteId, agendamentoId, anotacoesClinicas);
        _prontuarioRepository.Adicionar(sessao);

        // RF28, RN13, RQ07: Auditoria obrigatória LGPD
        var log = new LogAcesso(
            id: 0,
            usuarioId: usuarioId,
            operacao: "REGISTRO_PRONTUARIO",
            detalhes: $"Atendimento registrado para o paciente {pacienteId} (Sessão {sessao.Id})."
        );
        _logAcessoRepository.Salvar(log);

        return sessao;
    }

    public void EditarAnotacao(int sessaoId, string novoTexto, string motivo, int usuarioId)
    {
        var sessao = _prontuarioRepository.ObterPorId(sessaoId);
        if (sessao == null)
            throw new InvalidOperationException("Sessão de prontuário não encontrada.");

        // RF16, RN17: Gera versão imutável com texto anterior e motivo
        sessao.AlterarAnotacao(novoTexto, motivo);
        _prontuarioRepository.Atualizar(sessao);

        // RF28, RN13, RQ07: Auditoria obrigatória LGPD
        var log = new LogAcesso(
            id: 0,
            usuarioId: usuarioId,
            operacao: "EDICAO_PRONTUARIO",
            detalhes: $"Anotação da sessão {sessaoId} alterada. Motivo: {motivo}"
        );
        _logAcessoRepository.Salvar(log);
    }

    public IEnumerable<SessaoProntuario> ConsultarHistorico(int pacienteId, int usuarioId)
    {
        var paciente = _pacienteRepository.ObterPorId(pacienteId);
        if (paciente == null)
            throw new InvalidOperationException("Paciente não encontrado.");

        var historico = _prontuarioRepository.ObterPorPaciente(pacienteId);

        // RF28, RN13, RQ07: Auditoria obrigatória LGPD de consulta a dados sensíveis
        var log = new LogAcesso(
            id: 0,
            usuarioId: usuarioId,
            operacao: "CONSULTA_PRONTUARIO",
            detalhes: $"Consulta ao histórico de prontuário do paciente {pacienteId}."
        );
        _logAcessoRepository.Salvar(log);

        return historico;
    }
}

