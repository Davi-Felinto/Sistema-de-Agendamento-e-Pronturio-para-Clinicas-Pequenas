using System;
using System.Linq;
using Xunit;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

namespace ClinicaApp.Tests.Services;

public class FinanceiroServiceTests
{
    private readonly InMemoryPagamentoRepository _pagamentoRepository;
    private readonly InMemoryAgendamentoRepository _agendamentoRepository;
    private readonly FinanceiroService _financeiroService;

    public FinanceiroServiceTests()
    {
        _pagamentoRepository = new InMemoryPagamentoRepository();
        _agendamentoRepository = new InMemoryAgendamentoRepository();
        _financeiroService = new FinanceiroService(_pagamentoRepository, _agendamentoRepository);
    }

    private Agendamento CriarAgendamentoValido(int id = 1, int pacienteId = 10, int profissionalId = 5)
    {
        return new Agendamento(
            id: id,
            pacienteId: pacienteId,
            profissionalId: profissionalId,
            dataHoraInicio: DateTime.Today.AddDays(1).AddHours(14),
            dataHoraFim: DateTime.Today.AddDays(1).AddHours(15)
        );
    }

    [Fact]
    public void Deve_Gerar_Cobranca_Com_Sucesso_E_Status_Pendente()
    {
        // Arrange (RF19, RN07)
        var agendamento = CriarAgendamentoValido();
        _agendamentoRepository.Adicionar(agendamento);

        // Act
        var pagamento = _financeiroService.GerarCobranca(id: 1, agendamentoId: agendamento.Id, valor: 250.00m);

        // Assert
        Assert.NotNull(pagamento);
        Assert.Equal(1, pagamento.Id);
        Assert.Equal(agendamento.Id, pagamento.AgendamentoId);
        Assert.Equal(250.00m, pagamento.Valor);
        Assert.Equal(StatusPagamento.Pendente, pagamento.Status);
        Assert.Null(pagamento.Forma);
        Assert.Null(pagamento.DataPagamento);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Gerar_Cobranca_Para_Agendamento_Inexistente()
    {
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _financeiroService.GerarCobranca(id: 1, agendamentoId: 999, valor: 200.00m));

        Assert.Equal("Agendamento não encontrado.", ex.Message);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Gerar_Cobranca_Duplicada_Para_Mesmo_Agendamento()
    {
        // Arrange
        var agendamento = CriarAgendamentoValido();
        _agendamentoRepository.Adicionar(agendamento);
        _financeiroService.GerarCobranca(id: 1, agendamentoId: agendamento.Id, valor: 200.00m);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _financeiroService.GerarCobranca(id: 2, agendamentoId: agendamento.Id, valor: 200.00m));

        Assert.Equal("Já existe uma cobrança cadastrada para este agendamento.", ex.Message);
    }

