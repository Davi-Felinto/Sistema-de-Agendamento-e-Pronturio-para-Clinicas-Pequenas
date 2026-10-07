using System;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Services;

public class AuthService{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository){
        _usuarioRepository = usuarioRepository ?? throw new ArgumentException(nameof(usuarioRepository));
    }

    public Usuario? Autenticar(string login, string senha){
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            return null;
        
        var usuario = _usuarioRepository.ObterPorLogin(login);
        if (usuario == null || !usuario.Ativo)
            return null;

        return usuario.Autenticar(senha) ? usuario : null;
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