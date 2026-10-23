using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthController(AuthService authService, TokenService tokenService, IUsuarioRepository usuarioRepository)
    {
        _authService = authService;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
    }

    public record LoginRequest(
        [Required, StringLength(100)] string Login,
        [Required, StringLength(200)] string Senha);

    public record RegistroRequest(
        [Required, StringLength(100)] string Nome,
        [Required, EmailAddress, StringLength(100)] string Email,
        [Required, MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres."), StringLength(200)] string Senha,
        [Required] string Perfil, // "Profissional" ou "Administrador"
        [StringLength(100)] string? NomeClinica,
        [StringLength(50)] string? RegistroProfissional,
        [StringLength(50)] string? Especialidade);

    // POST: api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("login")] // Limita tentativas por IP para dificultar força bruta
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var usuario = _authService.Autenticar(request.Login, request.Senha);
        if (usuario == null)
        {
            return Unauthorized(new { erro = "Login ou senha inválidos." });
        }

        // RF26: Emissão do token JWT com claims de identidade e perfil
        return Ok(new {
            id = usuario.Id,
            nome = usuario.Nome,
            perfil = usuario.Perfil.ToString(),
            email = usuario.Login,
            token = _tokenService.GerarToken(usuario)
        });
    }

    // POST: api/auth/registrar
    [HttpPost("registrar")]
    [AllowAnonymous]
    [EnableRateLimiting("login")]
    public IActionResult Registrar([FromBody] RegistroRequest request)
    {
        var loginNormalizado = request.Email.Trim().ToLowerInvariant();
        if (_usuarioRepository.ObterPorLogin(loginNormalizado) != null)
        {
            return Conflict(new { erro = "Já existe uma conta cadastrada com este e-mail." });
        }

        var todos = _usuarioRepository.ListarTodos();
        int novoId = todos.Any() ? todos.Max(u => u.Id) + 1 : 1;

        Usuario novoUsuario;
        bool ehProfissional = string.Equals(request.Perfil, "Profissional", StringComparison.OrdinalIgnoreCase);

        if (ehProfissional)
        {
            novoUsuario = new ProfissionalSaude(
                novoId,
                request.Nome.Trim(),
                loginNormalizado,
                request.Senha,
                !string.IsNullOrWhiteSpace(request.RegistroProfissional) ? request.RegistroProfissional.Trim() : "CRM-BR",
                !string.IsNullOrWhiteSpace(request.Especialidade) ? request.Especialidade.Trim() : "Clínica Geral"
            );
        }
        else
        {
            string cargo = !string.IsNullOrWhiteSpace(request.NomeClinica)
                ? $"Gestão - {request.NomeClinica.Trim()}"
                : "Recepção e Atendimento";

            novoUsuario = new Administrador(
                novoId,
                request.Nome.Trim(),
                loginNormalizado,
                request.Senha,
                cargo
            );
        }

        _authService.Cadastrar(novoUsuario);

        return StatusCode(StatusCodes.Status201Created, new {
            id = novoUsuario.Id,
            nome = novoUsuario.Nome,
            perfil = novoUsuario.Perfil.ToString(),
            email = novoUsuario.Login,
            token = _tokenService.GerarToken(novoUsuario)
        });
    }
}

