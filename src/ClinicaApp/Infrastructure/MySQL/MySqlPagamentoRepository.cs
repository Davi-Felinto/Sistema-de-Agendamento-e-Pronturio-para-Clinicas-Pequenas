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

public class MySqlPagamentoRepository : IPagamentoRepository
{
    private readonly string _connectionString;

    public MySqlPagamentoRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    private record PagamentoDto(
        int id_pagamento,
        int id_agendamento,
        decimal valor,
        string status,
        string? forma_pagamento,
        DateTime? data_pagamento
    );

    private static Pagamento MapearParaEntidade(PagamentoDto dto)
    {
        var pag = new Pagamento(dto.id_pagamento, dto.id_agendamento, dto.valor);
        if (dto.status == "Pago" && !string.IsNullOrWhiteSpace(dto.forma_pagamento) &&
            Enum.TryParse<FormaPagamento>(dto.forma_pagamento, true, out var forma))
        {
            pag.RegistrarPagamento(forma, dto.data_pagamento);
        }
        return pag;
    }

    // RF19, RF20 - Obter cobrança por ID
    public Pagamento? ObterPorId(int id)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento
            FROM pagamentos
            WHERE id_pagamento = @Id;";

        var dto = conexao.QueryFirstOrDefault<PagamentoDto>(sql, new { Id = id });
        return dto != null ? MapearParaEntidade(dto) : null;
    }

    // RN07 - Obter cobrança vinculada a um agendamento específico
    public Pagamento? ObterPorAgendamentoId(int agendamentoId)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento
            FROM pagamentos
            WHERE id_agendamento = @AgendamentoId;";

        var dto = conexao.QueryFirstOrDefault<PagamentoDto>(sql, new { AgendamentoId = agendamentoId });
        return dto != null ? MapearParaEntidade(dto) : null;
    }

    // RF21, RF22 - Listar todas as cobranças para resumo e conciliação
    public IEnumerable<Pagamento> ListarTodos()
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento
            FROM pagamentos
            ORDER BY criado_em DESC;";

        var dtos = conexao.Query<PagamentoDto>(sql);
        return dtos.Select(MapearParaEntidade).ToList();
    }

    // RF19, RN07 - Inserir cobrança vinculada à consulta
    public void Adicionar(Pagamento pagamento)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO pagamentos (
                id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento, criado_em
            ) VALUES (
                @Id, @AgendamentoId, @Valor, @Status, @FormaPagamento, @DataPagamento, NOW()
            );";

        conexao.Execute(sql, new {
            pagamento.Id,
            pagamento.AgendamentoId,
            pagamento.Valor,
            Status = pagamento.Status.ToString(),
            FormaPagamento = pagamento.Forma?.ToString(),
            pagamento.DataPagamento
        });
    }

    // RF20, RN08 - Quitar cobrança com timestamp e forma de pagamento
    public void Atualizar(Pagamento pagamento)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            UPDATE pagamentos SET
                status = @Status,
                forma_pagamento = @FormaPagamento,
                data_pagamento = @DataPagamento
            WHERE id_pagamento = @Id;";

        conexao.Execute(sql, new {
            pagamento.Id,
            Status = pagamento.Status.ToString(),
            FormaPagamento = pagamento.Forma?.ToString(),
            pagamento.DataPagamento
        });
    }
}
