using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(PerfilUsuario.Administrador))] // RN12: só o Administrador audita os logs
public class LogsController : ControllerBase
{
    private readonly ILogAcessoRepository _logAcessoRepository;

    public LogsController(ILogAcessoRepository logAcessoRepository)
    {
        _logAcessoRepository = logAcessoRepository;
    }

    // GET: api/logs
    [HttpGet]
    public IActionResult ObterLogs()
    {
        var logs = _logAcessoRepository.ObterTodos();
        return Ok(logs);
    }
}
