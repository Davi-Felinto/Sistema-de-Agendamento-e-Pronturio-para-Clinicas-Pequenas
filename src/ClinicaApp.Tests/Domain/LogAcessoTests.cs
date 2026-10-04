using System;
using Xunit;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Tests.Domain;

public class LogAcessoTests
{
    [Fact]
    public void Deve_Criar_LogAcesso_Com_Sucesso()
    {
        // Arrange & Act (RF28, RN13, RQ07)
        var dataHora = new DateTime(2026, 10, 10, 16, 0, 0);
        var log = new LogAcesso(
            id: 1,
            usuarioId: 5,
            operacao: "CONSULTA_PRONTUARIO",
            detalhes: "Acessou histórico do paciente 10",
            dataHora: dataHora
        );

        // Assert
        Assert.Equal(1, log.Id);
        Assert.Equal(5, log.UsuarioId);
        Assert.Equal("CONSULTA_PRONTUARIO", log.Operacao);
        Assert.Equal("Acessou histórico do paciente 10", log.Detalhes);
        Assert.Equal(dataHora, log.DataHora);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Se_Usuario_Ou_Operacao_Forem_Invalidos()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new LogAcesso(1, 0, "OP", "detalhes"));
        Assert.Throws<ArgumentException>(() => new LogAcesso(1, 5, "", "detalhes"));
    }
}
