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
    private readonly GeradorDeIds _geradorDeIds;

    // Pedimos ao container o Service (regras de negócio) e o Repository (para listagem)
    public AgendamentosController(
        AgendaService agendaService, 
        IAgendamentoRepository agendamentoRepository,
        GeradorDeIds geradorDeIds
        )
    {
        _agendaService = agendaService;
        _agendamentoRepository = agendamentoRepository;
        _geradorDeIds = geradorDeIds;
    }

    // 1. DTO (Data Transfer Object) - Um objeto simples apenas para receber os dados do JSON
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
        try
        {
            // RN12: Profissional só agenda na própria agenda. O ProfissionalId do corpo
            // só é considerado quando quem agenda é o Administrador.
            int profissionalId = User.EhAdministrador() ? request.ProfissionalId : User.ObterId();
            if (profissionalId <= 0)
                return BadRequest(new { erro = "Informe o profissional da consulta." });

            // 2. Chamamos o Service, que é o Guardião das Regras de Negócio (RN01, RN02)
            var consulta = _agendaService.AgendarConsulta(
                id: _geradorDeIds.ProximoAgendamento(),
                pacienteId: request.PacienteId,
                profissionalId: profissionalId,
                dataHoraInicio: request.DataHoraInicio,
                dataHoraFim: request.DataHoraFim,
                observacoes: request.Observacoes
                );

            return CreatedAtAction(nameof(ListarTodos), new { id = consulta.Id }, consulta);
        }

        // 3. Capturando Exceções de Regra de Negócio
        catch(InvalidOperationException ex)
        {
            // Se cair aqui, é porque o Service barrou o agendamento (Ex: Conflito de Horário ou Paciente inativo)
            // Retornamos HTTP 400 (Bad Request) com a mensagem exata do problema
            return BadRequest(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            // Ex: Se passou uma data no passado (O AgendaService barra isso)
            return BadRequest(new { erro  = ex.Message });
        }
    }
}
