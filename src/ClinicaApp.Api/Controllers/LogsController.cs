using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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

