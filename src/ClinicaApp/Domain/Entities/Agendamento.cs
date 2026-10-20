using System;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public class Agendamento{
    public int Id { get; private set; }
    public int PacienteId { get; private set; }
    public int ProfissionalId { get; private set; }
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    public StatusAgendamento Status { get; private set; }
    public string? Observacoes  { get; private set; }
    public bool CancelamentoTardio { get; private set; }

    // RF31, RN20: Motivo e justificativa do cancelamento
    public MotivoCancelamento? MotivoCancelamento { get; private set; }
    public string? JustificativaCancelamento { get; private set; }

    public Agendamento(
        int id,
        int pacienteId,
        int profissionalId,
        DateTime dataHoraInicio,
        DateTime dataHoraFim,
        string? observacoes = null,
        StatusAgendamento status = StatusAgendamento.Pendente,
        bool cancelamentoTardio = false,
        MotivoCancelamento? motivoCancelamento = null,
        string? justificativaCancelamento = null)
    {    
        if (dataHoraFim <= dataHoraInicio)
            throw new ArgumentException("A data/hora de fim deve ser posterior ao início.");
        
        Id = id;
        PacienteId = pacienteId;
        ProfissionalId = profissionalId;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Observacoes = observacoes;
        Status = status;
        CancelamentoTardio = cancelamentoTardio;
        MotivoCancelamento = motivoCancelamento;
        JustificativaCancelamento = justificativaCancelamento;
    }

    /// <summary>
    /// RN01 e RN02: Verifica colisão/sobreposição de horários.
    /// Se a consulta já estiver cancelada, ela não gera conflito com novas consultas.
    /// </summary>
    public bool TemConflito(DateTime inicio, DateTime fim){
        if (Status == StatusAgendamento.Cancelado)
            return false;

        return inicio < DataHoraFim && fim > DataHoraInicio;
    }

    /// <summary>
    /// RF06: Confirmação do agendamento.
    /// </summary>
    public void Confirmar(){
        Status = StatusAgendamento.Confirmado;
    }

    /// <summary>
    /// RN10, RN20: Se o cancelamento for feito com menos de 24 horas de antecedência,
    /// sinaliza como cancelamento tardio no histórico e armazena o motivo categorizado.
    /// </summary>
    public void Cancelar(
        DateTime momentoCancelamento,
        MotivoCancelamento motivo = Enums.MotivoCancelamento.Outro,
        string? justificativa = null)
    {
        Status = StatusAgendamento.Cancelado;
        MotivoCancelamento = motivo;
        JustificativaCancelamento = justificativa;
        var diferencaHoras = (DataHoraInicio - momentoCancelamento).TotalHours;

        if (diferencaHoras < 24){
            CancelamentoTardio = true;
        }
    }


    public void Remarcar(DateTime novoInicio, DateTime novoFim){
        if (novoFim <= novoInicio)
            throw new ArgumentException("A nova data de fim deve ser posterior ao início.");

        DataHoraInicio = novoInicio;
        DataHoraFim = novoFim;
        Status = StatusAgendamento.Remarcado;
    }
}