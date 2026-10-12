using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Services;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceiroController : ControllerBase
{
    private readonly FinanceiroService _financeiroService;
    private readonly GeradorDeIds _geradorDeIds;

    public FinanceiroController(FinanceiroService financeiroService, GeradorDeIds geradorDeIds)
    {
        _financeiroService = financeiroService;
        _geradorDeIds = geradorDeIds;
    }

    public record NovaCobrancaRequest(
        [Range(1, int.MaxValue)] int AgendamentoId,
        [Range(0.01, 100000, ErrorMessage = "O valor deve estar entre R$ 0,01 e R$ 100.000,00.")] decimal Valor);
    public record QuitacaoRequest([EnumDataType(typeof(FormaPagamento))] FormaPagamento Forma);

    // POST: api/financeiro/cobranca
    [HttpPost("cobranca")]
    public IActionResult GerarCobranca([FromBody] NovaCobrancaRequest request)
    {
        try
        {
            int novoId = _geradorDeIds.ProximoPagamento();
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

