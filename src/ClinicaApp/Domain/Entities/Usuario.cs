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

        var partes = SenhaHash.Split('.');
        if (partes.Length != 3 || !int.TryParse(partes[0], out var iteracoes)) return false;

        var salt = Convert.FromHexString(partes[1]);
        var hashArmazenado = Convert.FromHexString(partes[2]);
        var hashDigitado = Rfc2898DeriveBytes.Pbkdf2(senhaDigitada, salt, iteracoes, HashAlgorithmName.SHA256, hashArmazenado.Length);

        // Comparação em tempo constante para não vazar informação pelo tempo de resposta
        return CryptographicOperations.FixedTimeEquals(hashDigitado, hashArmazenado);
    }

    /// <summary>
    /// RN12: Por padrão, usuários não podem auditar logs do sistema.
    /// Classes filhas autorizadas podem sobrescrever (override).
    /// </summary>
    public virtual bool PodeAuditarLogs() => false;

    private const int IteracoesPbkdf2 = 210_000;
    private const int TamanhoSalt = 16;
    private const int TamanhoHash = 32;

    /// <summary>
    /// RQ08: PBKDF2-SHA256 com salt aleatório por usuário.
    /// Formato armazenado: "iteracoes.saltHex.hashHex".
    /// </summary>
    private static string GerarHashSenha(string senha){
        var salt = RandomNumberGenerator.GetBytes(TamanhoSalt);
        var hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, IteracoesPbkdf2, HashAlgorithmName.SHA256, TamanhoHash);
        return $"{IteracoesPbkdf2}.{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
    }
}