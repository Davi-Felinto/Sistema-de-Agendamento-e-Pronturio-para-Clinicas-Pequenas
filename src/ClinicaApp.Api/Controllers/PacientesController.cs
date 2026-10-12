using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using System.Collections.Generic;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PacientesController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly GeradorDeIds _geradorDeIds;

    // A Injeção de Dependência que configuramos no Program.cs entrega o repositório aqui
    public PacientesController(IPacienteRepository pacienteRepository, GeradorDeIds geradorDeIds)
    {
        _pacienteRepository = pacienteRepository;
        _geradorDeIds = geradorDeIds;
    }

    // DTO de entrada: o cliente só envia os campos permitidos (Id e Ativo são definidos pelo servidor)
    // e o [ApiController] devolve HTTP 400 automaticamente quando alguma validação falha.
    public record PacienteRequest(
        [Required(ErrorMessage = "Nome é obrigatório."), StringLength(120, MinimumLength = 3)] string Nome,
        [Required(ErrorMessage = "CPF é obrigatório."), RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "CPF deve ter 11 dígitos.")] string Cpf,
        [Required] DateTime DataNascimento,
        [Required(ErrorMessage = "Telefone é obrigatório."), RegularExpression(@"^\(?\d{2}\)?\s?\d{5}-?\d{4}$", ErrorMessage = "Telefone deve ter DDD + 9 dígitos (11 dígitos).")] string Telefone,
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "E-mail inválido."), StringLength(120)] string? Email,
        [StringLength(200)] string? Endereco,
        [StringLength(70, ErrorMessage = "Alergias: máximo de 70 caracteres.")] string? Alergias,
        [StringLength(70, ErrorMessage = "Condições preexistentes: máximo de 70 caracteres.")] string? CondicoesPreexistentes);

    private static string ApenasDigitos(string valor) => new(valor.Where(char.IsDigit).ToArray());

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
    public ActionResult<Paciente> CadastrarPaciente([FromBody] PacienteRequest request)
    {
        if (request.DataNascimento.Date > DateTime.Today)
            return BadRequest(new { erro = "Data de nascimento não pode ser futura." });

        // O CPF é guardado só com dígitos, então a comparação ignora a pontuação
        var cpf = ApenasDigitos(request.Cpf);
        var cpfJaCadastrado = _pacienteRepository.ListarTodos(apenasAtivos: false)
            .Any(p => ApenasDigitos(p.DocumentoIdentificacao) == cpf);
        if (cpfJaCadastrado)
            return Conflict(new { erro = "Já existe um paciente cadastrado com este CPF." }); // HTTP 409

        var paciente = new Paciente(
            id: _geradorDeIds.ProximoPaciente(),
            nome: request.Nome.Trim(),
            documentoIdentificacao: cpf,
            dataNascimento: request.DataNascimento.Date,
            telefone: ApenasDigitos(request.Telefone),
            email: request.Email?.Trim() ?? string.Empty,
            endereco: request.Endereco?.Trim() ?? string.Empty,
            alergias: request.Alergias?.Trim(),
            condicoesPreexistentes: request.CondicoesPreexistentes?.Trim());

        _pacienteRepository.Adicionar(paciente);

        // Retorna HTTP 201 (Created) e a URL para buscar o novo paciente criado
        return CreatedAtAction(nameof(ObterPorId), new { id = paciente.Id }, paciente);
    }
}
