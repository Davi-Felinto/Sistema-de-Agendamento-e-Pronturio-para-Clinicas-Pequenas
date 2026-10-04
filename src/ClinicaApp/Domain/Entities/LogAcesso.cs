using System;

namespace ClinicaApp.Domain.Entities;

/// <summary>
/// RF28, RN13 e RQ07 (LGPD): Registro imutável de auditoria de acessos
/// e operações sensíveis em dados de prontuário e pacientes.
/// </summary>
public class LogAcesso
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public string Operacao { get; private set; }
    public string Detalhes { get; private set; }
    public DateTime DataHora { get; private set; }

    public LogAcesso(int id, int usuarioId, string operacao, string detalhes, DateTime? dataHora = null)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("Id do usuário deve ser válido.", nameof(usuarioId));

        if (string.IsNullOrWhiteSpace(operacao))
            throw new ArgumentException("Operação é obrigatória para o log de auditoria.", nameof(operacao));

        Id = id;
        UsuarioId = usuarioId;
        Operacao = operacao;
        Detalhes = detalhes ?? string.Empty;
        DataHora = dataHora ?? DateTime.Now;
    }
}
