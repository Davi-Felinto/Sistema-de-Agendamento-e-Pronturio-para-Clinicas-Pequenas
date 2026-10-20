using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IExameLaboratorioRepository
{
    void Adicionar(PedidoExameLaboratorio exame);
    void Atualizar(PedidoExameLaboratorio exame);
    PedidoExameLaboratorio? ObterPorId(int id);
    IEnumerable<PedidoExameLaboratorio> ListarTodos();
    IEnumerable<PedidoExameLaboratorio> ListarPorPaciente(int pacienteId);
}
