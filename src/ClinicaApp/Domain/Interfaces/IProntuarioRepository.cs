using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IProntuarioRepository{
    SessaoProntuario? ObterPorId(int id);
    IEnumerable<SessaoProntuario> ObterPorPaciente(int pacienteId);
    void Adicionar(SessaoProntuario sessao);
    void Atualizar(SessaoProntuario sessao);
}