using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamesLaboratorioController : ControllerBase
{
    private readonly IExameLaboratorioRepository _exameRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public ExamesLaboratorioController(
        IExameLaboratorioRepository exameRepository,
        IPacienteRepository pacienteRepository)
    {
        _exameRepository = exameRepository;
        _pacienteRepository = pacienteRepository;
    }

    public record NovoExameRequest(
        [Range(1, int.MaxValue)] int PacienteId,
        [Required(ErrorMessage = "Nome do exame é obrigatório."), StringLength(120)] string NomeExameAmostra,
        [Required(ErrorMessage = "Laboratório parceiro é obrigatório."), StringLength(100)] string LaboratorioParceiro,
        DateTime DataColeta,
        DateTime DataPrevisaoLaudo,
        string? CodigoRastreio = null,
        int? ProntuarioId = null,
        string? Observacoes = null);

    public record AtualizarStatusExameRequest(
        [EnumDataType(typeof(StatusAmostraLaboratorio))] StatusAmostraLaboratorio NovoStatus,
        string? Observacao = null);

    // GET: api/exameslaboratorio
    [HttpGet]
    public ActionResult<IEnumerable<PedidoExameLaboratorio>> ListarTodos([FromQuery] int? pacienteId = null)
    {
        if (pacienteId.HasValue)
        {
            return Ok(_exameRepository.ListarPorPaciente(pacienteId.Value));
        }

        return Ok(_exameRepository.ListarTodos());
    }

    // GET: api/exameslaboratorio/{id}
    [HttpGet("{id}")]
    public ActionResult<PedidoExameLaboratorio> ObterPorId(int id)
    {
        var exame = _exameRepository.ObterPorId(id);
        if (exame == null)
            return NotFound(new { mensagem = "Pedido de exame não encontrado." });

        return Ok(exame);
    }

    // POST: api/exameslaboratorio (RF34)
    [HttpPost]
    public IActionResult CadastrarExame([FromBody] NovoExameRequest request)
    {
        var paciente = _pacienteRepository.ObterPorId(request.PacienteId);
        if (paciente == null)
            return NotFound(new { erro = "Paciente não encontrado." });

        int novoId = _exameRepository.ListarTodos().Any()
            ? _exameRepository.ListarTodos().Max(e => e.Id) + 1
            : 1;

        var exame = new PedidoExameLaboratorio(
            id: novoId,
            pacienteId: request.PacienteId,
            nomePaciente: paciente.Nome,
            nomeExameAmostra: request.NomeExameAmostra.Trim(),
            laboratorioParceiro: request.LaboratorioParceiro.Trim(),
            dataColeta: request.DataColeta,
            dataPrevisaoLaudo: request.DataPrevisaoLaudo,
            prontuarioId: request.ProntuarioId,
            codigoRastreio: request.CodigoRastreio?.Trim(),
            statusAmostra: StatusAmostraLaboratorio.ColetadoNaClinica,
            observacoes: request.Observacoes?.Trim()
        );

        _exameRepository.Adicionar(exame);
        return CreatedAtAction(nameof(ObterPorId), new { id = exame.Id }, exame);
    }

    // PUT: api/exameslaboratorio/{id}/status (RF34, RN23)
    [HttpPut("{id}/status")]
    public IActionResult AtualizarStatus(int id, [FromBody] AtualizarStatusExameRequest request)
    {
        var exame = _exameRepository.ObterPorId(id);
        if (exame == null)
            return NotFound(new { mensagem = "Pedido de exame não encontrado." });

        exame.AtualizarStatus(request.NovoStatus, request.Observacao);
        _exameRepository.Atualizar(exame);

        return Ok(new
        {
            mensagem = "Status da amostra atualizado com sucesso.",
            exame
        });
    }
}
