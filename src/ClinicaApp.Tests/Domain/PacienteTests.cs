using System;
using Xunit;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Tests.Domain;

public class PacienteTests
{
    [Fact]
    public void Deve_Criar_Paciente_Com_Sucesso_E_Status_Ativo()
    {
        // Arrange & Act
        var paciente = new Paciente(
            id: 1,
            nome: "João da Silva",
            documentoIdentificacao: "123.456.789-00",
            dataNascimento: new DateTime(1990, 5, 15),
            telefone: "(61) 98888-7777",
            email: "joao.silva@email.com",
            endereco: "Brasília - DF",
            alergias: "Dipirona",
            condicoesPreexistentes: "Hipertensão"
        );

        // Assert
        Assert.Equal(1, paciente.Id);
        Assert.Equal("João da Silva", paciente.Nome);
        Assert.Equal("123.456.789-00", paciente.DocumentoIdentificacao);
        Assert.True(paciente.Ativo);
        Assert.Equal("Dipirona", paciente.Alergias);
        Assert.Equal("Hipertensão", paciente.CondicoesPreexistentes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Deve_Lancar_Excecao_Ao_Criar_Paciente_Com_Nome_Invalido(string? nomeInvalido)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Paciente(
                id: 1,
                nome: nomeInvalido!,
                documentoIdentificacao: "123.456.789-00",
                dataNascimento: new DateTime(1990, 5, 15),
                telefone: "(61) 98888-7777",
                email: "joao@email.com",
                endereco: "Brasília - DF"
            )
        );

        Assert.Contains("Nome", ex.Message);
    }

    [Fact]
    public void InativarComAnonimizacao_Deve_Inativar_E_Anonimizar_Dados_Sensiveis_Conforme_LGPD()
    {
        // Arrange (RN16, RQ03 - LGPD)
        var paciente = new Paciente(
            id: 1,
            nome: "Maria Oliveira",
            documentoIdentificacao: "987.654.321-11",
            dataNascimento: new DateTime(1985, 10, 20),
            telefone: "(61) 99999-1111",
            email: "maria@email.com",
            endereco: "Asa Sul, Bloco B",
            alergias: "Penicilina",
            condicoesPreexistentes: "Nenhuma"
        );

        // Act
        paciente.InativarComAnonimizacao();

        // Assert
        Assert.False(paciente.Ativo, "O status do paciente deve ser Inativo.");
        Assert.Equal("ANONIMIZADO", paciente.Telefone);
        Assert.Equal("ANONIMIZADO", paciente.Email);
        Assert.Equal("ANONIMIZADO", paciente.Endereco);
        
        // Histórico clínico deve permanecer intacto para o prontuário
        Assert.Equal("Penicilina", paciente.Alergias);
        Assert.Equal("Nenhuma", paciente.CondicoesPreexistentes);
    }
}