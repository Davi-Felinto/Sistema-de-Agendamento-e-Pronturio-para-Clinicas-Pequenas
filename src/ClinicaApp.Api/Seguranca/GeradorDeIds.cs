namespace ClinicaApp.Api.Seguranca;

/// <summary>
/// Sequência de IDs para os repositórios InMemory (que não têm auto incremento como o MySQL).
/// Interlocked garante que duas requisições simultâneas nunca recebam o mesmo ID.
/// </summary>
public class GeradorDeIds
{
    // Começa em 1000 para não colidir com os IDs dos dados iniciais
    private int _paciente = 1000;
    private int _agendamento = 1000;
    private int _sessao = 1000;
    private int _pagamento = 1000;

    public int ProximoPaciente() => Interlocked.Increment(ref _paciente);
    public int ProximoAgendamento() => Interlocked.Increment(ref _agendamento);
    public int ProximaSessao() => Interlocked.Increment(ref _sessao);
    public int ProximoPagamento() => Interlocked.Increment(ref _pagamento);
}
