using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.External;

public class NotificadorWhatsApp : INotificador
{
    public bool Enviar(Notificacao notificacao)
    {
        // Simulação de envio via API do WhatsApp
        if (string.IsNullOrWhiteSpace(notificacao.DestinatarioTelefone) || 
            string.IsNullOrWhiteSpace(notificacao.Mensagem))
        {
            return false;
        }

        return true;
    }
}
