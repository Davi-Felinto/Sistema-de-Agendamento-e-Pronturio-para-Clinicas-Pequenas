using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySqlConnector;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Domain.Interfaces;

namespace ClinicaApp.Infrastructure.MySQL;

public class MySqlAgendamentoRepository : IAgendamentoRepository
{
    private readonly string _connectionString;

    public MySqlAgendamentoRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    private IDbConnection CriarConexao() => new MySqlConnection(_connectionString);

    private record AgendamentoDbDto(
        int id_agendamento,
        int id_paciente,
        int id_profissional,
        DateTime data_hora_inicio,
        DateTime data_hora_fim,
        string? observacoes,
        string status,
        bool cancelamento_tardio
    );

    private static Agendamento MapearParaEntidade(AgendamentoDbDto dto)
    {
        var status = Enum.TryParse<StatusAgendamento>(dto.status, true, out var parsedStatus)
            ? parsedStatus
            : StatusAgendamento.Pendente;

        return new Agendamento(
            dto.id_agendamento,
            dto.id_paciente,
            dto.id_profissional,
            dto.data_hora_inicio,
            dto.data_hora_fim,
            dto.observacoes,
            status,
            dto.cancelamento_tardio
        );
    }

    // RF06, RF08 - Obter agendamento por ID
    public Agendamento? ObterPorId(int id)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_agendamento, id_paciente, id_profissional, 
                data_hora_inicio, data_hora_fim, observacoes, 
                status, cancelamento_tardio
            FROM agendamentos
            WHERE id_agendamento = @Id;";

        var dto = conexao.QueryFirstOrDefault<AgendamentoDbDto>(sql, new { Id = id });
        return dto != null ? MapearParaEntidade(dto) : null;
    }

    // RN01, RN02 - Consultas do profissional na data especificada para checagem de colisão de horários
    public IEnumerable<Agendamento> ObterPorProfissionalEPeriodo(int profissionalId, DateTime data)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_agendamento, id_paciente, id_profissional, 
                data_hora_inicio, data_hora_fim, observacoes, 
                status, cancelamento_tardio
            FROM agendamentos
            WHERE id_profissional = @ProfissionalId 
              AND DATE(data_hora_inicio) = DATE(@Data)
            ORDER BY data_hora_inicio ASC;";

        var dtos = conexao.Query<AgendamentoDbDto>(sql, new { ProfissionalId = profissionalId, Data = data.Date });
        return dtos.Select(MapearParaEntidade).ToList();
    }

    // RF07 - Histórico de agendamentos por paciente
    public IEnumerable<Agendamento> ObterPorPaciente(int pacienteId)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_agendamento, id_paciente, id_profissional, 
                data_hora_inicio, data_hora_fim, observacoes, 
                status, cancelamento_tardio
            FROM agendamentos
            WHERE id_paciente = @PacienteId
            ORDER BY data_hora_inicio DESC;";

        var dtos = conexao.Query<AgendamentoDbDto>(sql, new { PacienteId = pacienteId });
        return dtos.Select(MapearParaEntidade).ToList();
    }

    // RF06 - Listar todos os agendamentos cadastrados
    public IEnumerable<Agendamento> ListarTodos()
    {
        using var conexao = CriarConexao();
        const string sql = @"
            SELECT 
                id_agendamento, id_paciente, id_profissional, 
                data_hora_inicio, data_hora_fim, observacoes, 
                status, cancelamento_tardio
            FROM agendamentos
            ORDER BY data_hora_inicio ASC;";

        var dtos = conexao.Query<AgendamentoDbDto>(sql);
        return dtos.Select(MapearParaEntidade).ToList();
    }

    // RF06 - Gravar novo agendamento no banco relacional
    public void Adicionar(Agendamento agendamento)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            INSERT INTO agendamentos (
                id_agendamento, id_paciente, id_profissional, data_hora_inicio, 
                data_hora_fim, status, observacoes, cancelamento_tardio, criado_em
            ) VALUES (
                @Id, @PacienteId, @ProfissionalId, @DataHoraInicio, 
                @DataHoraFim, @Status, @Observacoes, @CancelamentoTardio, NOW()
            );";

        conexao.Execute(sql, new {
            agendamento.Id,
            agendamento.PacienteId,
            agendamento.ProfissionalId,
            agendamento.DataHoraInicio,
            agendamento.DataHoraFim,
            Status = agendamento.Status.ToString(),
            agendamento.Observacoes,
            CancelamentoTardio = agendamento.CancelamentoTardio ? 1 : 0
        });
    }

    // RF08, RN10 - Atualizar status (confirmar, cancelar, cancelamento tardio)
    public void Atualizar(Agendamento agendamento)
    {
        using var conexao = CriarConexao();
        const string sql = @"
            UPDATE agendamentos SET
                data_hora_inicio = @DataHoraInicio,
                data_hora_fim = @DataHoraFim,
                status = @Status,
                observacoes = @Observacoes,
                cancelamento_tardio = @CancelamentoTardio,
                data_cancelamento = CASE WHEN @Status = 'Cancelado' AND data_cancelamento IS NULL THEN NOW() ELSE data_cancelamento END
            WHERE id_agendamento = @Id;";

        conexao.Execute(sql, new {
            agendamento.Id,
            agendamento.DataHoraInicio,
            agendamento.DataHoraFim,
            Status = agendamento.Status.ToString(),
            agendamento.Observacoes,
            CancelamentoTardio = agendamento.CancelamentoTardio ? 1 : 0
        });
    }
}
