using System;

namespace ClinicaApp.Domain.Entities;

public class VersaoAnotacao
{
    public int Id { get; private set; }
    public string TextoAnterior { get; private set; }
    public DateTime DataModificacao { get; private set; }
    public string MotivoAlteracao { get; private set; }

    public VersaoAnotacao(
        int id,
        string textoAnterior, 
        DateTime dataModificacao,
        string motivoAlteracao)
    {
        Id = id;
        TextoAnterior = textoAnterior;
        DataModificacao = dataModificacao;
        MotivoAlteracao = motivoAlteracao;
    }
}