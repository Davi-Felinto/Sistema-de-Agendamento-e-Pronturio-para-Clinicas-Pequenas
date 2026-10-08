using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    public record LoginRequest(string Login, string Senha);

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var usuario = _authService.Autenticar(request.Login, request.Senha);
        if (usuario == null)
        {
            return Unauthorized(new { erro = "Login ou senha inválidos." });
        }

        // Em uma API real, devolveríamos um token JWT aqui.
        // No MVP, vamos apenas devolver os dados do usuário.
        return Ok(new {
            id = usuario.Id,
            nome = usuario.Nome,
            perfil = usuario.Perfil.ToString()
        });
    }
}

