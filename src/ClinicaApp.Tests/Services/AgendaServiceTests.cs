using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Infrastructure.External;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

namespace ClinicaApp.Tests.Services;

public class AgendaServiceTests
{
    private readonly InMemoryAgendamentoRepository _agendamentoRepository;
    private readonly InMemoryPacienteRepository _pacienteRepository;
    private readonly NotificadorWhatsApp _notificador;
    private readonly AgendaService _agendaService;

    public AgendaServiceTests()
    {
        _agendamentoRepository = new InMemoryAgendamentoRepository();
        _pacienteRepository = new InMemoryPacienteRepository();
        _notificador = new NotificadorWhatsApp();
        _agendaService = new AgendaService(_agendamentoRepository, _pacienteRepository, _notificador);
    }

    private Paciente CriarPacienteValido(int id = 1, bool ativo = true)
    {
        var paciente = new Paciente(
            id: id,
            nome: "Maria Santos",
            documentoIdentificacao: "123.456.789-00",
            dataNascimento: new DateTime(1995, 5, 20),
            telefone: "(61) 99999-8888",
            email: "maria.santos@email.com",
            endereco: "Brasília - DF"
        );

        if (!ativo)
        {
            paciente.InativarComAnonimizacao();
        }

        return paciente;
    }

    [Fact]
    public void Deve_Agendar_Consulta_Com_Sucesso_E_Confirmar()
    {
        // Arrange (RF06, RF12, RN03)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(2).AddHours(14);
        var fim = inicio.AddHours(1);

        // Act
        var agendamento = _agendaService.AgendarConsulta(
            id: 1,
            pacienteId: paciente.Id,
            profissionalId: 10,
            dataHoraInicio: inicio,
            dataHoraFim: fim,
            observacoes: "Primeira consulta de avaliação"
        );

        // Assert
        Assert.NotNull(agendamento);
        Assert.Equal(StatusAgendamento.Confirmado, agendamento.Status);
        Assert.Equal(paciente.Id, agendamento.PacienteId);
        Assert.Equal(10, agendamento.ProfissionalId);
        Assert.NotNull(_agendamentoRepository.ObterPorId(1));
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Agendar_Para_Paciente_Inexistente()
    {
        // Arrange
        var inicio = DateTime.Today.AddDays(2).AddHours(10);
        var fim = inicio.AddHours(1);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _agendaService.AgendarConsulta(1, 999, 10, inicio, fim));

        Assert.Equal("Paciente inválido ou inativo no sistema.", ex.Message);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Agendar_Para_Paciente_Inativo()
    {
        // Arrange (RN16)
        var paciente = CriarPacienteValido(id: 2, ativo: false);
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(2).AddHours(10);
        var fim = inicio.AddHours(1);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _agendaService.AgendarConsulta(1, paciente.Id, 10, inicio, fim));

        Assert.Equal("Paciente inválido ou inativo no sistema.", ex.Message);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Houver_Conflito_De_Horario()
    {
        // Arrange (RN01, RN02)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(2).AddHours(15);
        var fim = inicio.AddHours(1);

        _agendaService.AgendarConsulta(1, paciente.Id, 10, inicio, fim);

        // Act & Assert: Tentativa de sobreposição para o mesmo profissional
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _agendaService.AgendarConsulta(2, paciente.Id, 10, inicio.AddMinutes(30), fim.AddMinutes(30)));

        Assert.Equal("Horário indisponível: o profissional já possui consulta agendada neste período.", ex.Message);
    }

    [Fact]
    public void Deve_Permitir_Agendar_No_Mesmo_Horario_Para_Profissional_Diferente()
    {
        // Arrange
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(2).AddHours(9);
        var fim = inicio.AddHours(1);

        _agendaService.AgendarConsulta(1, paciente.Id, 10, inicio, fim);

        // Act: Profissional 20 no mesmo horário
        var agendamento2 = _agendaService.AgendarConsulta(2, paciente.Id, 20, inicio, fim);

        // Assert
        Assert.NotNull(agendamento2);
        Assert.Equal(20, agendamento2.ProfissionalId);
    }

    [Fact]
    public void Deve_Cancelar_Consulta_Com_Sucesso()
    {
        // Arrange (RF08)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(5).AddHours(10);
        var fim = inicio.AddHours(1);

        _agendaService.AgendarConsulta(1, paciente.Id, 10, inicio, fim);

        // Act (cancelado 5 dias antes -> antecedência normal > 24h)
        _agendaService.CancelarConsulta(1, DateTime.Today);

        // Assert
        var agendamento = _agendamentoRepository.ObterPorId(1);
        Assert.NotNull(agendamento);
        Assert.Equal(StatusAgendamento.Cancelado, agendamento.Status);
        Assert.False(agendamento.CancelamentoTardio);
    }

    [Fact]
    public void Deve_Sinalizar_Cancelamento_Tardio_Quando_Menos_De_24h()
    {
        // Arrange (RN10)
        var paciente = CriarPacienteValido();
        _pacienteRepository.Adicionar(paciente);

        var inicio = DateTime.Today.AddDays(1).AddHours(10);
        var fim = inicio.AddHours(1);

        _agendaService.AgendarConsulta(1, paciente.Id, 10, inicio, fim);

        // Act: cancelado apenas 2 horas antes da consulta
        var momentoCancelamento = inicio.AddHours(-2);
        _agendaService.CancelarConsulta(1, momentoCancelamento);

        // Assert
        var agendamento = _agendamentoRepository.ObterPorId(1);
        Assert.NotNull(agendamento);
        Assert.Equal(StatusAgendamento.Cancelado, agendamento.Status);
        Assert.True(agendamento.CancelamentoTardio);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Cancelar_Agendamento_Inexistente()
    {
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _agendaService.CancelarConsulta(999, DateTime.Now));

        Assert.Equal("Agendamento não encontrado.", ex.Message);
    }
}
