using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySqlConnector;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.MySQL;

public class MySqlUsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public MySqlUsuarioRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    private record UsuarioDbDto(
        int id_usuario,
        string nome,
        string login,
        string senha_hash,
        string perfil,
        bool ativo,
        DateTime criado_em,
        DateTime? ultimo_acesso,
        string? registro_profissional,
        string? especialidade,
        string? cargo
    );

    private static Usuario MapearParaEntidade(UsuarioDbDto dto)
    {
        if (dto.perfil == "Profissional")
        {
            var prof = new ProfissionalSaude(
                dto.id_usuario,
                dto.nome,
                dto.login,
                "dummy_temporario",
                dto.registro_profissional ?? "N/A",
                dto.especialidade ?? "Geral"
            );
            prof.DefinirHashPersistido(dto.senha_hash);
            prof.DefinirAtivo(dto.ativo);
            prof.DefinirDatasPersistidas(dto.criado_em, dto.ultimo_acesso);
            return prof;
        }
        else
        {
            var admin = new Administrador(
                dto.id_usuario,
                dto.nome,
                dto.login,
                "dummy_temporario",
                dto.cargo ?? "Administrador"
            );
            admin.DefinirHashPersistido(dto.senha_hash);
            admin.DefinirAtivo(dto.ativo);
            admin.DefinirDatasPersistidas(dto.criado_em, dto.ultimo_acesso);
            return admin;
        }
    }

    // RF26, RQ08 - Obter usuário por ID com dados especializados
    public Usuario? ObterPorId(int id)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                u.id_usuario, u.nome, u.login, u.senha_hash, u.perfil, u.ativo,
                u.criado_em, u.ultimo_acesso,
                p.registro_profissional, p.especialidade,
                a.cargo
            FROM usuarios u
            LEFT JOIN profissionais_saude p ON p.id_usuario = u.id_usuario
            LEFT JOIN administradores a ON a.id_usuario = u.id_usuario
            WHERE u.id_usuario = @Id;";

        var dto = conexao.QueryFirstOrDefault<UsuarioDbDto>(sql, new { Id = id });
        return dto != null ? MapearParaEntidade(dto) : null;
    }

    // RF26, RQ08 - Obter usuário por login para autenticação segura
    public Usuario? ObterPorLogin(string login)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                u.id_usuario, u.nome, u.login, u.senha_hash, u.perfil, u.ativo,
                u.criado_em, u.ultimo_acesso,
                p.registro_profissional, p.especialidade,
                a.cargo
            FROM usuarios u
            LEFT JOIN profissionais_saude p ON p.id_usuario = u.id_usuario
            LEFT JOIN administradores a ON a.id_usuario = u.id_usuario
            WHERE u.login = @Login;";

        var dto = conexao.QueryFirstOrDefault<UsuarioDbDto>(sql, new { Login = login });
        return dto != null ? MapearParaEntidade(dto) : null;
    }

    // RF27 - Listar todos os usuários do sistema ordenados por último acesso e criação
    public IEnumerable<Usuario> ListarTodos()
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                u.id_usuario, u.nome, u.login, u.senha_hash, u.perfil, u.ativo,
                u.criado_em, u.ultimo_acesso,
                p.registro_profissional, p.especialidade,
                a.cargo
            FROM usuarios u
            LEFT JOIN profissionais_saude p ON p.id_usuario = u.id_usuario
            LEFT JOIN administradores a ON a.id_usuario = u.id_usuario
            ORDER BY u.id_usuario ASC;";

        var dtos = conexao.Query<UsuarioDbDto>(sql);
        return dtos.Select(MapearParaEntidade).ToList();
    }

    // RF26, RF27 - Persistência relacional em duas tabelas (herança Table-per-Type)
    public void Adicionar(Usuario usuario)
    {
        using var conexao = CriarConexao();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sqlUsuario = @"
                INSERT INTO usuarios (id_usuario, nome, login, senha_hash, perfil, ativo, criado_em, ultimo_acesso)
                VALUES (@Id, @Nome, @Login, @SenhaHash, @Perfil, @Ativo, NOW(), NOW());";

            conexao.Execute(sqlUsuario, new {
                usuario.Id,
                usuario.Nome,
                usuario.Login,
                usuario.SenhaHash,
                Perfil = usuario.Perfil.ToString(),
                Ativo = usuario.Ativo ? 1 : 0
            }, transacao);

            if (usuario is ProfissionalSaude prof)
            {
                // Evita colisão na constraint UNIQUE(registro_profissional) quando o usuário não informa CRM próprio
                var registroUnico = string.IsNullOrWhiteSpace(prof.RegistroProfissional) || prof.RegistroProfissional == "CRM-BR"
                    ? $"CRM-PILOTO-{prof.Id}"
                    : prof.RegistroProfissional;

                const string sqlProf = @"
                    INSERT INTO profissionais_saude (id_usuario, perfil, registro_profissional, especialidade)
                    VALUES (@Id, 'Profissional', @RegistroProfissional, @Especialidade);";

                conexao.Execute(sqlProf, new {
                    prof.Id,
                    RegistroProfissional = registroUnico,
                    prof.Especialidade
                }, transacao);
            }
            else if (usuario is Administrador admin)
            {
                const string sqlAdmin = @"
                    INSERT INTO administradores (id_usuario, perfil, cargo)
                    VALUES (@Id, 'Administrador', @Cargo);";

                conexao.Execute(sqlAdmin, new {
                    admin.Id,
                    admin.Cargo
                }, transacao);
            }

            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    // RF26, RF28 - Atualiza o timestamp de último acesso na tabela usuarios
    public void RegistrarUltimoAcesso(int id)
    {
        using var conexao = CriarConexao();
        const string sql = "UPDATE usuarios SET ultimo_acesso = NOW() WHERE id_usuario = @Id;";
        conexao.Execute(sql, new { Id = id });
    }
}
