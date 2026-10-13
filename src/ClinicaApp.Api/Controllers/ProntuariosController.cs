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

    public ProntuariosController(ProntuarioService prontuarioService)
    {
        _prontuarioService = prontuarioService;
    }

    // RN13: O autor da operação vem sempre do token (User), nunca do corpo da requisição
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
        int novoId = new Random().Next(1000, 100000);

        var sessao = _prontuarioService.RegistrarSessao(
            id: novoId,
            pacienteId: request.PacienteId,
            agendamentoId: request.AgendamentoId,
            anotacoesClinicas: request.AnotacoesClinicas,
            usuarioId: User.ObterId()
        );

        return Ok(sessao);
    }

    // PUT: api/prontuarios/{id}
    [HttpPut("{id}")]
    public IActionResult EditarAnotacao(int id, [FromBody] EdicaoAnotacaoRequest request)
    {
        _prontuarioService.EditarAnotacao(id, request.NovoTexto, request.Motivo, User.ObterId());
        return Ok(new { mensagem = "Anotação editada e versionada com sucesso (RN17)." });
    }

    // GET: api/prontuarios/paciente/{pacienteId}
    [HttpGet("paciente/{pacienteId}")]
    public IActionResult ConsultarHistorico(int pacienteId)
    {
        var historico = _prontuarioService.ConsultarHistorico(pacienteId, User.ObterId());
        return Ok(historico);
    }
}
