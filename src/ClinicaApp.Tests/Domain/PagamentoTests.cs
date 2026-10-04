using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Tests.Domain;

public class PagamentoTests
{
    [Fact]
    public void Deve_Criar_Pagamento_Com_Sucesso_E_Status_Pendente()
    {
        // Arrange & Act (RF19, RN08)
        var pagamento = new Pagamento(
            id: 1,
            agendamentoId: 100,
            valor: 150.00m
        );

        // Assert
        Assert.Equal(1, pagamento.Id);
        Assert.Equal(100, pagamento.AgendamentoId);
        Assert.Equal(150.00m, pagamento.Valor);
        Assert.Equal(StatusPagamento.Pendente, pagamento.Status);
        Assert.Null(pagamento.Forma);
        Assert.Null(pagamento.DataPagamento);
    }

    [Fact]
    public void RegistrarPagamento_Deve_Mudar_Status_Para_Pago_E_Salvar_Forma()
    {
        // Arrange (RN07)
        var pagamento = new Pagamento(1, 100, 200.00m);

        // Act: Paciente paga via Pix
        var dataHoraPagamento = new DateTime(2026, 10, 10, 15, 30, 0);
        pagamento.RegistrarPagamento(FormaPagamento.Pix, dataHoraPagamento);

        // Assert
        Assert.Equal(StatusPagamento.Pago, pagamento.Status);
        Assert.Equal(FormaPagamento.Pix, pagamento.Forma);
        Assert.Equal(dataHoraPagamento, pagamento.DataPagamento);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Se_Valor_For_Zero_Ou_Negativo()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Pagamento(1, 100, 0.00m));
        Assert.Throws<ArgumentException>(() => new Pagamento(1, 100, -50.00m));
    }
}
