using System;
using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IAgendamentoRepository{
    Agendamento? ObterPorId(int id);
    IEnumerable<Agendamento> ObterPorProfissionalEPeriodo(int profissionalId, DateTime data);
    IEnumerable<Agendamento> ObterPorPaciente(int pacienteId);
    IEnumerable<Agendamento> ListarTodos();
    void Adicionar(Agendamento agendamento);
    void Atualizar(Agendamento agendamento);
}