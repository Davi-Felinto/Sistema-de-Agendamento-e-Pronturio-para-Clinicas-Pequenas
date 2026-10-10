using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySqlConnector;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.MySQL;

public class MySqlPacienteRepository : IPacienteRepository
{
    private readonly string _connectionString;

    public MySqlPacienteRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    // RF01, RF04 - Obter por ID
    public Paciente? ObterPorId(int id)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_paciente AS Id,
                nome AS Nome,
                documento_identificacao AS DocumentoIdentificacao,
                data_nascimento AS DataNascimento,
                telefone AS Telefone,
                email AS Email,
                endereco AS Endereco,
                alergias AS Alergias,
                condicoes_preexistentes AS CondicoesPreexistentes,
                ativo AS Ativo
            FROM pacientes 
            WHERE id_paciente = @Id;";

        return conexao.QueryFirstOrDefault<Paciente>(sql, new { Id = id });
    }

    // RF05 - Obter por Documento (CPF / RG)
    public Paciente? ObterPorDocumento(string documento)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_paciente AS Id,
                nome AS Nome,
                documento_identificacao AS DocumentoIdentificacao,
                data_nascimento AS DataNascimento,
                telefone AS Telefone,
                email AS Email,
                endereco AS Endereco,
                alergias AS Alergias,
                condicoes_preexistentes AS CondicoesPreexistentes,
                ativo AS Ativo
            FROM pacientes 
            WHERE documento_identificacao = @Documento;";

        return conexao.QueryFirstOrDefault<Paciente>(sql, new { Documento = documento });
    }

    // RF02, RN16 - Listar todos os pacientes com filtro opcional de ativos
    public IEnumerable<Paciente> ListarTodos(bool apenasAtivos = true)
    {
        using var conexao = CriarConexao();
        string sql = @"
            SELECT 
                id_paciente AS Id,
                nome AS Nome,
                documento_identificacao AS DocumentoIdentificacao,
                data_nascimento AS DataNascimento,
                telefone AS Telefone,
                email AS Email,
                endereco AS Endereco,
                alergias AS Alergias,
                condicoes_preexistentes AS CondicoesPreexistentes,
                ativo AS Ativo
            FROM pacientes";

        if (apenasAtivos)
        {
            sql += " WHERE ativo = 1";
        }

        sql += " ORDER BY nome ASC;";

        return conexao.Query<Paciente>(sql);
    }

    // RF01 - Cadastrar novo paciente
    public void Adicionar(Paciente paciente)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO pacientes (
                id_paciente, nome, documento_identificacao, data_nascimento, 
                telefone, email, endereco, alergias, condicoes_preexistentes, ativo, data_cadastro
            ) VALUES (
                @Id, @Nome, @DocumentoIdentificacao, @DataNascimento, 
                @Telefone, @Email, @Endereco, @Alergias, @CondicoesPreexistentes, @Ativo, NOW()
            );";

        conexao.Execute(sql, new {
            paciente.Id,
            paciente.Nome,
            paciente.DocumentoIdentificacao,
            paciente.DataNascimento,
            paciente.Telefone,
            paciente.Email,
            paciente.Endereco,
            paciente.Alergias,
            paciente.CondicoesPreexistentes,
            Ativo = paciente.Ativo ? 1 : 0
        });
    }

    // RF03, RN16, RQ03 - Atualizar dados e inativar com anonimização LGPD
    public void Atualizar(Paciente paciente)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            UPDATE pacientes SET
                nome = @Nome,
                documento_identificacao = @DocumentoIdentificacao,
                telefone = @Telefone,
                email = @Email,
                endereco = @Endereco,
                alergias = @Alergias,
                condicoes_preexistentes = @CondicoesPreexistentes,
                ativo = @Ativo,
                data_inativacao = CASE WHEN @Ativo = 0 AND data_inativacao IS NULL THEN NOW() ELSE data_inativacao END,
                anonimizado_em = CASE WHEN @Ativo = 0 AND anonimizado_em IS NULL THEN NOW() ELSE anonimizado_em END
            WHERE id_paciente = @Id;";

        conexao.Execute(sql, new {
            paciente.Id,
            paciente.Nome,
            paciente.DocumentoIdentificacao,
            paciente.Telefone,
            paciente.Email,
            paciente.Endereco,
            paciente.Alergias,
            paciente.CondicoesPreexistentes,
            Ativo = paciente.Ativo ? 1 : 0
        });
    }
}
