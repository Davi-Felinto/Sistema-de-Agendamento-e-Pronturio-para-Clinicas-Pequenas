using System;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public class Pagamento{
    public int Id { get; private set; }
    public int AgendamentoId { get; private set; }
    public decimal Valor { get; private set; }
    public StatusPagamento Status { get; private set; }
    public FormaPagamento? Forma { get; private set; }
    public DateTime? DataPagamento { get; private set; }

    // RF30, RN19: Tipo do lançamento (Integral, Entrada, SaldoRestante) e observações
    public string TipoLancamento { get; private set; }
    public string? Observacao { get; private set; }

    public Pagamento(
        int id,
        int agendamentoId,
        decimal valor,
        string? tipoLancamento = "Integral",
        string? observacao = null)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(valor));

        Id = id;
        AgendamentoId= agendamentoId;
        Valor = valor;
        Status = StatusPagamento.Pendente;
        Forma = null;
        DataPagamento = null;
        TipoLancamento = tipoLancamento ?? "Integral";
        Observacao = observacao;
    }

    /// <summary>
    /// RN07, RN08 e RN19: Registra a quitação da consulta ou parcela, atualizando o status para Pago
    /// e salvando a forma, momento e observação do pagamento.
    /// </summary>
    public void RegistrarPagamento(FormaPagamento forma, DateTime? dataPagamento = null, string? observacao = null)
    {
        Status = StatusPagamento.Pago;
        Forma = forma;
        DataPagamento = dataPagamento ?? DateTime.Now;
        if (!string.IsNullOrWhiteSpace(observacao))
        {
            Observacao = observacao;
        }
    }

    /// <summary>
    /// RF30, RN19: Permite reajustar o valor da parcela em caso de desmembramento ou desconto.
    /// </summary>
    public void AjustarValor(decimal novoValor)
    {
        if (novoValor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(novoValor));
        Valor = novoValor;
    }
}