using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Services;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentosController : ControllerBase
{
    private readonly AgendaService _agendaService;
    private readonly IAgendamentoRepository _agendamentoRepository;

    // Pedimos ao container o Service (regras de negócio) e o Repository (para listagem)
    public AgendamentosController(
        AgendaService agendaService, 
        IAgendamentoRepository agendamentoRepository
        )
    {
        _agendaService = agendaService;
        _agendamentoRepository = agendamentoRepository;
    }

    // 1. DTO (Data Transfer Object) - Um objeto simples apenas para receber os dados do JSON
    public record AgendamentoRequest(
        int PacienteId,
        int ProfissionalId,
        DateTime DataHoraInicio,
        DateTime DataHoraFim,
        string Observacoes);

    // GET: api/agendamentos
    [HttpGet]
    public IActionResult ListarTodos()
    {
        var agendamento = _agendamentoRepository.ListarTodos();
        return Ok(agendamento);
    }

    // POST: api/agendamentos
    [HttpPost]
    public IActionResult Agendar([FromBody] AgendamentoRequest request)
    {
        try
        {
            // Como nossos repositórios InMemory não tem "Auto Increment" de ID nativo do MySQL, 
            // calculamos o próximo ID baseado no maior existente.
            int novoId = _agendamentoRepository.ListarTodos().Any()
                ? _agendamentoRepository.ListarTodos().Max(a => a.Id) + 1
                : 1;

            // 2. Chamamos o Service, que é o Guardião das Regras de Negócio (RN01, RN02)
            var consulta = _agendaService.AgendarConsulta(
                id: novoId,
                pacienteId: request.PacienteId,
                profissionalId: request.ProfissionalId,
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
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/agendamentos/{id}/cancelar
    [HttpPut("{id}/cancelar")]
    public IActionResult Cancelar(int id)
    {
        try
        {
            // RN10: O service avalia se o cancelamento foi feito com menos de 24h
            _agendaService.CancelarConsulta(id, DateTime.Now);
            var agendamento = _agendamentoRepository.ObterPorId(id);
            return Ok(new { 
                mensagem = "Consulta cancelada com sucesso.",
                cancelamentoTardio = agendamento?.CancelamentoTardio ?? false
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}
