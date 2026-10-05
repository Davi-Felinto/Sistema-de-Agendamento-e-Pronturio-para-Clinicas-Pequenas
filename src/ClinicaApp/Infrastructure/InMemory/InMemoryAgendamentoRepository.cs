using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryAgendamentoRepository : IAgendamentoRepository
{
    private readonly List<Agendamento> _agendamentos = new();

    public Agendamento? ObterPorId(int id)
    {
        return _agendamentos.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Agendamento> ObterPorProfissionalEPeriodo(int profissionalId, DateTime data)
    {
        return _agendamentos.Where(a => a.ProfissionalId == profissionalId && a.DataHoraInicio.Date == data.Date).ToList();
    }

    public IEnumerable<Agendamento> ObterPorPaciente(int pacienteId)
    {
        return _agendamentos.Where(a => a.PacienteId == pacienteId).ToList();
    }

    public IEnumerable<Agendamento> ListarTodos()
    {
        return _agendamentos.ToList();
    }

    public void Adicionar(Agendamento agendamento)
    {
        _agendamentos.Add(agendamento);
    }

    public void Atualizar(Agendamento agendamento)
    {
        var index = _agendamentos.FindIndex(a => a.Id == agendamento.Id);
        if (index >= 0)
        {
            _agendamentos[index] = agendamento;
        }
    }
}
