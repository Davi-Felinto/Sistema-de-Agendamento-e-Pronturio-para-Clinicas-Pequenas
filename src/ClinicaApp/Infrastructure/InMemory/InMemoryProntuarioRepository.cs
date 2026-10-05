using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryProntuarioRepository : IProntuarioRepository
{
    private readonly List<SessaoProntuario> _sessoes = new();

    public SessaoProntuario? ObterPorId(int id)
    {
        return _sessoes.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<SessaoProntuario> ObterPorPaciente(int pacienteId)
    {
        return _sessoes.Where(s => s.PacienteId == pacienteId).ToList();
    }

    public void Adicionar(SessaoProntuario sessao)
    {
        _sessoes.Add(sessao);
    }

    public void Atualizar(SessaoProntuario sessao)
    {
        var index = _sessoes.FindIndex(s => s.Id == sessao.Id);
        if (index >= 0)
        {
            _sessoes[index] = sessao;
        }
    }
}
