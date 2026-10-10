using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySqlConnector;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.MySQL;

public class MySqlLogAcessoRepository : ILogAcessoRepository
{
    private readonly string _connectionString;

    public MySqlLogAcessoRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    // RF28, RN13, RQ07 - Gravação imutável de log de auditoria LGPD
    public void Salvar(LogAcesso log)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO logs_acesso (
                id_log, id_usuario, operacao, detalhes, data_hora
            ) VALUES (
                @Id, @UsuarioId, @Operacao, @Detalhes, @DataHora
            );";

        conexao.Execute(sql, new {
            log.Id,
            log.UsuarioId,
            log.Operacao,
            log.Detalhes,
            log.DataHora
        });
    }

    // RF28 - Listagem de auditoria para administradores
    public IEnumerable<LogAcesso> ObterTodos()
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_log AS Id,
                id_usuario AS UsuarioId,
                operacao AS Operacao,
                detalhes AS Detalhes,
                data_hora AS DataHora
            FROM logs_acesso
            ORDER BY data_hora DESC;";

        return conexao.Query<LogAcesso>(sql);
    }
}
