using System;
using Xunit;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Tests.Domain;

public class ExameLaboratorioTests
{
    [Fact]
    public void Deve_Criar_Pedido_Exame_Com_Status_Coletado_E_Laudo_Pendente()
    {
        // Arrange & Act (RF34, RN23)
        var exame = new PedidoExameLaboratorio(
            id: 1,
            pacienteId: 2,
            nomePaciente: "Carlos Neves",
            nomeExameAmostra: "Painel Lipídico",
            laboratorioParceiro: "Laboratório Sabin",
            dataColeta: DateTime.Today,
            dataPrevisaoLaudo: DateTime.Today.AddDays(3),
            codigoRastreio: "SAB-1234"
        );

        // Assert
        Assert.Equal(1, exame.Id);
        Assert.Equal("Carlos Neves", exame.NomePaciente);
        Assert.Equal(StatusAmostraLaboratorio.ColetadoNaClinica, exame.StatusAmostra);
        Assert.True(exame.LaudoPendente);
        Assert.Null(exame.DataRecebimentoLaudo);
    }

    [Fact]
    public void AtualizarStatus_Para_LaudoLiberado_Deve_Definir_DataRecebimentoLaudo_E_Remover_Pendencia()
    {
        // Arrange
        var exame = new PedidoExameLaboratorio(
            id: 1,
            pacienteId: 2,
            nomePaciente: "Carlos Neves",
            nomeExameAmostra: "Biópsia Cutânea",
            laboratorioParceiro: "Laboratório Exame",
            dataColeta: DateTime.Today.AddDays(-5),
            dataPrevisaoLaudo: DateTime.Today
        );

        // Act
        exame.AtualizarStatus(StatusAmostraLaboratorio.LaudoLiberado, "Laudo liberado e anexado ao prontuário.");

        // Assert
        Assert.Equal(StatusAmostraLaboratorio.LaudoLiberado, exame.StatusAmostra);
        Assert.False(exame.LaudoPendente);
        Assert.NotNull(exame.DataRecebimentoLaudo);
        Assert.Equal("Laudo liberado e anexado ao prontuário.", exame.Observacoes);
    }
}
