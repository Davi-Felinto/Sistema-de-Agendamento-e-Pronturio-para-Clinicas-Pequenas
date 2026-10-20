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
public class AvaliacoesController : ControllerBase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public AvaliacoesController(
        IAvaliacaoRepository avaliacaoRepository,
        IAgendamentoRepository agendamentoRepository,
        IPacienteRepository pacienteRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _agendamentoRepository = agendamentoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public record NovaAvaliacaoRequest(
        [Range(1, int.MaxValue)] int AgendamentoId,
        [Range(1, 5, ErrorMessage = "A nota da recepção deve ser de 1 a 5.")] int NotaRecepcao,
        [Range(1, 5, ErrorMessage = "A nota clínica deve ser de 1 a 5.")] int NotaAtendimentoClinico,
        [Range(1, 5, ErrorMessage = "A nota do espaço físico deve ser de 1 a 5.")] int NotaEspacoFisico,
        [StringLength(500, ErrorMessage = "Comentário: máximo de 500 caracteres.")] string? ComentarioOuvidoria = null,
        bool Anonimo = false);

    // GET: api/avaliacoes
    [HttpGet]
    public ActionResult<object> ListarTodas()
    {
        var avaliacoes = _avaliacaoRepository.ListarTodas().ToList();

        double mediaGeral = avaliacoes.Any() ? Math.Round(avaliacoes.Average(a => a.MediaGeral), 1) : 5.0;
        double mediaRecepcao = avaliacoes.Any() ? Math.Round(avaliacoes.Average(a => a.NotaRecepcao), 1) : 5.0;
        double mediaClinica = avaliacoes.Any() ? Math.Round(avaliacoes.Average(a => a.NotaAtendimentoClinico), 1) : 5.0;
        double mediaEspaco = avaliacoes.Any() ? Math.Round(avaliacoes.Average(a => a.NotaEspacoFisico), 1) : 5.0;

        return Ok(new
        {
            resumo = new
            {
                totalAvaliacoes = avaliacoes.Count,
                mediaGeral,
                mediaRecepcao,
                mediaClinica,
                mediaEspaco
            },
            avaliacoes
        });
    }

    // POST: api/avaliacoes (RF32, RN21)
    [HttpPost]
    public IActionResult RegistrarAvaliacao([FromBody] NovaAvaliacaoRequest request)
    {
        var agendamento = _agendamentoRepository.ObterPorId(request.AgendamentoId);
        if (agendamento == null)
            return NotFound(new { erro = "Agendamento não encontrado." });

        // RN21: Unicidade de avaliação por atendimento
        var existente = _avaliacaoRepository.ObterPorAgendamentoId(request.AgendamentoId);
        if (existente != null)
            return Conflict(new { erro = "Este atendimento já possui uma avaliação registrada." });

        var paciente = _pacienteRepository.ObterPorId(agendamento.PacienteId);
        string nomePaciente = paciente?.Nome ?? "Paciente";

        int novoId = _avaliacaoRepository.ListarTodas().Any()
            ? _avaliacaoRepository.ListarTodas().Max(a => a.Id) + 1
            : 1;

        var avaliacao = new AvaliacaoAtendimento(
            id: novoId,
            agendamentoId: request.AgendamentoId,
            pacienteId: agendamento.PacienteId,
            nomePaciente: nomePaciente,
            dataAvaliacao: DateTime.Now,
            notaRecepcao: request.NotaRecepcao,
            notaAtendimentoClinico: request.NotaAtendimentoClinico,
            notaEspacoFisico: request.NotaEspacoFisico,
            comentarioOuvidoria: request.ComentarioOuvidoria,
            anonimo: request.Anonimo
        );

        _avaliacaoRepository.Adicionar(avaliacao);
        return CreatedAtAction(nameof(ListarTodas), new { id = avaliacao.Id }, avaliacao);
    }
}
