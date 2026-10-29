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

    // RF28, RN13, RQ07 - Gravação imutável de log de auditoria LGPD com IP de origem
    public void Salvar(LogAcesso log)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO logs_acesso (
                id_usuario, operacao, detalhes, ip_origem, data_hora
            ) VALUES (
                @UsuarioId, @Operacao, @Detalhes, @IpOrigem, @DataHora
            );";

        conexao.Execute(sql, new {
            log.UsuarioId,
            log.Operacao,
            log.Detalhes,
            log.IpOrigem,
            log.DataHora
        });
    }

    // RF28 - Listagem de auditoria convertendo id_log (BIGINT) para Int32 compatível com Dapper
    public IEnumerable<LogAcesso> ObterTodos()
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                CAST(id_log AS SIGNED) AS Id,
                id_usuario AS UsuarioId,
                operacao AS Operacao,
                COALESCE(detalhes, '') AS Detalhes,
                ip_origem AS IpOrigem,
                data_hora AS DataHora
            FROM logs_acesso
            ORDER BY data_hora DESC
            LIMIT 250;";

        return conexao.Query<LogAcesso>(sql);
    }
}
