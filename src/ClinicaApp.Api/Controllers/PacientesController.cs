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
public class PacientesController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacientesController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public record PacienteRequest(
        [Required(ErrorMessage = "Nome é obrigatório."), StringLength(120, MinimumLength = 3)] string Nome,
        [Required(ErrorMessage = "CPF é obrigatório."), RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "CPF deve ter 11 dígitos.")] string DocumentoIdentificacao,
        [Required] DateTime DataNascimento,
        [Required(ErrorMessage = "Telefone é obrigatório."), RegularExpression(@"^\(?\d{2}\)?\s?\d{5}-?\d{4}$", ErrorMessage = "Telefone deve ter DDD + 9 dígitos (11 dígitos).")] string Telefone,
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "E-mail inválido."), StringLength(120)] string? Email,
        [StringLength(200)] string? Endereco,
        [StringLength(70, ErrorMessage = "Alergias: máximo de 70 caracteres.")] string? Alergias,
        [StringLength(70, ErrorMessage = "Condições preexistentes: máximo de 70 caracteres.")] string? CondicoesPreexistentes);

    private static string ApenasDigitos(string valor) => new(valor.Where(char.IsDigit).ToArray());

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
    public ActionResult<Paciente> CadastrarPaciente([FromBody] PacienteRequest request)
    {
        if (request.DataNascimento.Date > DateTime.Today)
            return BadRequest(new { erro = "Data de nascimento não pode ser futura." });

        var cpf = ApenasDigitos(request.DocumentoIdentificacao);
        var cpfJaCadastrado = _pacienteRepository.ListarTodos(apenasAtivos: false)
            .Any(p => ApenasDigitos(p.DocumentoIdentificacao) == cpf);
        if (cpfJaCadastrado)
            return Conflict(new { erro = "Já existe um paciente cadastrado com este CPF." });

        int novoId = _pacienteRepository.ListarTodos(false).Any()
            ? _pacienteRepository.ListarTodos(false).Max(p => p.Id) + 1
            : 1;

        var paciente = new Paciente(
            id: novoId,
            nome: request.Nome.Trim(),
            documentoIdentificacao: cpf,
            dataNascimento: request.DataNascimento.Date,
            telefone: ApenasDigitos(request.Telefone),
            email: request.Email?.Trim() ?? string.Empty,
            endereco: request.Endereco?.Trim() ?? string.Empty,
            alergias: request.Alergias?.Trim(),
            condicoesPreexistentes: request.CondicoesPreexistentes?.Trim()
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
