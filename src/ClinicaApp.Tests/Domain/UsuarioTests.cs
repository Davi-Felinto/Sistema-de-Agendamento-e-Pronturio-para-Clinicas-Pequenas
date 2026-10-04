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
}
