using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Services;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceiroController : ControllerBase
{
    private readonly FinanceiroService _financeiroService;

    public FinanceiroController(FinanceiroService financeiroService)
    {
        _financeiroService = financeiroService;
    }

    public record NovaCobrancaRequest(
        [Range(1, int.MaxValue)] int AgendamentoId,
        [Range(0.01, 100000, ErrorMessage = "O valor deve estar entre R$ 0,01 e R$ 100.000,00.")] decimal Valor);
        
    public record QuitacaoRequest(string Forma);

    public record PagamentoDivididoRequest(
        [Range(0.01, 100000, ErrorMessage = "Valor de entrada deve ser maior que zero.")] decimal ValorEntrada,
        string FormaEntrada,
        [Range(0.01, 100000, ErrorMessage = "Valor de saldo deve ser maior que zero.")] decimal ValorSaldo,
        string FormaSaldo,
        bool SaldoPago = false);

    private static FormaPagamento NormalizarForma(string? forma)
    {
        if (string.IsNullOrWhiteSpace(forma)) return FormaPagamento.Pix;
        var f = forma.Trim().ToLowerInvariant()
            .Replace(" ", "")
            .Replace("ã", "a")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o");

        if (f.Contains("debito")) return FormaPagamento.CartaoDebito;
        if (f.Contains("credito")) return FormaPagamento.CartaoCredito;
        if (f.Contains("dinheiro")) return FormaPagamento.Dinheiro;
        if (f.Contains("boleto")) return FormaPagamento.Dinheiro;
        return FormaPagamento.Pix;
    }

    // POST: api/financeiro/cobranca
    [HttpPost("cobranca")]
    public IActionResult GerarCobranca([FromBody] NovaCobrancaRequest request)
    {
        int novoId = new Random().Next(100, 10000);
        var cobranca = _financeiroService.GerarCobranca(novoId, request.AgendamentoId, request.Valor);
        return Ok(cobranca);
    }

    // PUT: api/financeiro/{id}/quitar
    [HttpPut("{id}/quitar")]
    public IActionResult QuitarPagamento(int id, [FromBody] QuitacaoRequest request)
    {
        var forma = NormalizarForma(request.Forma);
        var pagamentoQuitado = _financeiroService.QuitarPagamento(id, forma);
        return Ok(pagamentoQuitado);
    }

    // POST: api/financeiro/{id}/pagamento-dividido (RF30, RN19)
    [HttpPost("{id}/pagamento-dividido")]
    public IActionResult RegistrarPagamentoDividido(int id, [FromBody] PagamentoDivididoRequest request)
    {
        var formaEntrada = NormalizarForma(request.FormaEntrada);
        var formaSaldo = NormalizarForma(request.FormaSaldo);

        var (entrada, saldo) = _financeiroService.RegistrarPagamentoDividido(
            id,
            request.ValorEntrada,
            formaEntrada,
            request.ValorSaldo,
            formaSaldo,
            request.SaldoPago
        );

        return Ok(new {
            mensagem = "Pagamento dividido registrado com sucesso (RF30, RN19).",
            entrada,
            saldo
        });
    }

    // GET: api/financeiro/pendentes
    [HttpGet("pendentes")]
    public IActionResult ObterPendencias()
    {
        var pendentes = _financeiroService.ObterPagamentosPendentes();
        return Ok(pendentes);
    }

    // GET: api/financeiro/resumo?mes=10&ano=2026
    [HttpGet("resumo")]
    public IActionResult GerarResumo([FromQuery] int mes, [FromQuery] int ano)
    {
        var resumo = _financeiroService.GerarResumoMensal(mes, ano);
        return Ok(resumo);
    }
}
