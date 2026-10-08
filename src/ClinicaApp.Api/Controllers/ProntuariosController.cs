using System;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProntuariosController : ControllerBase
{
    private readonly ProntuarioService _prontuarioService;

    public ProntuariosController(ProntuarioService prontuarioService)
    {
        _prontuarioService = prontuarioService;
    }

    public record RegistroSessaoRequest(int PacienteId, int AgendamentoId, string AnotacoesClinicas, int UsuarioId);
    public record EdicaoAnotacaoRequest(string NovoTexto, string Motivo, int UsuarioId);

    // POST: api/prontuarios
    [HttpPost]
    public IActionResult RegistrarSessao([FromBody] RegistroSessaoRequest request)
    {
        try
        {
            // Simulando ID autoincremento para o MVP
            int novoId = new Random().Next(100, 10000);

            var sessao = _prontuarioService.RegistrarSessao(
                id: novoId,
                pacienteId: request.PacienteId,
                agendamentoId: request.AgendamentoId,
                anotacoesClinicas: request.AnotacoesClinicas,
                usuarioId: request.UsuarioId
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
            _prontuarioService.EditarAnotacao(id, request.NovoTexto, request.Motivo, request.UsuarioId);
            return Ok(new { mensagem = "Anotação editada e versionada com sucesso (RN17)." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // GET: api/prontuarios/paciente/{pacienteId}?usuarioId=1
    [HttpGet("paciente/{pacienteId}")]
    public IActionResult ConsultarHistorico(int pacienteId, [FromQuery] int usuarioId)
    {
        try
        {
            var historico = _prontuarioService.ConsultarHistorico(pacienteId, usuarioId);
            return Ok(historico);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}

