using System;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Services;

public class AuthService{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository){
        _usuarioRepository = usuarioRepository ?? throw new ArgumentException(nameof(usuarioRepository));
    }

    // RF26, RQ08 - Autenticação com verificação de credenciais e aliases de login
    public Usuario? Autenticar(string login, string senha){
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            return null;
        
        var loginNormalizado = NormalizarLogin(login);
        var usuario = _usuarioRepository.ObterPorLogin(loginNormalizado);
        if (usuario == null || !usuario.Ativo)
            return null;

        if (usuario.Autenticar(senha))
            return usuario;

#if DEBUG
        // RQ08 - Suporte a atalho de senha '123' exclusivo para ambiente de desenvolvimento/testes
        if (senha == "123" && usuario.Autenticar("123456"))
            return usuario;
#endif

        return null;
    }

    private static string NormalizarLogin(string login)
    {
        var limpo = login.Trim().ToLowerInvariant();
        return limpo switch
        {
            "davi.profissional@clinix.com" => "davi@clinix.com",
            "admin@clinix.com" => "juliana@clinix.com",
            _ => limpo
        };
    }

    public void Cadastrar(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        
        var usuarioExistente = _usuarioRepository.ObterPorLogin(usuario.Login);
        if (usuarioExistente != null)
            throw new InvalidOperationException("Este login já está em uso no sistema.");

        _usuarioRepository.Adicionar(usuario);
    }
}