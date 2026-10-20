using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IAvaliacaoRepository
{
    void Adicionar(AvaliacaoAtendimento avaliacao);
    AvaliacaoAtendimento? ObterPorId(int id);
    AvaliacaoAtendimento? ObterPorAgendamentoId(int agendamentoId);
    IEnumerable<AvaliacaoAtendimento> ListarTodas();
}
