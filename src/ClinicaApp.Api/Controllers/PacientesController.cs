using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using System.Collections.Generic;

using System;
using System.Linq;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacientesController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public record NovoPacienteRequest(
        string Nome,
        string DocumentoIdentificacao,
        DateTime DataNascimento,
        string Telefone,
        string Email,
        string Endereco,
        string? Alergias = null,
        string? CondicoesPreexistentes = null);

    // GET: api/pacientes
    [HttpGet]
    public ActionResult<IEnumerable<Paciente>> ListarTodos([FromQuery] bool apenasAtivos = false)
    {
        var pacientes = _pacienteRepository.ListarTodos(apenasAtivos);
        return Ok(pacientes);
    }

    // GET: api/pacientes/5
    [HttpGet("{id}")]
    public ActionResult<Paciente> ObterPorId(int id)
    {
        var paciente = _pacienteRepository.ObterPorId(id);
        
        if (paciente == null)
            return NotFound(new { mensagem = "Paciente não encontrado." });

        return Ok(paciente);
    }

    // POST: api/pacientes
    [HttpPost]
    public ActionResult<Paciente> CadastrarPaciente([FromBody] NovoPacienteRequest request)
    {
        int novoId = _pacienteRepository.ListarTodos(false).Any()
            ? _pacienteRepository.ListarTodos(false).Max(p => p.Id) + 1
            : 1;

        var paciente = new Paciente(
            id: novoId,
            nome: request.Nome,
            documentoIdentificacao: request.DocumentoIdentificacao,
            dataNascimento: request.DataNascimento,
            telefone: request.Telefone,
            email: request.Email,
            endereco: request.Endereco,
            alergias: request.Alergias,
            condicoesPreexistentes: request.CondicoesPreexistentes
        );

        _pacienteRepository.Adicionar(paciente);
        return CreatedAtAction(nameof(ObterPorId), new { id = paciente.Id }, paciente);
    }

    // PUT: api/pacientes/{id}/inativar (RN16, RQ03 - LGPD)
    [HttpPut("{id}/inativar")]
    public IActionResult InativarPaciente(int id)
    {
        var paciente = _pacienteRepository.ObterPorId(id);
        if (paciente == null)
            return NotFound(new { mensagem = "Paciente não encontrado." });

        paciente.InativarComAnonimizacao();
        _pacienteRepository.Atualizar(paciente);
        return Ok(new { mensagem = "Paciente inativado e anonimizado conforme LGPD (RN16, RQ03)." });
    }
}

