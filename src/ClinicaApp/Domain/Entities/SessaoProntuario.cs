using System;
using System.Collections.Generic;

namespace ClinicaApp.Domain.Entities;

public class SessaoProntuario{
    public int Id { get; private set; }
    public int PacienteId { get; private set; }
    public int AgendamentoId { get; private set; }
    public DateTime DataRegistro { get; private set; }
    public string AnotacoesClinicas { get; private set; }

    // Lista privada para garantir encapsulamento:
    private readonly List<VersaoAnotacao> _historicoVersoes = new();
    public IReadOnlyCollection<VersaoAnotacao> HistoricoVersoes => _historicoVersoes.AsReadOnly();

    public SessaoProntuario(
        int id,
        int pacienteId,
        int agendamentoId,
        string anotacoesClinicas,
        DateTime? dataRegistro = null)
    {
        if (string.IsNullOrWhiteSpace(anotacoesClinicas))
            throw new ArgumentException("As anotações clínicas são obrigatórias.", nameof(anotacoesClinicas));

        Id = id;
        PacienteId = pacienteId;
        AgendamentoId = agendamentoId;
        AnotacoesClinicas = anotacoesClinicas;
        DataRegistro = dataRegistro ?? DateTime.Now;
    }

    /// <summary>
    /// RF16 e RN17: Toda alteração em prontuário gera uma versão arquivada
    /// imutável contendo o snapshot anterior, timestamp e o motivo da edição.
    /// </summary>
    public void AlterarAnotacao(string novoTexto, string motivo)
    {
        if (string.IsNullOrWhiteSpace(novoTexto))
            throw new ArgumentException("O novo texto da anotação é obrigatório.", nameof(novoTexto));
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("O motivo da alteração é obrigatório para auditoria.", nameof(motivo));

        var novaVersao = new VersaoAnotacao(
            id: _historicoVersoes.Count + 1,
            textoAnterior: AnotacoesClinicas,
            dataModificacao: DateTime.Now,
            motivoAlteracao: motivo);
        
        _historicoVersoes.Add(novaVersao);

        AnotacoesClinicas = novoTexto;
    }

    public void AdicionarVersaoExistente(VersaoAnotacao versao)
    {
        if (versao != null)
            _historicoVersoes.Add(versao);
    }
}