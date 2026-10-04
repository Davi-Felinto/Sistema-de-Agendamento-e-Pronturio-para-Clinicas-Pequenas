using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Tests.Domain;

public class AgendamentoTests
{
    [Fact]
    public void Deve_Criar_Agendamento_Com_Sucesso_E_Status_Pendente()
    {
        // Arrange & Act
        var inicio = new DateTime(2026, 10, 10, 14, 0, 0);
        var fim = inicio.AddHours(1);

        var agendamento = new Agendamento(
            id: 1,
            pacienteId: 10,
            profissionalId: 5,
            dataHoraInicio: inicio,
            dataHoraFim: fim,
            observacoes: "Primeira consulta"
        );

        // Assert
        Assert.Equal(1, agendamento.Id);
        Assert.Equal(10, agendamento.PacienteId);
        Assert.Equal(5, agendamento.ProfissionalId);
        Assert.Equal(StatusAgendamento.Pendente, agendamento.Status);
        Assert.False(agendamento.CancelamentoTardio);
    }

    [Fact]
    public void TemConflito_Com_Horarios_Sobrepostos_Deve_Retornar_True()
    {
        // Arrange (RN01, RN02): Consulta das 14h às 15h
        var inicio = new DateTime(2026, 10, 10, 14, 0, 0);
        var fim = new DateTime(2026, 10, 10, 15, 0, 0);

        var agendamento = new Agendamento(1, 10, 5, inicio, fim);

        // Act & Assert
        // Tentativa de marcar das 14h30 às 15h30 (colisão direta!)
        var temConflito = agendamento.TemConflito(
            new DateTime(2026, 10, 10, 14, 30, 0),
            new DateTime(2026, 10, 10, 15, 30, 0)
        );

        Assert.True(temConflito, "Deve acusar conflito de horários sobrepostos (RN01, RN02).");
    }

    [Fact]
    public void TemConflito_Com_Horarios_Diferentes_Deve_Retornar_False()
    {
        // Arrange: Consulta das 14h às 15h
        var inicio = new DateTime(2026, 10, 10, 14, 0, 0);
        var fim = new DateTime(2026, 10, 10, 15, 0, 0);

        var agendamento = new Agendamento(1, 10, 5, inicio, fim);

        // Act & Assert: Consulta das 15h às 16h (sem sobreposição)
        var temConflito = agendamento.TemConflito(
            new DateTime(2026, 10, 10, 15, 0, 0),
            new DateTime(2026, 10, 10, 16, 0, 0)
        );

        Assert.False(temConflito);
    }

    [Fact]
    public void Cancelar_Com_Menos_De_24_Horas_Deve_Marcar_Cancelamento_Tardio()
    {
        // Arrange (RN10): Consulta marcada para amanhã às 10h
        var dataConsulta = new DateTime(2026, 10, 10, 10, 0, 0);
        var agendamento = new Agendamento(1, 10, 5, dataConsulta, dataConsulta.AddHours(1));

        // Act: Paciente cancela faltando apenas 5 horas (menos de 24h)
        var momentoCancelamento = dataConsulta.AddHours(-5);
        agendamento.Cancelar(momentoCancelamento);

        // Assert
        Assert.Equal(StatusAgendamento.Cancelado, agendamento.Status);
        Assert.True(agendamento.CancelamentoTardio, "Cancelamento com < 24h deve ser sinalizado como tardio (RN10).");
    }
}
