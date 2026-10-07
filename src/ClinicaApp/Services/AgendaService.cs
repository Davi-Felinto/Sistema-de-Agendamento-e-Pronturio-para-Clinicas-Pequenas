using System;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Services;

public class AgendaService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly INotificador _notificador;

    public AgendaService(
        IAgendamentoRepository agendamentoRepository,
        IPacienteRepository pacienteRepository,
        INotificador notificador)
    {
        _agendamentoRepository = agendamentoRepository ?? throw new ArgumentNullException(nameof(agendamentoRepository));
        _pacienteRepository = pacienteRepository ?? throw new ArgumentNullException(nameof(pacienteRepository));
        _notificador = notificador ?? throw new ArgumentNullException(nameof(notificador));
    }

    public Agendamento AgendarConsulta(
        int id,
        int pacienteId,
        int profissionalId,
        DateTime dataHoraInicio,
        DateTime dataHoraFim,
        string? observacoes = null)
    {
        // 1. Valida se o paciente existe e está ativo (RN16)
        var paciente = _pacienteRepository.ObterPorId(pacienteId);
        if (paciente == null || !paciente.Ativo)
            throw new InvalidOperationException("Paciente inválido ou inativo no sistema.");

        // 2. Busca consultas do profissional no dia para checar conflitos (RN01, RN02)
        var consultasDoDia = _agendamentoRepository.ObterPorProfissionalEPeriodo(profissionalId, dataHoraInicio.Date);
        foreach (var consultaExistente in consultasDoDia)
        {
            if (consultaExistente.TemConflito(dataHoraInicio, dataHoraFim))
            {
                throw new InvalidOperationException("Horário indisponível: o profissional já possui consulta agendada neste período.");
            }
        }

        // 3. Cria e persiste o novo agendamento
        var novoAgendamento = new Agendamento(
            id,
            pacienteId,
            profissionalId,
            dataHoraInicio,
            dataHoraFim,
            observacoes);

        novoAgendamento.Confirmar();
        _agendamentoRepository.Adicionar(novoAgendamento);

        // 4. Dispara a notificação de confirmação via WhatsApp (RF12, RN03)
        var mensagem = $"Olá, {paciente.Nome}! Sua consulta foi agendada para {dataHoraInicio:dd/MM/yyyy HH:mm}.";
        var notificacao = new Notificacao(
            id: id,
            agendamentoId: novoAgendamento.Id,
            tipo: TipoNotificacao.Confirmacao,
            destinatarioTelefone: paciente.Telefone,
            mensagem: mensagem,
            canal: CanalNotificacao.WhatsApp
        );
        _notificador.Enviar(notificacao);

        return novoAgendamento;
    }

    public void CancelarConsulta(int agendamentoId, DateTime momentoCancelamento)
    {
        var agendamento = _agendamentoRepository.ObterPorId(agendamentoId);
        if (agendamento == null)
            throw new InvalidOperationException("Agendamento não encontrado.");

        // RN10: Valida se é cancelamento tardio (< 24h)
        agendamento.Cancelar(momentoCancelamento);
        _agendamentoRepository.Atualizar(agendamento);
    }
}