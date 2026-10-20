using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryAvaliacaoRepository : IAvaliacaoRepository
{
    private readonly ConcurrentDictionary<int, AvaliacaoAtendimento> _avaliacoes = new();

    public void Adicionar(AvaliacaoAtendimento avaliacao)
    {
        _avaliacoes[avaliacao.Id] = avaliacao;
    }

    public AvaliacaoAtendimento? ObterPorId(int id)
    {
        _avaliacoes.TryGetValue(id, out var avaliacao);
        return avaliacao;
    }

    public AvaliacaoAtendimento? ObterPorAgendamentoId(int agendamentoId)
    {
        return _avaliacoes.Values.FirstOrDefault(a => a.AgendamentoId == agendamentoId);
    }

    public IEnumerable<AvaliacaoAtendimento> ListarTodas()
    {
        return _avaliacoes.Values.OrderByDescending(a => a.DataAvaliacao).ToList();
    }
}
