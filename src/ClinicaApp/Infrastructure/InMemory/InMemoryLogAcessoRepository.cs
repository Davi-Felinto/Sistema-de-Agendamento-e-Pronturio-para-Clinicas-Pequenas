using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryLogAcessoRepository : ILogAcessoRepository
{
    private readonly List<LogAcesso> _logs = new();

    public void Salvar(LogAcesso log)
    {
        _logs.Add(log);
    }

    public IEnumerable<LogAcesso> ObterTodos()
    {
        return _logs.ToList();
    }
}
