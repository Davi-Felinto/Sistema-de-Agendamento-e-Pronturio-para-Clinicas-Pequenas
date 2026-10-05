using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface ILogAcessoRepository
{
    void Salvar(LogAcesso log);
    IEnumerable<LogAcesso> ObterTodos();
}
