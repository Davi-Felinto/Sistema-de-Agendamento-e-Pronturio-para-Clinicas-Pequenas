using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryExameLaboratorioRepository : IExameLaboratorioRepository
{
    private readonly ConcurrentDictionary<int, PedidoExameLaboratorio> _exames = new();

    public void Adicionar(PedidoExameLaboratorio exame)
    {
        _exames[exame.Id] = exame;
    }

    public void Atualizar(PedidoExameLaboratorio exame)
    {
        _exames[exame.Id] = exame;
    }

    public PedidoExameLaboratorio? ObterPorId(int id)
    {
        _exames.TryGetValue(id, out var exame);
        return exame;
    }

    public IEnumerable<PedidoExameLaboratorio> ListarTodos()
    {
        return _exames.Values.OrderByDescending(e => e.DataColeta).ToList();
    }

    public IEnumerable<PedidoExameLaboratorio> ListarPorPaciente(int pacienteId)
    {
        return _exames.Values.Where(e => e.PacienteId == pacienteId).OrderByDescending(e => e.DataColeta).ToList();
    }
}
