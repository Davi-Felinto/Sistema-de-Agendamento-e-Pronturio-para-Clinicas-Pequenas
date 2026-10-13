using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Services;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgendamentosController : ControllerBase
{
    private readonly AgendaService _agendaService;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentosController(
        AgendaService agendaService, 
        IAgendamentoRepository agendamentoRepository)
    {
        _agendaService = agendaService;
        _agendamentoRepository = agendamentoRepository;
    }

    public record AgendamentoRequest(
        [Range(1, int.MaxValue)] int PacienteId,
        int ProfissionalId,
        DateTime DataHoraInicio,
        DateTime DataHoraFim,
        [StringLength(500)] string? Observacoes);

    // GET: api/agendamentos
    [HttpGet]
    public IActionResult ListarTodos()
    {
        var agendamentos = _agendamentoRepository.ListarTodos();

        // RN12: Profissional vê apenas os próprios agendamentos; Administrador vê todos
        if (!User.EhAdministrador())
        {
            var usuarioId = User.ObterId();
            agendamentos = agendamentos.Where(a => a.ProfissionalId == usuarioId);
        }

        return Ok(agendamentos);
    }

    // POST: api/agendamentos
    [HttpPost]
    public IActionResult Agendar([FromBody] AgendamentoRequest request)
    {
        // RN12: Profissional só agenda na própria agenda. O ProfissionalId do corpo
        // só é considerado quando quem agenda é o Administrador.
        int profissionalId = User.EhAdministrador() ? request.ProfissionalId : User.ObterId();
        if (profissionalId <= 0)
            return BadRequest(new { erro = "Informe o profissional da consulta." });

        int novoId = _agendamentoRepository.ListarTodos().Any()
            ? _agendamentoRepository.ListarTodos().Max(a => a.Id) + 1
            : 1;

        var consulta = _agendaService.AgendarConsulta(
            id: novoId,
            pacienteId: request.PacienteId,
            profissionalId: profissionalId,
            dataHoraInicio: request.DataHoraInicio,
            dataHoraFim: request.DataHoraFim,
            observacoes: request.Observacoes
        );

        return CreatedAtAction(nameof(ListarTodos), new { id = consulta.Id }, consulta);
    }

    // PUT: api/agendamentos/{id}/cancelar
    [HttpPut("{id}/cancelar")]
    public IActionResult Cancelar(int id)
    {
        // RN12: Profissional só cancela consultas da própria agenda
        var existente = _agendamentoRepository.ObterPorId(id);
        if (existente != null && !User.EhAdministrador() && existente.ProfissionalId != User.ObterId())
            return Forbid();

        // RN10: O service avalia se o cancelamento foi feito com menos de 24h
        _agendaService.CancelarConsulta(id, DateTime.Now);
        var agendamento = _agendamentoRepository.ObterPorId(id);
        return Ok(new { 
            mensagem = "Consulta cancelada com sucesso.",
            cancelamentoTardio = agendamento?.CancelamentoTardio ?? false
        });
    }
}
