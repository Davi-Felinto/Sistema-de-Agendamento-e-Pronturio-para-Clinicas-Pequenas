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
        
    public record QuitacaoRequest([EnumDataType(typeof(FormaPagamento))] FormaPagamento Forma);

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
        var pagamentoQuitado = _financeiroService.QuitarPagamento(id, request.Forma);
        return Ok(pagamentoQuitado);
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
