using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface INotificador
{
    bool Enviar(Notificacao notificacao);
}
