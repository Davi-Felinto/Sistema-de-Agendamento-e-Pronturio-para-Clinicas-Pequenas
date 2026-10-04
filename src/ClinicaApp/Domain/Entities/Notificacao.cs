using System;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public class Notificacao
{
    public int Id { get; private set; }
    public int AgendamentoId { get; private set; }
    public TipoNotificacao Tipo { get; private set; }
    public CanalNotificacao Canal { get; private set; }
    public string DestinatarioTelefone { get; private set; }
    public string Mensagem { get; private set; }
    public StatusNotificacao Status { get; private set; }
    public int TentativasEnvio { get; private set; }
    public DateTime? ProximaTentativa { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Notificacao(
        int id,
        int agendamentoId,
        TipoNotificacao tipo,
        string destinatarioTelefone,
        string mensagem,
        CanalNotificacao canal = CanalNotificacao.WhatsApp)
    {
        if (string.IsNullOrWhiteSpace(destinatarioTelefone))
            throw new ArgumentException("Telefone do destinatário é obrigatório.", nameof(destinatarioTelefone));

        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException("Mensagem é obrigatória.", nameof(mensagem));

        Id = id;
        AgendamentoId = agendamentoId;
        Tipo = tipo;
        Canal = canal;
        DestinatarioTelefone = destinatarioTelefone;
        Mensagem = mensagem;
        Status = StatusNotificacao.Pendente;
        TentativasEnvio = 0;
        ProximaTentativa = null;
        DataCriacao = DateTime.Now;
    }

    /// <summary>
    /// RF12, RF13: Registra sucesso no envio. Se foi uma segunda tentativa, marca como Reenviado.
    /// </summary>
    public void RegistrarSucesso(bool foiReenvio = false)
    {
        Status = foiReenvio ? StatusNotificacao.Reenviado : StatusNotificacao.Enviado;
        ProximaTentativa = null;
    }

    /// <summary>
    /// RN14 e RN15: Em caso de falha, permite reenviar automaticamente uma única vez,
    /// 15 minutos após a falha. Se falhar de novo, encerra tentativas.
    /// </summary>
    public void RegistrarFalha(DateTime momentoFalha)
    {
        Status = StatusNotificacao.Falha;
        TentativasEnvio++;

        if (TentativasEnvio < 2)
        {
            // RN14: Programa a nova tentativa para daqui a 15 minutos
            ProximaTentativa = momentoFalha.AddMinutes(15);
        }
        else
        {
            // RN15: Nova tentativa também falhou, esgotou limite
            ProximaTentativa = null;
        }
    }

    /// <summary>
    /// Verifica se a notificação está apta para reenvio no momento atual.
    /// </summary>
    public bool PodeReenviar(DateTime agora)
    {
        return Status == StatusNotificacao.Falha &&
               TentativasEnvio < 2 &&
               ProximaTentativa.HasValue &&
               agora >= ProximaTentativa.Value;
    }
}
