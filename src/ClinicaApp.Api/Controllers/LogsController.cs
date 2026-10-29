using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // RF28, RN12, RQ07: Consulta autenticada à trilha de auditoria imutável
public class LogsController : ControllerBase
{
    private readonly ILogAcessoRepository _logAcessoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public LogsController(ILogAcessoRepository logAcessoRepository, IUsuarioRepository usuarioRepository)
    {
        _logAcessoRepository = logAcessoRepository;
        _usuarioRepository = usuarioRepository;
    }

    // GET: api/logs
    [HttpGet]
    public IActionResult ObterLogs()
    {
        var usuarios = _usuarioRepository.ListarTodos()
            .GroupBy(u => u.Id)
            .ToDictionary(g => g.Key, g => g.First());

        var logs = _logAcessoRepository.ObterTodos().Select(l =>
        {
            usuarios.TryGetValue(l.UsuarioId, out var u);
            return new
            {
                id = l.Id,
                usuarioId = l.UsuarioId,
                usuarioNome = u?.Nome ?? $"Usuário #{l.UsuarioId}",
                usuarioEmail = u?.Login ?? string.Empty,
                operacao = l.Operacao,
                detalhes = l.Detalhes,
                ipOrigem = l.IpOrigem,
                dataHora = l.DataHora
            };
        });

        return Ok(logs);
    }
}
