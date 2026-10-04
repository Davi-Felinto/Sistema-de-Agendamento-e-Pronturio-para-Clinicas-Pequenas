using System;
using System.Linq;
using Xunit;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Tests.Domain;

public class SessaoProntuarioTests
{
    [Fact]
    public void Deve_Criar_Sessao_Prontuario_Com_Sucesso()
    {
        // Arrange & Act (RF15)
        var sessao = new SessaoProntuario(
            id: 1,
            pacienteId: 10,
            agendamentoId: 100,
            anotacoesClinicas: "Paciente relata cefaleia frequente e insônia."
        );

        // Assert
        Assert.Equal(1, sessao.Id);
        Assert.Equal(10, sessao.PacienteId);
        Assert.Equal(100, sessao.AgendamentoId);
        Assert.Equal("Paciente relata cefaleia frequente e insônia.", sessao.AnotacoesClinicas);
        Assert.Empty(sessao.HistoricoVersoes);
    }

    [Fact]
    public void AlterarAnotacao_Deve_Salvar_Versao_Anterior_No_Historico_Imutavel()
    {
        // Arrange (RF16, RN17): Criação inicial
        var sessao = new SessaoProntuario(
            id: 1,
            pacienteId: 10,
            agendamentoId: 100,
            anotacoesClinicas: "Diagnóstico inicial: estresse."
        );

        // Act: Profissional corrige/altera anotação justificando o motivo
        sessao.AlterarAnotacao(
            novoTexto: "Diagnóstico corrigido: estresse moderado e ansiedade leve.",
            motivo: "Complementação de laudo após resultado de exames."
        );

        // Assert
        // 1. O texto atual deve ser o novo
        Assert.Equal("Diagnóstico corrigido: estresse moderado e ansiedade leve.", sessao.AnotacoesClinicas);

        // 2. O histórico deve conter a versão anterior arquivada (RN17)
        Assert.Single(sessao.HistoricoVersoes);

        var versaoAnterior = sessao.HistoricoVersoes.First();
        Assert.Equal("Diagnóstico inicial: estresse.", versaoAnterior.TextoAnterior);
        Assert.Equal("Complementação de laudo após resultado de exames.", versaoAnterior.MotivoAlteracao);
    }
}
