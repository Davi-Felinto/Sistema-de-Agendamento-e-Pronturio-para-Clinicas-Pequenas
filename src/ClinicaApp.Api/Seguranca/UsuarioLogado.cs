using System.Security.Claims;
using ClinicaApp.Domain.Enums;

namespace ClinicaApp.Api.Seguranca;

/// <summary>
/// Lê a identidade do usuário a partir do token, e não de dados enviados pelo cliente.
/// </summary>
public static class UsuarioLogado
{
    public static int ObterId(this ClaimsPrincipal usuario)
    {
        var valor = usuario.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(valor, out var id)
            ? id
            : throw new UnauthorizedAccessException("Token sem identificação de usuário.");
    }

    public static bool EhAdministrador(this ClaimsPrincipal usuario)
    {
        return usuario.IsInRole(nameof(PerfilUsuario.Administrador));
    }
}
