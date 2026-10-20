using System;

namespace ClinicaApp.Domain.Entities;

public enum StatusAmostraLaboratorio
{
    ColetadoNaClinica = 1,
    EnviadoAoLaboratorio = 2,
    EmAnalise = 3,
    LaudoLiberado = 4,
    EntregueAoPaciente = 5
}

/// <summary>
/// RF34, RN23: Rastreamento do ciclo de vida de exames e amostras terceirizadas.
/// </summary>
public class PedidoExameLaboratorio
{
    public int Id { get; private set; }
    public int PacienteId { get; private set; }
    public string NomePaciente { get; private set; }
    public int? ProntuarioId { get; private set; }
    public string NomeExameAmostra { get; private set; }
    public string LaboratorioParceiro { get; private set; }
    public string? CodigoRastreio { get; private set; }
    public DateTime DataColeta { get; private set; }
    public DateTime DataPrevisaoLaudo { get; private set; }
    public DateTime? DataRecebimentoLaudo { get; private set; }
    public StatusAmostraLaboratorio StatusAmostra { get; private set; }
    public string? Observacoes { get; private set; }

    public bool LaudoPendente => StatusAmostra != StatusAmostraLaboratorio.LaudoLiberado && StatusAmostra != StatusAmostraLaboratorio.EntregueAoPaciente;

    public PedidoExameLaboratorio(
        int id,
        int pacienteId,
        string nomePaciente,
        string nomeExameAmostra,
        string laboratorioParceiro,
        DateTime dataColeta,
        DateTime dataPrevisaoLaudo,
        int? prontuarioId = null,
        string? codigoRastreio = null,
        StatusAmostraLaboratorio statusAmostra = StatusAmostraLaboratorio.ColetadoNaClinica,
        DateTime? dataRecebimentoLaudo = null,
        string? observacoes = null)
    {
        if (string.IsNullOrWhiteSpace(nomeExameAmostra))
            throw new ArgumentException("O nome do exame ou amostra é obrigatório.", nameof(nomeExameAmostra));

        if (string.IsNullOrWhiteSpace(laboratorioParceiro))
            throw new ArgumentException("O laboratório parceiro é obrigatório.", nameof(laboratorioParceiro));

        Id = id;
        PacienteId = pacienteId;
        NomePaciente = string.IsNullOrWhiteSpace(nomePaciente) ? "Paciente" : nomePaciente;
        NomeExameAmostra = nomeExameAmostra;
        LaboratorioParceiro = laboratorioParceiro;
        DataColeta = dataColeta;
        DataPrevisaoLaudo = dataPrevisaoLaudo;
        ProntuarioId = prontuarioId;
        CodigoRastreio = codigoRastreio;
        StatusAmostra = statusAmostra;
        DataRecebimentoLaudo = dataRecebimentoLaudo;
        Observacoes = observacoes;
    }

    public void AtualizarStatus(StatusAmostraLaboratorio novoStatus, string? observacao = null)
    {
        StatusAmostra = novoStatus;
        if (novoStatus == StatusAmostraLaboratorio.LaudoLiberado && DataRecebimentoLaudo == null)
        {
            DataRecebimentoLaudo = DateTime.Now;
        }
        if (!string.IsNullOrWhiteSpace(observacao))
        {
            Observacoes = observacao;
        }
    }
}
