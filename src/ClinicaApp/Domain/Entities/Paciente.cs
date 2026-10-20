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

    // RF29, RN18: Endereço estruturado com CEP
    public string? Cep { get; private set; }
    public string? Logradouro { get; private set; }
    public string? Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string? Bairro { get; private set; }
    public string? Cidade { get; private set; }
    public string? EstadoUf { get; private set; }

    public string? Alergias {get; private set;}
    public string? CondicoesPreexistentes {get; private set;} 
    public bool Ativo {get; private set;}

    // Construtor sem parâmetros para materialização do Dapper / ORM
    private Paciente()
    {
        Nome = string.Empty;
        DocumentoIdentificacao = string.Empty;
        Telefone = string.Empty;
        Email = string.Empty;
        Endereco = string.Empty;
    }

    // Construtor com 10 parâmetros (assinatura exata esperada pelo Dapper em consultas SQL legadas)
    public Paciente(
        int id,
        string nome,
        string documentoIdentificacao,
        DateTime dataNascimento,
        string telefone,
        string email,
        string endereco,
        string? alergias = null,
        string? condicoesPreexistentes = null,
        bool ativo = true)
        : this(id, nome, documentoIdentificacao, dataNascimento, telefone, email, endereco, alergias, condicoesPreexistentes, ativo, null, null, null, null, null, null, null)
    {
    }

    // Construtor completo com validação de invariantes e suporte a endereço estruturado (RF29):
    public Paciente(
        int id,
        string nome,
        string documentoIdentificacao,
        DateTime dataNascimento,
        string telefone,
        string email,
        string endereco,
        string? alergias,
        string? condicoesPreexistentes,
        bool ativo,
        string? cep,
        string? logradouro,
        string? numero,
        string? complemento,
        string? bairro,
        string? cidade,
        string? estadoUf)
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
        Ativo = ativo;

        // RF29: Campos atômicos de endereço
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        EstadoUf = estadoUf;
    }

    /// <summary>
    /// RF29: Atualiza o endereço completo garantindo consistência com o CEP.
    /// </summary>
    public void AtualizarEndereco(
        string cep,
        string logradouro,
        string numero,
        string? complemento,
        string bairro,
        string cidade,
        string estadoUf)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        EstadoUf = estadoUf;
        Endereco = $"{logradouro}, {numero}{(string.IsNullOrWhiteSpace(complemento) ? "" : " - " + complemento)}, {bairro}, {cidade} - {estadoUf}, CEP: {cep}";
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
        Cep = "00000000";
        Logradouro = "ANONIMIZADO";
        Numero = "S/N";
        Complemento = null;
        Bairro = "ANONIMIZADO";
        Cidade = "ANONIMIZADO";
        EstadoUf = "XX";
    }
}
