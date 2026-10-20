namespace ClinicaApp.Domain.Enums;

/// <summary>
/// RF31, RN20: Categorização obrigatória do motivo de cancelamento de consultas.
/// </summary>
public enum MotivoCancelamento
{
    IncompatibilidadeHorario = 1,
    DificuldadeFinanceira = 2,
    ProblemaSaudeImprevisto = 3,
    InsatisfacaoAtendimento = 4,
    DesistenciaSemMotivoInformado = 5,
    Outro = 6
}
