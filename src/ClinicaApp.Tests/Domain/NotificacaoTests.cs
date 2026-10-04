using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Tests.Domain;

public class NotificacaoTests
{
    [Fact]
    public void Deve_Criar_Notificacao_Com_Status_Pendente_E_Zero_Tentativas()
    {
        // Arrange & Act (RF12, RF14)
        var notificacao = new Notificacao(
            id: 1,
            agendamentoId: 100,
            tipo: TipoNotificacao.Confirmacao,
            destinatarioTelefone: "(61) 98888-7777",
            mensagem: "Sua consulta foi agendada para 10/10 às 14h."
        );

        // Assert
        Assert.Equal(1, notificacao.Id);
        Assert.Equal(100, notificacao.AgendamentoId);
        Assert.Equal(TipoNotificacao.Confirmacao, notificacao.Tipo);
        Assert.Equal(StatusNotificacao.Pendente, notificacao.Status);
        Assert.Equal(0, notificacao.TentativasEnvio);
        Assert.Equal(CanalNotificacao.WhatsApp, notificacao.Canal);
    }

    [Fact]
    public void RegistrarSucesso_Deve_Marcar_Como_Enviado()
    {
        var notificacao = new Notificacao(1, 100, TipoNotificacao.Confirmacao, "(61) 98888-7777", "Msg");
        
        notificacao.RegistrarSucesso(foiReenvio: false);

        Assert.Equal(StatusNotificacao.Enviado, notificacao.Status);
        Assert.Null(notificacao.ProximaTentativa);
    }

    [Fact]
    public void RegistrarFalha_Deve_Agendar_Retentativa_Apos_15_Minutos()
    {
        // Arrange (RN14)
        var notificacao = new Notificacao(1, 100, TipoNotificacao.Confirmacao, "(61) 98888-7777", "Msg");
        var momentoFalha = new DateTime(2026, 10, 10, 14, 0, 0);

        // Act: Primeira falha
        notificacao.RegistrarFalha(momentoFalha);

        // Assert
        Assert.Equal(StatusNotificacao.Falha, notificacao.Status);
        Assert.Equal(1, notificacao.TentativasEnvio);
        Assert.Equal(momentoFalha.AddMinutes(15), notificacao.ProximaTentativa);
        
        // Antes dos 15 min não pode reenviar
        Assert.False(notificacao.PodeReenviar(momentoFalha.AddMinutes(10)));
        
        // Passados 15 min pode reenviar (RN14)
        Assert.True(notificacao.PodeReenviar(momentoFalha.AddMinutes(15)));
    }

    [Fact]
    public void RegistrarFalha_Pela_Segunda_Vez_Nao_Deve_Permitir_Mais_Retentativas()
    {
        // Arrange (RN14, RN15): Já falhou 1 vez
        var notificacao = new Notificacao(1, 100, TipoNotificacao.Confirmacao, "(61) 98888-7777", "Msg");
        notificacao.RegistrarFalha(DateTime.Now);

        // Act: Segunda falha na retentativa
        notificacao.RegistrarFalha(DateTime.Now.AddMinutes(15));

        // Assert: Atingiu o limite de 2 tentativas (não pode mais reenviar - RN15)
        Assert.Equal(2, notificacao.TentativasEnvio);
        Assert.False(notificacao.PodeReenviar(DateTime.Now.AddHours(1)));
        Assert.Null(notificacao.ProximaTentativa);
    }
}
