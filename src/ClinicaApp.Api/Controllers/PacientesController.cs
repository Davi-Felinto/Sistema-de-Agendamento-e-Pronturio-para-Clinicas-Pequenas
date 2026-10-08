using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using System.Collections.Generic;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    // A Injeção de Dependência que configuramos no Program.cs entrega o repositório aqui
    public PacientesController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    // GET: api/pacientes
    [HttpGet]
    public ActionResult<IEnumerable<Paciente>> ListarTodos()
    {
        var pacientes = _pacienteRepository.ListarTodos();
        return Ok(pacientes); // Retorna HTTP 200 (OK) com a lista em formato JSON
    }

    // GET: api/pacientes/5
    [HttpGet("{id}")]
    public ActionResult<Paciente> ObterPorId(int id)
    {
        var paciente = _pacienteRepository.ObterPorId(id);
        
        if (paciente == null)
            return NotFound(new { mensagem = "Paciente não encontrado." }); // HTTP 404

        return Ok(paciente);
    }

    // POST: api/pacientes
    [HttpPost]
    public ActionResult<Paciente> CadastrarPaciente([FromBody] Paciente paciente)
    {
        // Se quiséssemos regras de negócio complexas, passaríamos para um PacienteService.
        // Como é um CRUD simples, usamos o repositório diretamente.
        _pacienteRepository.Adicionar(paciente);

        // Retorna HTTP 201 (Created) e a URL para buscar o novo paciente criado
        return CreatedAtAction(nameof(ObterPorId), new { id = paciente.Id }, paciente);
    }
}

