using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryPagamentoRepository : IPagamentoRepository
{
    private readonly List<Pagamento> _pagamentos = new();

    public Pagamento? ObterPorId(int id)
    {
        return _pagamentos.FirstOrDefault(p => p.Id == id);
    }

    public Pagamento? ObterPorAgendamentoId(int agendamentoId)
    {
        return _pagamentos.FirstOrDefault(p => p.AgendamentoId == agendamentoId);
    }

    public IEnumerable<Pagamento> ListarTodos()
    {
        return _pagamentos.ToList();
    }

    public void Adicionar(Pagamento pagamento)
    {
        _pagamentos.Add(pagamento);
    }

    public void Atualizar(Pagamento pagamento)
    {
        var index = _pagamentos.FindIndex(p => p.Id == pagamento.Id);
        if (index >= 0)
        {
            _pagamentos[index] = pagamento;
        }
    }
}
