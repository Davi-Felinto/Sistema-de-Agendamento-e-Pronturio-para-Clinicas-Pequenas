using System.Collections.Generic;
using ClinicaApp.Domain.Entities;

namespace ClinicaApp.Domain.Interfaces;

public interface IUsuarioRepository
{
    Usuario? ObterPorId(int id);
    Usuario? ObterPorLogin(string login);
    IEnumerable<Usuario> ListarTodos();
    void Adicionar(Usuario usuario);
}
