using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySqlConnector;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.MySQL;

public class MySqlProntuarioRepository : IProntuarioRepository
{
    private readonly string _connectionString;

    public MySqlProntuarioRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    // RF15, RF16 - Obter sessão de prontuário com suas versões imutáveis
    public SessaoProntuario? ObterPorId(int id)
    {
        using var conexao = CriarConexao();
        const string sqlSessao = @"
            SELECT 
                id_sessao AS Id,
                id_paciente AS PacienteId,
                id_agendamento AS AgendamentoId,
                anotacoes_clinicas AS AnotacoesClinicas,
                data_registro AS DataRegistro
            FROM sessoes_prontuario
            WHERE id_sessao = @Id;";

        var sessao = conexao.QueryFirstOrDefault<SessaoProntuario>(sqlSessao, new { Id = id });
        if (sessao == null) return null;

        CarregarVersoes(conexao, sessao);
        return sessao;
    }

    // RF15, RF17 - Obter todo o histórico clínico do paciente
    public IEnumerable<SessaoProntuario> ObterPorPaciente(int pacienteId)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_sessao AS Id,
                id_paciente AS PacienteId,
                id_agendamento AS AgendamentoId,
                anotacoes_clinicas AS AnotacoesClinicas,
                data_registro AS DataRegistro
            FROM sessoes_prontuario
            WHERE id_paciente = @PacienteId
            ORDER BY data_registro ASC;";

        var sessoes = conexao.Query<SessaoProntuario>(sql, new { PacienteId = pacienteId }).ToList();

        foreach (var sessao in sessoes)
        {
            CarregarVersoes(conexao, sessao);
        }

        return sessoes;
    }

    private static void CarregarVersoes(IDbConnection conexao, SessaoProntuario sessao)
    {
        const string sqlVersoes = @"
            SELECT 
                id_versao AS Id,
                texto_anterior AS TextoAnterior,
                data_modificacao AS DataModificacao,
                motivo_alteracao AS MotivoAlteracao
            FROM historico_versoes_prontuario
            WHERE id_sessao = @SessaoId
            ORDER BY numero_versao ASC;";

        var versoes = conexao.Query<VersaoAnotacao>(sqlVersoes, new { SessaoId = sessao.Id });
        foreach (var v in versoes)
        {
            sessao.AdicionarVersaoExistente(v);
        }
    }

    // RF15 - Adicionar nova sessão clínica
    public void Adicionar(SessaoProntuario sessao)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO sessoes_prontuario (
                id_sessao, id_paciente, id_agendamento, data_registro, 
                anotacoes_clinicas, versao_atual
            ) VALUES (
                @Id, @PacienteId, @AgendamentoId, @DataRegistro, 
                @AnotacoesClinicas, 1
            );";

        conexao.Execute(sql, new {
            sessao.Id,
            sessao.PacienteId,
            sessao.AgendamentoId,
            sessao.DataRegistro,
            sessao.AnotacoesClinicas
        });
    }

    // RF16, RN17 - Atualizar anotação com registro imutável de versão
    public void Atualizar(SessaoProntuario sessao)
    {
        using var conexao = CriarConexao();
        conexao.Open();
        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sqlUpdate = @"
                UPDATE sessoes_prontuario SET
                    anotacoes_clinicas = @AnotacoesClinicas,
                    versao_atual = @VersaoAtual,
                    data_ultima_alteracao = NOW()
                WHERE id_sessao = @Id;";

            conexao.Execute(sqlUpdate, new {
                sessao.Id,
                sessao.AnotacoesClinicas,
                VersaoAtual = sessao.HistoricoVersoes.Count + 1
            }, transacao);

            // Persiste as versões que ainda não foram gravadas
            const string sqlExisteVersao = "SELECT numero_versao FROM historico_versoes_prontuario WHERE id_sessao = @SessaoId;";
            var versoesNoBanco = conexao.Query<int>(sqlExisteVersao, new { SessaoId = sessao.Id }, transacao).ToHashSet();

            int numero = 1;
            foreach (var v in sessao.HistoricoVersoes)
            {
                if (!versoesNoBanco.Contains(numero))
                {
                    const string sqlInsertVersao = @"
                        INSERT INTO historico_versoes_prontuario (
                            id_sessao, numero_versao, texto_anterior, 
                            data_modificacao, motivo_alteracao, id_usuario
                        ) VALUES (
                            @SessaoId, @NumeroVersao, @TextoAnterior, 
                            @DataModificacao, @MotivoAlteracao, 1
                        );";

                    conexao.Execute(sqlInsertVersao, new {
                        SessaoId = sessao.Id,
                        NumeroVersao = numero,
                        v.TextoAnterior,
                        v.DataModificacao,
                        v.MotivoAlteracao
                    }, transacao);
                }
                numero++;
            }

            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }
}
