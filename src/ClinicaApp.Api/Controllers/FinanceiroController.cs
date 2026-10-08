using System;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Services;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceiroController : ControllerBase
{
    private readonly FinanceiroService _financeiroService;

    public FinanceiroController(FinanceiroService financeiroService)
    {
        _financeiroService = financeiroService;
    }

    public record NovaCobrancaRequest(int AgendamentoId, decimal Valor);
    public record QuitacaoRequest(FormaPagamento Forma);

    // POST: api/financeiro/cobranca
    [HttpPost("cobranca")]
    public IActionResult GerarCobranca([FromBody] NovaCobrancaRequest request)
    {
        try
        {
            int novoId = new Random().Next(100, 10000);
            var cobranca = _financeiroService.GerarCobranca(novoId, request.AgendamentoId, request.Valor);
            return Ok(cobranca);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/financeiro/{id}/quitar
    [HttpPut("{id}/quitar")]
    public IActionResult QuitarPagamento(int id, [FromBody] QuitacaoRequest request)
    {
        try
        {
            var pagamentoQuitado = _financeiroService.QuitarPagamento(id, request.Forma);
            return Ok(pagamentoQuitado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
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
        try
        {
            var resumo = _financeiroService.GerarResumoMensal(mes, ano);
            return Ok(resumo);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

