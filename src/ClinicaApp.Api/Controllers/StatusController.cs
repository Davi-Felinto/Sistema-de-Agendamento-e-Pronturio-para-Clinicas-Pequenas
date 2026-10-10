using Microsoft.AspNetCore.Mvc;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Infrastructure.MySQL;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    public StatusController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    [HttpGet]
    public IActionResult ObterStatus()
    {
        bool isMySql = _pacienteRepository is MySqlPacienteRepository;

        return Ok(new
        {
            online = true,
            storage = isMySql ? "mysql" : "memoria",
            storageLabel = isMySql ? "Banco: MySQL 8.0" : "Banco: Memória (Mock)"
        });
    }
}
