using System;

namespace ClinicaApp.Domain.Entities;

/// <summary>
/// RF32, RN21: Registro de avaliação de satisfação (Ouvidoria / NPS pós-consulta).
/// </summary>
public class AvaliacaoAtendimento
{
    public int Id { get; private set; }
    public int AgendamentoId { get; private set; }
    public int PacienteId { get; private set; }
    public string NomePaciente { get; private set; }
    public DateTime DataAvaliacao { get; private set; }
    public int NotaRecepcao { get; private set; } // 1 a 5 estrelas
    public int NotaAtendimentoClinico { get; private set; } // 1 a 5 estrelas
    public int NotaEspacoFisico { get; private set; } // 1 a 5 estrelas
    public string? ComentarioOuvidoria { get; private set; }
    public bool Anonimo { get; private set; }

    public double MediaGeral => Math.Round((NotaRecepcao + NotaAtendimentoClinico + NotaEspacoFisico) / 3.0, 1);

    public AvaliacaoAtendimento(
        int id,
        int agendamentoId,
        int pacienteId,
        string nomePaciente,
        DateTime dataAvaliacao,
        int notaRecepcao,
        int notaAtendimentoClinico,
        int notaEspacoFisico,
        string? comentarioOuvidoria = null,
        bool anonimo = false)
    {
        if (notaRecepcao < 1 || notaRecepcao > 5)
            throw new ArgumentOutOfRangeException(nameof(notaRecepcao), "A nota da recepção deve estar entre 1 e 5.");

        if (notaAtendimentoClinico < 1 || notaAtendimentoClinico > 5)
            throw new ArgumentOutOfRangeException(nameof(notaAtendimentoClinico), "A nota do atendimento clínico deve estar entre 1 e 5.");

        if (notaEspacoFisico < 1 || notaEspacoFisico > 5)
            throw new ArgumentOutOfRangeException(nameof(notaEspacoFisico), "A nota do espaço físico deve estar entre 1 e 5.");

        Id = id;
        AgendamentoId = agendamentoId;
        PacienteId = pacienteId;
        NomePaciente = anonimo ? "Paciente Anônimo" : (string.IsNullOrWhiteSpace(nomePaciente) ? "Paciente" : nomePaciente);
        DataAvaliacao = dataAvaliacao;
        NotaRecepcao = notaRecepcao;
        NotaAtendimentoClinico = notaAtendimentoClinico;
        NotaEspacoFisico = notaEspacoFisico;
        ComentarioOuvidoria = comentarioOuvidoria;
        Anonimo = anonimo;
    }
}
