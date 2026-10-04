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

    public Agendamento(
        int id,
        int pacienteId,
        int profissionalId,
        DateTime dataHoraInicio,
        DateTime dataHoraFim,
        string? observacoes = null)
    {    
        if (dataHoraFim <= dataHoraInicio)
            throw new ArgumentException("A data/hora de fim deve ser posterior ao início.");
        
        Id = id;
        PacienteId = pacienteId;
        ProfissionalId = profissionalId;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Observacoes = observacoes;
        Status = StatusAgendamento.Pendente;
        CancelamentoTardio =false;
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
    /// RN10: Se o cancelamento for feito com menos de 24 horas de antecedência,
    /// sinaliza como cancelamento tardio no histórico.
    /// </summary>
    public void Cancelar(DateTime momentoCancelamento){
        Status = StatusAgendamento.Cancelado;
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