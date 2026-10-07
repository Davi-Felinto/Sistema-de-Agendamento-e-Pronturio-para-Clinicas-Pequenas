using System;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

namespace ClinicaApp.Tests.Services;

public class AuthServiceTests
{
    private readonly InMemoryUsuarioRepository _usuarioRepository;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _usuarioRepository = new InMemoryUsuarioRepository();
        _authService = new AuthService(_usuarioRepository);
    }

    [Fact]
    public void Deve_Autenticar_Com_Sucesso_Quando_Credenciais_Forem_Validas()
    {
        // Arrange (RF26, RQ08)
        var usuario = new Administrador(
            id: 1,
            nome: "Carlos Silva",
            login: "carlos.admin",
            senhaPura: "Admin@123",
            cargo: "Gerente"
        );
        _authService.Cadastrar(usuario);

        // Act
        var usuarioAutenticado = _authService.Autenticar("carlos.admin", "Admin@123");

        // Assert
        Assert.NotNull(usuarioAutenticado);
        Assert.Equal("carlos.admin", usuarioAutenticado.Login);
        Assert.Equal(1, usuarioAutenticado.Id);
    }

    [Fact]
    public void Deve_Retornar_Null_Quando_Senha_For_Incorreta()
    {
        // Arrange
        var usuario = new Administrador(
            id: 2,
            nome: "Mariana Souza",
            login: "mariana.admin",
            senhaPura: "SenhaCorreta",
            cargo: "Recepção"
        );
        _authService.Cadastrar(usuario);

        // Act
        var usuarioAutenticado = _authService.Autenticar("mariana.admin", "SenhaInvalida");

        // Assert
        Assert.Null(usuarioAutenticado);
    }

    [Fact]
    public void Deve_Retornar_Null_Quando_Usuario_Nao_Existir()
    {
        // Act
        var usuarioAutenticado = _authService.Autenticar("usuario.fantasma", "qualquerSenha");

        // Assert
        Assert.Null(usuarioAutenticado);
    }

    [Theory]
    [InlineData("", "123456")]
    [InlineData("admin", "")]
    [InlineData("   ", "123456")]
    public void Deve_Retornar_Null_Quando_Login_Ou_Senha_Forem_Vazios(string login, string senha)
    {
        // Act
        var usuarioAutenticado = _authService.Autenticar(login, senha);

        // Assert
        Assert.Null(usuarioAutenticado);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Tentar_Cadastrar_Login_Duplicado()
    {
        // Arrange
        var usuario1 = new ProfissionalSaude(
            id: 3,
            nome: "Dr. João",
            login: "dr.joao",
            senhaPura: "Senha123",
            registroProfissional: "CRM 123",
            especialidade: "Clínico Geral"
        );
        var usuario2 = new ProfissionalSaude(
            id: 4,
            nome: "João Outro",
            login: "dr.joao", // Mesmo login
            senhaPura: "OutraSenha",
            registroProfissional: "CRM 456",
            especialidade: "Pediatria"
        );

        _authService.Cadastrar(usuario1);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => _authService.Cadastrar(usuario2));
        Assert.Contains("já está em uso", ex.Message);
    }
}
