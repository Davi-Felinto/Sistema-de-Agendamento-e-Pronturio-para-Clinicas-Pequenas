using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.InMemory;

public class InMemoryUsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios = new();

    public Usuario? ObterPorId(int id)
    {
        return _usuarios.FirstOrDefault(u => u.Id == id);
    }

    public Usuario? ObterPorLogin(string login)
    {
        return _usuarios.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Usuario> ListarTodos()
    {
        return _usuarios.ToList();
    }

    public void Adicionar(Usuario usuario)
    {
        _usuarios.Add(usuario);
    }
}
