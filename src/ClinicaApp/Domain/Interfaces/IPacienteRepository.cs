using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IPacienteRepository{
    Paciente? ObterPorId(int id);
    Paciente? ObterPorDocumento(string documento);
    IEnumerable<Paciente> ListarTodos(bool apenasAtivos = true);
    void Adicionar(Paciente paciente);
    void Atualizar(Paciente paciente);
}