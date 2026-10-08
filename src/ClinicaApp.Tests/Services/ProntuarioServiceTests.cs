using System;
using System.Linq;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

namespace ClinicaApp.Tests.Services;

public class ProntuarioServiceTests
{
    private readonly InMemoryProntuarioRepository _prontuarioRepository;
    private readonly InMemoryPacienteRepository _pacienteRepository;
    private readonly InMemoryLogAcessoRepository _logAcessoRepository;
    private readonly ProntuarioService _prontuarioService;

    public ProntuarioServiceTests()
    {
        _prontuarioRepository = new InMemoryProntuarioRepository();
        _pacienteRepository = new InMemoryPacienteRepository();
        _logAcessoRepository = new InMemoryLogAcessoRepository();

        _prontuarioService = new ProntuarioService(
            _prontuarioRepository,
            _pacienteRepository,
            _logAcessoRepository
        );
    }

    private Paciente CriarPacienteValido(int id = 1, bool ativo = true)
    {
        var paciente = new Paciente(
            id: id,
            nome: "Carlos Eduardo",
            documentoIdentificacao: "111.222.333-44",
            dataNascimento: new DateTime(1990, 1, 1),
            telefone: "(61) 98888-7777",
            email: "carlos@email.com",
            endereco: "Brasília - DF"
        );

        if (!ativo)
        {
            paciente.InativarComAnonimizacao();
        }

        return paciente;
    }

    [Fact]
    public void Deve_Registrar_Sessao_Com_Sucesso_E_Gerar_Log_Auditoria()
    {
        // Arrange (RF15, RN05, RF28, RN13, RQ07)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        // Act
        var sessao = _prontuarioService.RegistrarSessao(
            id: 1,
            pacienteId: paciente.Id,
            agendamentoId: 10,
            anotacoesClinicas: "Paciente relatou melhora progressiva.",
            usuarioId: 99
        );

        // Assert
        Assert.NotNull(sessao);
        Assert.Equal("Paciente relatou melhora progressiva.", sessao.AnotacoesClinicas);

        var logs = _logAcessoRepository.ObterTodos().ToList();
        Assert.Single(logs);
        Assert.Equal("REGISTRO_PRONTUARIO", logs[0].Operacao);
        Assert.Equal(99, logs[0].UsuarioId);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Registrar_Sessao_Para_Paciente_Inexistente_Ou_Inativo()
    {
        // RN16: Paciente inativo
        var pacienteInativo = CriarPacienteValido(id: 2, ativo: false);
        _pacienteRepository.Adicionar(pacienteInativo);

        // Act & Assert (Paciente inativo)
        var exInativo = Assert.Throws<InvalidOperationException>(() =>
            _prontuarioService.RegistrarSessao(1, pacienteInativo.Id, 10, "Anotação", 99));
        Assert.Contains("inativo", exInativo.Message);

        // Act & Assert (Paciente não cadastrado)
        var exInexistente = Assert.Throws<InvalidOperationException>(() =>
            _prontuarioService.RegistrarSessao(2, 999, 10, "Anotação", 99));
        Assert.Contains("inválido ou inativo", exInexistente.Message);
    }

    [Fact]
    public void Deve_Editar_Anotacao_Gerando_Versao_Imutavel_E_Log_Auditoria()
    {
        // Arrange (RF16, RN17, RF28, RN13, RQ07)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var sessao = _prontuarioService.RegistrarSessao(
            id: 1,
            pacienteId: paciente.Id,
            agendamentoId: 10,
            anotacoesClinicas: "Texto inicial com erro de digitação.",
            usuarioId: 99
        );

        // Act
        _prontuarioService.EditarAnotacao(
            sessaoId: sessao.Id,
            novoTexto: "Texto corrigido e atualizado.",
            motivo: "Correção de termo técnico",
            usuarioId: 99
        );

        // Assert
        var sessaoAtualizada = _prontuarioRepository.ObterPorId(sessao.Id);
        Assert.NotNull(sessaoAtualizada);
        Assert.Equal("Texto corrigido e atualizado.", sessaoAtualizada.AnotacoesClinicas);
        Assert.Single(sessaoAtualizada.HistoricoVersoes);

        var versaoAnterior = sessaoAtualizada.HistoricoVersoes.First();
        Assert.Equal("Texto inicial com erro de digitação.", versaoAnterior.TextoAnterior);
        Assert.Equal("Correção de termo técnico", versaoAnterior.MotivoAlteracao);

        var logs = _logAcessoRepository.ObterTodos().ToList();
        Assert.Equal(2, logs.Count); // 1 do registro + 1 da edição
        Assert.Equal("EDICAO_PRONTUARIO", logs[1].Operacao);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Editar_Sessao_Inexistente()
    {
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _prontuarioService.EditarAnotacao(999, "Novo texto", "Motivo", 99));

        Assert.Equal("Sessão de prontuário não encontrada.", ex.Message);
    }

    [Fact]
    public void Deve_Consultar_Historico_Do_Paciente_E_Gerar_Log_Auditoria()
    {
        // Arrange (RF28, RN13, RQ07)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        _prontuarioService.RegistrarSessao(1, paciente.Id, 10, "Sessão 1", 99);
        _prontuarioService.RegistrarSessao(2, paciente.Id, 11, "Sessão 2", 99);

        // Act
        var historico = _prontuarioService.ConsultarHistorico(paciente.Id, usuarioId: 50).ToList();

        // Assert
        Assert.Equal(2, historico.Count);

        var logs = _logAcessoRepository.ObterTodos().ToList();
        var logConsulta = logs.Last();
        Assert.Equal("CONSULTA_PRONTUARIO", logConsulta.Operacao);
        Assert.Equal(50, logConsulta.UsuarioId);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Consultar_Historico_De_Paciente_Inexistente()
    {
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _prontuarioService.ConsultarHistorico(pacienteId: 999, usuarioId: 50));

        Assert.Equal("Paciente não encontrado.", ex.Message);
    }
}
