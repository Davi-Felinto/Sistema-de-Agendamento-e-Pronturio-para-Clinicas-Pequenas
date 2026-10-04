using System;

namespace ClinicaApp.Domain.Entities;

public class Paciente
{
    // Propriedades com encapsulamento (private set):
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string DocumentoIdentificacao {get; private set;}
    public DateTime DataNascimento { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string Endereco { get; private set; }
    public string? Alergias {get; private set;}
    public string? CondicoesPreexistentes {get; private set;} 
    public bool Ativo {get; private set;}

    // Construtor com validação de invariantes:
    public Paciente(
        int id,
        string nome,
        string documentoIdentificacao,
        DateTime dataNascimento,
        string telefone,
        string email,
        string endereco,
        string? alergias = null,
        string? condicoesPreexistentes = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.", nameof(nome));
        
        if (string.IsNullOrWhiteSpace(documentoIdentificacao))
            throw new ArgumentException("O Documento de Identificação é obrigatório.", nameof(documentoIdentificacao));
        
        Id = id;
        Nome = nome;
        DocumentoIdentificacao = documentoIdentificacao;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Email = email;
        Endereco = endereco;
        Alergias = alergias;
        CondicoesPreexistentes = condicoesPreexistentes;
        Ativo = true; // Todo paciente nasce ativo
    }
    
    /// <summary>
    /// RN16 e RQ03 (LGPD): Inativação lógica com anonimização de dados pessoais,
    /// preservando o histórico clínico do prontuário.
    /// </summary>
    public void InativarComAnonimizacao()
    {
        Ativo = false;
        Telefone = "ANONIMIZADO";
        Email = "ANONIMIZADO";
        Endereco = "ANONIMIZADO";
    }
}
