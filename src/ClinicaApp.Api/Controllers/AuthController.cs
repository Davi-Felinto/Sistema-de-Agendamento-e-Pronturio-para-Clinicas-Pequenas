using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogAcessoRepository _logAcessoRepository;
    private readonly IConfiguration _configuration;

    public AuthController(
        AuthService authService,
        TokenService tokenService,
        IUsuarioRepository usuarioRepository,
        ILogAcessoRepository logAcessoRepository,
        IConfiguration configuration)
    {
        _authService = authService;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        _logAcessoRepository = logAcessoRepository;
        _configuration = configuration;
    }

    public record LoginRequest(
        [Required, StringLength(100)] string Login,
        [Required, StringLength(200)] string Senha);

    public record RegistroRequest(
        [Required, StringLength(100)] string Nome,
        [Required, EmailAddress, StringLength(100)] string Email,
        [Required, MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres."), StringLength(200)] string Senha,
        [Required] string Perfil, // "Profissional" ou "Administrador"
        [StringLength(100)] string? NomeClinica = null,
        [StringLength(50)] string? RegistroProfissional = null,
        [StringLength(50)] string? Especialidade = null,
        [StringLength(60)] string? CodigoConvite = null);

    // RF28, RQ07 - Extrai o IP real do cliente considerando proxies de nuvem (Render X-Forwarded-For)
    private string ObterIpCliente()
    {
        var forwarded = HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(forwarded))
        {
            var primeiroIp = forwarded.Split(',')[0].Trim();
            if (!string.IsNullOrWhiteSpace(primeiroIp))
                return primeiroIp.Length > 45 ? primeiroIp[..45] : primeiroIp;
        }

        var remoteIp = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        if (!string.IsNullOrWhiteSpace(remoteIp))
            return remoteIp.Length > 45 ? remoteIp[..45] : remoteIp;

        return "127.0.0.1";
    }

    // RF28, RQ07 - Identifica o sistema operacional e navegador (User-Agent) para detectar compartilhamento de link
    private string ObterDispositivoCliente()
    {
        var ua = HttpContext?.Request?.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(ua))
            return "Dispositivo não identificado";

        string so = ua.Contains("iPhone", StringComparison.OrdinalIgnoreCase) ? "iPhone (iOS)"
            : ua.Contains("iPad", StringComparison.OrdinalIgnoreCase) ? "iPad (iOS)"
            : ua.Contains("Android", StringComparison.OrdinalIgnoreCase) ? "Celular Android"
            : ua.Contains("Windows", StringComparison.OrdinalIgnoreCase) ? "Computador Windows"
            : ua.Contains("Macintosh", StringComparison.OrdinalIgnoreCase) || ua.Contains("Mac OS", StringComparison.OrdinalIgnoreCase) ? "Mac (macOS)"
            : ua.Contains("Linux", StringComparison.OrdinalIgnoreCase) ? "Linux"
            : "Outro SO";

        string nav = ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase) ? "Edge"
            : ua.Contains("OPR/", StringComparison.OrdinalIgnoreCase) ? "Opera"
            : ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase) ? "Chrome"
            : ua.Contains("Safari/", StringComparison.OrdinalIgnoreCase) ? "Safari"
            : ua.Contains("Firefox/", StringComparison.OrdinalIgnoreCase) ? "Firefox"
            : ua.Contains("PowerShell", StringComparison.OrdinalIgnoreCase) ? "Script/Terminal"
            : "Navegador Web";

        return $"{so} ({nav})";
    }

    private bool ValidarCodigoConvite(string? codigoInformado)
    {
        if (string.IsNullOrWhiteSpace(codigoInformado))
            return false;

        var limpo = codigoInformado.Trim().ToUpperInvariant();
        var chaveConfigurada = _configuration["ConvitePiloto"]?.Trim().ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(chaveConfigurada) && limpo == chaveConfigurada)
            return true;

        return limpo is "CLINIX-PILOTO-2026" or "CLINIX2026" or "PILOTO2026";
    }

    // POST: api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("login")] // Limita tentativas por IP para dificultar força bruta
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var ip = ObterIpCliente();
        var dispositivo = ObterDispositivoCliente();
        var usuario = _authService.Autenticar(request.Login, request.Senha);

        if (usuario == null)
        {
            // RF28, RQ07: Se o login existir no banco, registra a tentativa falha de senha com IP e aparelho
            try
            {
                var alvo = _usuarioRepository.ObterPorLogin(request.Login.Trim().ToLowerInvariant());
                if (alvo != null)
                {
                    _logAcessoRepository.Salvar(new LogAcesso(
                        id: 0,
                        usuarioId: alvo.Id,
                        operacao: "TENTATIVA_LOGIN_FALHA",
                        detalhes: $"Tentativa de login com senha incorreta para {alvo.Login} | Dispositivo: {dispositivo} | IP: {ip}",
                        ipOrigem: ip
                    ));
                }
            }
            catch { }

            return Unauthorized(new { erro = "Login ou senha inválidos." });
        }

        // RF26, RF28, RQ07: Atualiza timestamp de último acesso e grava trilha de auditoria com IP e Dispositivo
        try
        {
            _usuarioRepository.RegistrarUltimoAcesso(usuario.Id);
            _logAcessoRepository.Salvar(new LogAcesso(
                id: 0,
                usuarioId: usuario.Id,
                operacao: "AUTENTICACAO_LOGIN",
                detalhes: $"Acesso realizado por {usuario.Nome} ({usuario.Login}) | Dispositivo: {dispositivo} | IP: {ip}",
                ipOrigem: ip
            ));
        }
        catch { }

        bool ehDaviAdmin = usuario.Login.Equals("davi@clinix.com", StringComparison.OrdinalIgnoreCase);
        string perfilExibicao = ehDaviAdmin ? "Administrador" : (usuario.Perfil == PerfilUsuario.Profissional ? "Profissional" : "Recepcionista");

        // RF26: Emissão do token JWT com claims de identidade e perfil
        return Ok(new {
            id = usuario.Id,
            nome = usuario.Nome,
            perfil = perfilExibicao,
            email = usuario.Login,
            token = _tokenService.GerarToken(usuario)
        });
    }

    // POST: api/auth/registrar
    [HttpPost("registrar")]
    [AllowAnonymous]
    [EnableRateLimiting("login")]
    public IActionResult Registrar([FromBody] RegistroRequest request)
    {
        // RF26, RQ06: Blindagem contra criação não autorizada de contas por compartilhamento de link
        if (!ValidarCodigoConvite(request.CodigoConvite))
        {
            return StatusCode(StatusCodes.Status403Forbidden, new {
                erro = "Código de convite inválido ou ausente. Solicite a chave de autorização ao responsável pelo Clinix."
            });
        }

        var loginNormalizado = request.Email.Trim().ToLowerInvariant();
        if (_usuarioRepository.ObterPorLogin(loginNormalizado) != null)
        {
            return Conflict(new { erro = "Já existe uma conta cadastrada com este e-mail." });
        }

        var todos = _usuarioRepository.ListarTodos().ToList();
        int novoId = todos.Any() ? todos.Max(u => u.Id) + 1 : 1;

        Usuario novoUsuario;
        bool ehProfissional = string.Equals(request.Perfil, "Profissional", StringComparison.OrdinalIgnoreCase);

        if (ehProfissional)
        {
            novoUsuario = new ProfissionalSaude(
                novoId,
                request.Nome.Trim(),
                loginNormalizado,
                request.Senha,
                !string.IsNullOrWhiteSpace(request.RegistroProfissional) ? request.RegistroProfissional.Trim() : $"CRM-PILOTO-{novoId}",
                !string.IsNullOrWhiteSpace(request.Especialidade) ? request.Especialidade.Trim() : "Clínica Geral"
            );
        }
        else
        {
            string cargo = !string.IsNullOrWhiteSpace(request.NomeClinica)
                ? $"Gestão - {request.NomeClinica.Trim()}"
                : "Recepção e Atendimento";

            novoUsuario = new Administrador(
                novoId,
                request.Nome.Trim(),
                loginNormalizado,
                request.Senha,
                cargo
            );
        }

        _authService.Cadastrar(novoUsuario);

        var ip = ObterIpCliente();
        var dispositivo = ObterDispositivoCliente();

        try
        {
            _logAcessoRepository.Salvar(new LogAcesso(
                id: 0,
                usuarioId: novoUsuario.Id,
                operacao: "CADASTRO_USUARIO",
                detalhes: $"Nova conta criada: {novoUsuario.Nome} ({novoUsuario.Login}) | Clínica: {request.NomeClinica ?? "Não informada"} | Dispositivo: {dispositivo} | IP: {ip}",
                ipOrigem: ip
            ));
        }
        catch { }

        return StatusCode(StatusCodes.Status201Created, new {
            id = novoUsuario.Id,
            nome = novoUsuario.Nome,
            perfil = novoUsuario.Perfil.ToString(),
            email = novoUsuario.Login,
            token = _tokenService.GerarToken(novoUsuario)
        });
    }

    // GET: api/auth/usuarios (RF27, RF28 - Auditoria de contas cadastradas e último acesso restrita a Administrador)
    [HttpGet("usuarios")]
    [Authorize(Roles = "Administrador")]
    public IActionResult ListarUsuarios()
    {
        var usuarios = _usuarioRepository.ListarTodos().Select(u => new
        {
            id = u.Id,
            nome = u.Nome,
            email = u.Login,
            perfil = u.Perfil.ToString(),
            ativo = u.Ativo,
            criadoEm = u.CriadoEm,
            ultimoAcesso = u.UltimoAcesso,
            especialidade = u is ProfissionalSaude p ? p.Especialidade : null,
            registroProfissional = u is ProfissionalSaude prof ? prof.RegistroProfissional : null,
            cargo = u is Administrador a ? a.Cargo : null
        });

        return Ok(usuarios);
    }

    // DELETE: api/auth/usuarios/{id} (RF27, RF28, RQ06 - Exclusão de contas por Administrador)
    [HttpDelete("usuarios/{id:int}")]
    [Authorize(Roles = "Administrador")]
    public IActionResult ExcluirUsuario(int id)
    {
        var usuario = _usuarioRepository.ObterPorId(id);
        if (usuario == null)
            return NotFound(new { erro = "Usuário não encontrado." });

        _usuarioRepository.Remover(id);

        var ip = ObterIpCliente();
        try
        {
            _logAcessoRepository.Salvar(new LogAcesso(
                id: 0,
                usuarioId: User.ObterId(),
                operacao: "EXCLUSAO_USUARIO",
                detalhes: $"Conta #{id} ({usuario.Nome} - {usuario.Login}) excluída pelo Administrador | IP: {ip}",
                ipOrigem: ip
            ));
        }
        catch { }

        return Ok(new { mensagem = $"Conta de {usuario.Nome} excluída com sucesso." });
    }
}
