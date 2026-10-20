using System;
using Xunit;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Tests.Domain;

public class AvaliacaoAtendimentoTests
{
    [Fact]
    public void Deve_Criar_Avaliacao_Com_Sucesso_E_Calcular_Media_Geral()
    {
        // Arrange & Act (RF32, RN21)
        var avaliacao = new AvaliacaoAtendimento(
            id: 1,
            agendamentoId: 10,
            pacienteId: 5,
            nomePaciente: "Mariana Duarte",
            dataAvaliacao: DateTime.Now,
            notaRecepcao: 5,
            notaAtendimentoClinico: 4,
            notaEspacoFisico: 5,
            comentarioOuvidoria: "Excelente acolhimento!",
            anonimo: false
        );

        // Assert
        Assert.Equal(1, avaliacao.Id);
        Assert.Equal("Mariana Duarte", avaliacao.NomePaciente);
        Assert.Equal(5, avaliacao.NotaRecepcao);
        Assert.Equal(4, avaliacao.NotaAtendimentoClinico);
        Assert.Equal(5, avaliacao.NotaEspacoFisico);
        Assert.Equal(4.7, avaliacao.MediaGeral); // (5+4+5)/3 = 4.6667 -> 4.7
        Assert.False(avaliacao.Anonimo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Deve_Lancar_Excecao_Para_Notas_Fora_Do_Intervalo_1_A_5(int notaInvalida)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new AvaliacaoAtendimento(
                id: 1,
                agendamentoId: 10,
                pacienteId: 5,
                nomePaciente: "Mariana",
                dataAvaliacao: DateTime.Now,
                notaRecepcao: notaInvalida,
                notaAtendimentoClinico: 5,
                notaEspacoFisico: 5
            )
        );
    }

    [Fact]
    public void Deve_Anonimizar_Nome_Do_Paciente_Quando_Avaliacao_For_Anonima()
    {
        // Arrange & Act
        var avaliacao = new AvaliacaoAtendimento(
            id: 2,
            agendamentoId: 11,
            pacienteId: 6,
            nomePaciente: "Carlos Neves",
            dataAvaliacao: DateTime.Now,
            notaRecepcao: 5,
            notaAtendimentoClinico: 5,
            notaEspacoFisico: 5,
            anonimo: true
        );

        // Assert
        Assert.Equal("Paciente Anônimo", avaliacao.NomePaciente);
        Assert.True(avaliacao.Anonimo);
    }
}
