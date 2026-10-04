using System;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public class Administrador : Usuario{
    public string Cargo {get; private set;}

    public Administrador(
        int id,
        string nome,
        string login,
        string senhaPura,
        string cargo)
        : base(id, nome, login, senhaPura, PerfilUsuario.Administrador)
    {
        Cargo = cargo ?? "Administrador";
    }

    /// <summary>
    /// RN12: Administrador tem permissão para auditar logs e relatórios gerais.
    /// </summary>
    public override bool PodeAuditarLogs() => true;
}