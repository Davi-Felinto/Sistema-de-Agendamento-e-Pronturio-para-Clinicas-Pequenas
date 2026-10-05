using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IPagamentoRepository
{
    Pagamento? ObterPorId(int id);
    Pagamento? ObterPorAgendamentoId(int agendamentoId);
    IEnumerable<Pagamento> ListarTodos();
    void Adicionar(Pagamento pagamento);
    void Atualizar(Pagamento pagamento);
}
