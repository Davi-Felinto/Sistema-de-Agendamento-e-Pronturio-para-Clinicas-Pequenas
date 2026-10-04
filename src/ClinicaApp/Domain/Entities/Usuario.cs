using System;
using System.Security.Cryptography;
using System.Text;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Domain.Entities;

public abstract class Usuario
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; } 
    public string Login { get; protected set; }
    public string SenhaHash { get; protected set; }
    public PerfilUsuario Perfil { get; protected set; }
    public bool Ativo { get; protected set; }

    protected Usuario(
        int id,
        string nome,
        string login,
        string senhaPura,
        PerfilUsuario perfil)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório", nameof(nome));
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Login é obrigatório", nameof(login));
        if (string.IsNullOrEmpty(senhaPura))
            throw new ArgumentException("Senha é obrigatória", nameof(senhaPura));

        Id = id;
        Nome = nome;
        Login = login;
        SenhaHash = GerarHashSenha(senhaPura);
        Perfil = perfil;
        Ativo = true;
    }

    /// <summary>
    /// RQ08: Autenticação segura comparando o hash da senha digitada.
    /// </summary>
    public bool Autenticar(string senhaDigitada){
        if (string.IsNullOrWhiteSpace(senhaDigitada)) return false;
        var hashDigitado = GerarHashSenha(senhaDigitada);
        return hashDigitado == SenhaHash;
    }

    /// <summary>
    /// RN12: Por padrão, usuários não podem auditar logs do sistema.
    /// Classes filhas autorizadas podem sobrescrever (override).
    /// </summary>
    public virtual bool PodeAuditarLogs() => false;

    private static string GerarHashSenha(string senha){
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }
}