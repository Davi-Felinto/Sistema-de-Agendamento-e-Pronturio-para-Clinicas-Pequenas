using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryPacienteRepository : IPacienteRepository
{
    private readonly List<Paciente> _pacientes = new();

    public Paciente? ObterPorId(int id)
    {
        return _pacientes.FirstOrDefault(p => p.Id == id);
    }

    public Paciente? ObterPorDocumento(string documento)
    {
        return _pacientes.FirstOrDefault(p => p.DocumentoIdentificacao == documento);
    }

    public IEnumerable<Paciente> ListarTodos(bool apenasAtivos = true)
    {
        return apenasAtivos
            ? _pacientes.Where(p => p.Ativo).ToList()
            : _pacientes.ToList();
    }

    public void Adicionar(Paciente paciente)
    {
        _pacientes.Add(paciente);
    }

    public void Atualizar(Paciente paciente)
    {
        var index = _pacientes.FindIndex(p => p.Id == paciente.Id);
        if (index >= 0)
        {
            _pacientes[index] = paciente;
        }
    }
}
