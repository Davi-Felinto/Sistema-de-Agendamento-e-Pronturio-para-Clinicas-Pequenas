using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Services;

public record ResumoFinanceiroMensal(
    int Mes,
    int Ano,
    decimal TotalRecebido,
    decimal TotalPendente,
    int QuantidadePagos,
    int QuantidadePendentes
);

public class FinanceiroService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public FinanceiroService(
        IPagamentoRepository pagamentoRepository,
        IAgendamentoRepository agendamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository ?? throw new ArgumentNullException(nameof(pagamentoRepository));
        _agendamentoRepository = agendamentoRepository ?? throw new ArgumentNullException(nameof(agendamentoRepository));
    }

    public Pagamento GerarCobranca(int id, int agendamentoId, decimal valor)
    {
        // RF19, RN07: Valida se o agendamento existe
        var agendamento = _agendamentoRepository.ObterPorId(agendamentoId);
        if (agendamento == null)
            throw new InvalidOperationException("Agendamento não encontrado.");

        // Evita cobrança em duplicidade para a mesma consulta
        var pagamentoExistente = _pagamentoRepository.ObterPorAgendamentoId(agendamentoId);
        if (pagamentoExistente != null)
            throw new InvalidOperationException("Já existe uma cobrança cadastrada para este agendamento.");

        var novoPagamento = new Pagamento(id, agendamentoId, valor);
        _pagamentoRepository.Adicionar(novoPagamento);

        return novoPagamento;
    }

    public Pagamento QuitarPagamento(int pagamentoId, FormaPagamento forma, DateTime? dataPagamento = null)
    {
        var pagamento = _pagamentoRepository.ObterPorId(pagamentoId) 
                     ?? _pagamentoRepository.ObterPorAgendamentoId(pagamentoId);
        if (pagamento == null)
            throw new InvalidOperationException("Pagamento não encontrado.");

        if (pagamento.Status == StatusPagamento.Pago)
            throw new InvalidOperationException("Este pagamento já foi liquidado anteriormente.");

        // RF20, RN07, RN08: Registra quitação com forma e data
        pagamento.RegistrarPagamento(forma, dataPagamento);
        _pagamentoRepository.Atualizar(pagamento);

        return pagamento;
    }

    // RF30, RN19: Permite liquidar uma cobrança dividindo entre entrada e saldo restante
    public (Pagamento Entrada, Pagamento Saldo) RegistrarPagamentoDividido(
        int pagamentoOriginalId,
        decimal valorEntrada,
        FormaPagamento formaEntrada,
        decimal valorSaldo,
        FormaPagamento formaSaldo,
        bool saldoPago = false,
        DateTime? momento = null)
    {
        var pagamentoOriginal = _pagamentoRepository.ObterPorId(pagamentoOriginalId)
                             ?? _pagamentoRepository.ObterPorAgendamentoId(pagamentoOriginalId);
        if (pagamentoOriginal == null)
            throw new InvalidOperationException("Cobrança original não encontrada.");

        if (pagamentoOriginal.Status == StatusPagamento.Pago)
            throw new InvalidOperationException("Esta cobrança já foi liquidada.");

        if (valorEntrada <= 0 || valorSaldo <= 0)
            throw new ArgumentException("Valores de entrada e saldo devem ser maiores que zero.");

        if ((valorEntrada + valorSaldo) != pagamentoOriginal.Valor)
            throw new InvalidOperationException($"A soma da entrada ({valorEntrada:C}) com o saldo ({valorSaldo:C}) deve ser igual ao valor total da consulta ({pagamentoOriginal.Valor:C}).");

        var momentoEfetivo = momento ?? DateTime.Now;

        // Atualiza a cobrança original para ser o lançamento da Entrada
        pagamentoOriginal.AjustarValor(valorEntrada);
        pagamentoOriginal.RegistrarPagamento(formaEntrada, momentoEfetivo, "Entrada de pagamento dividido");
        _pagamentoRepository.Atualizar(pagamentoOriginal);

        // Gera o lançamento do saldo restante vinculado ao mesmo agendamento
        int novoId = _pagamentoRepository.ListarTodos().Any() ? _pagamentoRepository.ListarTodos().Max(p => p.Id) + 1 : 1;
        var pagamentoSaldo = new Pagamento(novoId, pagamentoOriginal.AgendamentoId, valorSaldo, "SaldoRestante", "Saldo restante de pagamento dividido");
        if (saldoPago)
        {
            pagamentoSaldo.RegistrarPagamento(formaSaldo, momentoEfetivo, "Saldo restante quitado");
        }
        _pagamentoRepository.Adicionar(pagamentoSaldo);

        return (pagamentoOriginal, pagamentoSaldo);
    }

    public IEnumerable<Pagamento> ObterPagamentosPendentes()
    {
        // RF21, RN08: Lista todas as pendências
        return _pagamentoRepository.ListarTodos()
            .Where(p => p.Status == StatusPagamento.Pendente);
    }

    public IEnumerable<Pagamento> ObterPendenciasPorPaciente(int pacienteId)
    {
        // RF21, RN08: Filtra pendências associadas a um paciente específico
        var pendentes = _pagamentoRepository.ListarTodos()
            .Where(p => p.Status == StatusPagamento.Pendente);

        return pendentes.Where(p =>
        {
            var agendamento = _agendamentoRepository.ObterPorId(p.AgendamentoId);
            return agendamento != null && agendamento.PacienteId == pacienteId;
        });
    }

    public ResumoFinanceiroMensal GerarResumoMensal(int mes, int ano)
    {
        // RF22, RN09: Valida mês
        if (mes < 1 || mes > 12)
            throw new ArgumentOutOfRangeException(nameof(mes), "O mês deve estar entre 1 e 12.");

        var todos = _pagamentoRepository.ListarTodos().ToList();

        var pagosNoMes = todos.Where(p =>
            p.Status == StatusPagamento.Pago &&
            p.DataPagamento.HasValue &&
            p.DataPagamento.Value.Month == mes &&
            p.DataPagamento.Value.Year == ano).ToList();

        var pendentes = todos.Where(p => p.Status == StatusPagamento.Pendente).ToList();

        return new ResumoFinanceiroMensal(
            Mes: mes,
            Ano: ano,
            TotalRecebido: pagosNoMes.Sum(p => p.Valor),
            TotalPendente: pendentes.Sum(p => p.Valor),
            QuantidadePagos: pagosNoMes.Count,
            QuantidadePendentes: pendentes.Count
        );
    }
}