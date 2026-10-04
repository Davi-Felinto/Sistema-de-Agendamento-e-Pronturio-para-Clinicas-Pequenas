using System;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public class ProfissionalSaude : Usuario{
    public string RegistroProfissional {get; private set;}
    public string Especialidade {get; private set;}

    public ProfissionalSaude(
        int id,
        string nome,
        string login,
        string senhaPura,
        string registroProfissional,
        string especialidade)
        : base(id, nome, login, senhaPura, PerfilUsuario.Profissional)
    {
        if (string.IsNullOrWhiteSpace(registroProfissional))
            throw new ArgumentException("Registro profissional é obrigatório.", nameof(registroProfissional));

        RegistroProfissional = registroProfissional;
        Especialidade = especialidade ?? "Geral";
    }
}