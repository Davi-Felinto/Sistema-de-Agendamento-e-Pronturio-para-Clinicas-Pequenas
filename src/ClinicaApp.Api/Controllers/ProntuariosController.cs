using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProntuariosController : ControllerBase
{
    private readonly ProntuarioService _prontuarioService;
    private readonly GeradorDeIds _geradorDeIds;

    public ProntuariosController(ProntuarioService prontuarioService, GeradorDeIds geradorDeIds)
    {
        _prontuarioService = prontuarioService;
        _geradorDeIds = geradorDeIds;
    }

    // RN13: o autor da operação vem sempre do token (User), nunca do corpo da requisição,
    // para que ninguém consiga registrar ou consultar um prontuário em nome de outro usuário.
    public record RegistroSessaoRequest(
        [Range(1, int.MaxValue)] int PacienteId,
        int AgendamentoId,
        [Required, StringLength(10000)] string AnotacoesClinicas);
    public record EdicaoAnotacaoRequest(
        [Required, StringLength(10000)] string NovoTexto,
        [Required, StringLength(500)] string Motivo);

    // POST: api/prontuarios
    [HttpPost]
    public IActionResult RegistrarSessao([FromBody] RegistroSessaoRequest request)
    {
        try
        {
            int novoId = _geradorDeIds.ProximaSessao();

            var sessao = _prontuarioService.RegistrarSessao(
                id: novoId,
                pacienteId: request.PacienteId,
                agendamentoId: request.AgendamentoId,
                anotacoesClinicas: request.AnotacoesClinicas,
                usuarioId: User.ObterId()
            );

            return Ok(sessao);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/prontuarios/{id}
    [HttpPut("{id}")]
    public IActionResult EditarAnotacao(int id, [FromBody] EdicaoAnotacaoRequest request)
    {
        try
        {
            _prontuarioService.EditarAnotacao(id, request.NovoTexto, request.Motivo, User.ObterId());
            return Ok(new { mensagem = "Anotação editada e versionada com sucesso (RN17)." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // GET: api/prontuarios/paciente/{pacienteId}
    [HttpGet("paciente/{pacienteId}")]
    public IActionResult ConsultarHistorico(int pacienteId)
    {
        try
        {
            var historico = _prontuarioService.ConsultarHistorico(pacienteId, User.ObterId());
            return Ok(historico);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}