    [Fact]
    public void Deve_Quitar_Pagamento_Com_Sucesso_E_Atualizar_Status_Para_Pago()
    {
        // Arrange (RF20, RN07, RN08)
        var agendamento = CriarAgendamentoValido();
        _agendamentoRepository.Adicionar(agendamento);
        var pagamento = _financeiroService.GerarCobranca(id: 1, agendamentoId: agendamento.Id, valor: 300.00m);
        var dataPagamento = new DateTime(2026, 10, 15, 10, 30, 0);

        // Act
        var pagamentoQuitado = _financeiroService.QuitarPagamento(
            pagamentoId: pagamento.Id,
            forma: FormaPagamento.Pix,
            dataPagamento: dataPagamento
        );

        // Assert
        Assert.Equal(StatusPagamento.Pago, pagamentoQuitado.Status);
        Assert.Equal(FormaPagamento.Pix, pagamentoQuitado.Forma);
        Assert.Equal(dataPagamento, pagamentoQuitado.DataPagamento);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Ao_Quitar_Pagamento_Inexistente_Ou_Ja_Pago()
    {
        // Act & Assert (Inexistente)
        var exInexistente = Assert.Throws<InvalidOperationException>(() =>
            _financeiroService.QuitarPagamento(999, FormaPagamento.Dinheiro));
        Assert.Equal("Pagamento não encontrado.", exInexistente.Message);

        // Arrange (Já pago)
        var agendamento = CriarAgendamentoValido();
        _agendamentoRepository.Adicionar(agendamento);
        var pagamento = _financeiroService.GerarCobranca(1, agendamento.Id, 150.00m);
        _financeiroService.QuitarPagamento(pagamento.Id, FormaPagamento.CartaoCredito);

        // Act & Assert (Tentativa de pagar novamente)
        var exJaPago = Assert.Throws<InvalidOperationException>(() =>
            _financeiroService.QuitarPagamento(pagamento.Id, FormaPagamento.CartaoCredito));
        Assert.Equal("Este pagamento já foi liquidado anteriormente.", exJaPago.Message);
    }

    [Fact]
    public void Deve_Obter_Pendencias_Gerais_E_Por_Paciente()
    {
        // Arrange (RF21, RN08)
        var agendamentoPaciente1 = CriarAgendamentoValido(id: 1, pacienteId: 10);
        var agendamentoPaciente2 = CriarAgendamentoValido(id: 2, pacienteId: 20);
        _agendamentoRepository.Adicionar(agendamentoPaciente1);
        _agendamentoRepository.Adicionar(agendamentoPaciente2);

        var pag1 = _financeiroService.GerarCobranca(1, agendamentoPaciente1.Id, 100.00m);
        var pag2 = _financeiroService.GerarCobranca(2, agendamentoPaciente2.Id, 200.00m);

        // Quita apenas o segundo
        _financeiroService.QuitarPagamento(pag2.Id, FormaPagamento.Pix);

        // Act
        var todasPendencias = _financeiroService.ObterPagamentosPendentes().ToList();
        var pendenciasPaciente1 = _financeiroService.ObterPendenciasPorPaciente(pacienteId: 10).ToList();
        var pendenciasPaciente2 = _financeiroService.ObterPendenciasPorPaciente(pacienteId: 20).ToList();

        // Assert
        Assert.Single(todasPendencias);
        Assert.Equal(pag1.Id, todasPendencias[0].Id);

        Assert.Single(pendenciasPaciente1);
        Assert.Equal(pag1.Id, pendenciasPaciente1[0].Id);

        Assert.Empty(pendenciasPaciente2);
    }

    [Fact]
    public void Deve_Gerar_Resumo_Financeiro_Mensal_Corretamente()
    {
        // Arrange (RF22, RN09)
        var ag1 = CriarAgendamentoValido(id: 1);
        var ag2 = CriarAgendamentoValido(id: 2);
        var ag3 = CriarAgendamentoValido(id: 3);
        _agendamentoRepository.Adicionar(ag1);
        _agendamentoRepository.Adicionar(ag2);
        _agendamentoRepository.Adicionar(ag3);

        var p1 = _financeiroService.GerarCobranca(1, ag1.Id, 150.00m);
        var p2 = _financeiroService.GerarCobranca(2, ag2.Id, 250.00m);
        var p3 = _financeiroService.GerarCobranca(3, ag3.Id, 100.00m);

        // Paga p1 e p2 em Outubro/2026
        _financeiroService.QuitarPagamento(p1.Id, FormaPagamento.Pix, new DateTime(2026, 10, 5));
        _financeiroService.QuitarPagamento(p2.Id, FormaPagamento.CartaoDebito, new DateTime(2026, 10, 10));
        // p3 permanece pendente

        // Act
        var resumo = _financeiroService.GerarResumoMensal(mes: 10, ano: 2026);

        // Assert
        Assert.Equal(10, resumo.Mes);
        Assert.Equal(2026, resumo.Ano);
        Assert.Equal(400.00m, resumo.TotalRecebido); // 150 + 250
        Assert.Equal(100.00m, resumo.TotalPendente); // 100
        Assert.Equal(2, resumo.QuantidadePagos);
        Assert.Equal(1, resumo.QuantidadePendentes);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Se_Mes_For_Invalido_No_Resumo_Mensal()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _financeiroService.GerarResumoMensal(0, 2026));
        Assert.Throws<ArgumentOutOfRangeException>(() => _financeiroService.GerarResumoMensal(13, 2026));
    }
}

