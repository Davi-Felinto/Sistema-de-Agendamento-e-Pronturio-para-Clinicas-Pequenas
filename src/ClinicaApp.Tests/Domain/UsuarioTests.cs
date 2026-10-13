using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Tests.Domain;

public class UsuarioTests
{
    [Fact]
    public void Deve_Criar_ProfissionalSaude_Com_Perfil_Correto_E_Dados_Profissionais()
    {
        // Arrange & Act
        var medico = new ProfissionalSaude(
            id: 1,
            nome: "Dr. Roberto Carlos",
            login: "roberto.carlos",
            senhaPura: "SenhaForte@123",
            registroProfissional: "CRM/DF 12345",
            especialidade: "Cardiologia"
        );

        // Assert
        Assert.Equal(1, medico.Id);
        Assert.Equal("Dr. Roberto Carlos", medico.Nome);
        Assert.Equal("roberto.carlos", medico.Login);
        Assert.Equal(PerfilUsuario.Profissional, medico.Perfil);
        Assert.Equal("CRM/DF 12345", medico.RegistroProfissional);
        Assert.Equal("Cardiologia", medico.Especialidade);
        Assert.True(medico.Ativo);
    }

    [Fact]
    public void Deve_Criar_Administrador_Com_Perfil_Correto_E_Permissao_De_Auditoria()
    {
        // Arrange & Act (RN12)
        var admin = new Administrador(
            id: 2,
            nome: "Ana Gerente",
            login: "ana.admin",
            senhaPura: "Admin@2026",
            cargo: "Gerente Geral"
        );

        // Assert
        Assert.Equal(PerfilUsuario.Administrador, admin.Perfil);
        Assert.Equal("Gerente Geral", admin.Cargo);
        Assert.True(admin.PodeAuditarLogs(), "Administrador deve ter permissão para auditar logs (RN12).");
    }

    [Fact]
    public void Autenticar_Com_Senha_Correta_Deve_Retornar_True()
    {
        // Arrange (RF26, RQ08)
        var usuario = new ProfissionalSaude(
            id: 3,
            nome: "Dra. Juliana",
            login: "juliana.psi",
            senhaPura: "Segredo123",
            registroProfissional: "CRP 01/9999",
            especialidade: "Psicologia"
        );

        // Act & Assert
        Assert.True(usuario.Autenticar("Segredo123"));
        Assert.False(usuario.Autenticar("SenhaErrada"));
    }

    [Fact]
    public void Senhas_Iguais_Devem_Gerar_Hashes_Diferentes_Por_Causa_Do_Salt()
    {
        // Arrange (RQ08)
        var usuario1 = new Administrador(id: 4, nome: "Admin Um", login: "admin.um", senhaPura: "MesmaSenha@1", cargo: "Gerente");
        var usuario2 = new Administrador(id: 5, nome: "Admin Dois", login: "admin.dois", senhaPura: "MesmaSenha@1", cargo: "Gerente");

        // Assert
        Assert.NotEqual(usuario1.SenhaHash, usuario2.SenhaHash);
        Assert.DoesNotContain("MesmaSenha@1", usuario1.SenhaHash);
        Assert.True(usuario1.Autenticar("MesmaSenha@1"));
        Assert.True(usuario2.Autenticar("MesmaSenha@1"));
    }

    [Fact]
    public void Autenticar_Com_Hash_Legado_Sha256_Deve_Retornar_True()
    {
        // Arrange (Compatibilidade com o banco Aiven / MySQL existente)
        var usuario = new Administrador(id: 6, nome: "Admin Legado", login: "admin.legado", senhaPura: "Provisoria", cargo: "Gerente");
        // Hash SHA-256 de "123456"
        usuario.DefinirHashPersistido("8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");

        // Act & Assert
        Assert.True(usuario.Autenticar("123456"));
        Assert.False(usuario.Autenticar("OutraSenha"));
    }
}
