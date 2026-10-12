using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    public record LoginRequest(
        [Required, StringLength(100)] string Login,
        [Required, StringLength(200)] string Senha);

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

        // RF26: o token deve ser enviado nas próximas requisições no cabeçalho
        // "Authorization: Bearer <token>"
        return Ok(new {
            id = usuario.Id,
            nome = usuario.Nome,
            perfil = usuario.Perfil.ToString(),
            token = _tokenService.GerarToken(usuario)
        });
    }
}
