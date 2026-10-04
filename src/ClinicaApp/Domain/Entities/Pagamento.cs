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

    public Pagamento(int id, int agendamentoId, decimal valor){
        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(valor));

        Id = id;
        AgendamentoId= agendamentoId;
        Valor = valor;
        Status = StatusPagamento.Pendente;
        Forma = null;
        DataPagamento = null;
    }

    /// <summary>
    /// RN07 e RN08: Registra a quitação da consulta, atualizando o status para Pago
    /// e salvando a forma e o momento do pagamento.
    /// </summary>
    public void RegistrarPagamento(FormaPagamento forma, DateTime? dataPagamento = null){
        Status = StatusPagamento.Pago;
        Forma = forma;
        DataPagamento = dataPagamento ?? DateTime.Now;
    }
}