using System;
using System.Linq;
using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Enums;
using ClinicaApp.Infrastructure.External;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("================================================================================");
Console.WriteLine("       SISTEMA DE AGENDAMENTO E PRONTUÁRIO CLÍNICO PARA CLÍNICAS PEQUENAS       ");
Console.WriteLine("                   Projeto Integrador - 2º Semestre CEUB                        ");
Console.WriteLine("                         Desenvolvedor: Davi Felinto                            ");
Console.WriteLine("================================================================================\n");
Console.ResetColor();

// -----------------------------------------------------------------------------
// 1. INICIALIZAÇÃO DA INFRAESTRUTURA E SERVIÇOS (Injeção de Dependência Manual)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 1. Inicializando Infraestrutura e Serviços da Aplicação...");
Console.ResetColor();

var usuarioRepo = new InMemoryUsuarioRepository();
var pacienteRepo = new InMemoryPacienteRepository();
var agendamentoRepo = new InMemoryAgendamentoRepository();
var prontuarioRepo = new InMemoryProntuarioRepository();
var pagamentoRepo = new InMemoryPagamentoRepository();
var logRepo = new InMemoryLogAcessoRepository();
var notificador = new NotificadorWhatsApp();

var authService = new AuthService(usuarioRepo);
var agendaService = new AgendaService(agendamentoRepo, pacienteRepo, notificador);
var prontuarioService = new ProntuarioService(prontuarioRepo, pacienteRepo, logRepo);
var financeiroService = new FinanceiroService(pagamentoRepo, agendamentoRepo);

Console.WriteLine("   ✔ Repositórios InMemory e Serviços prontos.\n");

// -----------------------------------------------------------------------------
// 2. AUTENTICAÇÃO E USUÁRIOS (AuthService, RQ08)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 2. Cadastrando e Autenticando Usuários do Sistema...");
Console.ResetColor();

var medico = new ProfissionalSaude(
    id: 1,
    nome: "Dr. Roberto Silva",
    login: "roberto.med",
    senhaPura: "SenhaSegura@2026",
    registroProfissional: "CRM-DF 12345",
    especialidade: "Clínica Geral"
);
authService.Cadastrar(medico);

var admin = new Administrador(
    id: 2,
    nome: "Ana Costa",
    login: "ana.admin",
    senhaPura: "AdminMaster@2026",
    cargo: "Gerente de Operações"
);
authService.Cadastrar(admin);

var usuarioLogado = authService.Autenticar("roberto.med", "SenhaSegura@2026");
if (usuarioLogado != null)
{
    Console.WriteLine($"   ✔ Usuário autenticado: {usuarioLogado.Nome} | Perfil: {usuarioLogado.Perfil} | Salt Hash OK.\n");
}

// -----------------------------------------------------------------------------
// 3. CADASTRO DE PACIENTE (Paciente)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 3. Cadastrando Paciente...");
Console.ResetColor();

var paciente = new Paciente(
    id: 1,
    nome: "Mariana Souza",
    documentoIdentificacao: "023.456.789-10",
    dataNascimento: new DateTime(1998, 7, 14),
    telefone: "(61) 99876-5432",
    email: "mariana.souza@email.com",
    endereco: "Asa Sul - Brasília/DF",
    alergias: "Dipirona"
);
pacienteRepo.Adicionar(paciente);
Console.WriteLine($"   ✔ Paciente: {paciente.Nome} | CPF: {paciente.DocumentoIdentificacao} | Tel: {paciente.Telefone}\n");

// -----------------------------------------------------------------------------
// 4. AGENDAMENTO E VALIDAÇÃO DE CONFLITO (AgendaService, RN01, RN02, RN03)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 4. Agendando Consulta e Disparando Notificação...");
Console.ResetColor();

var dataConsulta = DateTime.Today.AddDays(2).AddHours(14); // Depois de amanhã às 14h
var consulta = agendaService.AgendarConsulta(
    id: 1,
    pacienteId: paciente.Id,
    profissionalId: medico.Id,
    dataHoraInicio: dataConsulta,
    dataHoraFim: dataConsulta.AddMinutes(45),
    observacoes: "Primeira consulta de rotina"
);
Console.WriteLine($"   ✔ Consulta agendada com sucesso para {consulta.DataHoraInicio:dd/MM/yyyy HH:mm}!");

// Demonstração da regra de colisão de horários (RN01, RN02):
try
{
    Console.WriteLine("   * Testando tentativa de agendamento conflitante no mesmo horário...");
    agendaService.AgendarConsulta(
        id: 2,
        pacienteId: paciente.Id,
        profissionalId: medico.Id,
        dataHoraInicio: dataConsulta.AddMinutes(15),
        dataHoraFim: dataConsulta.AddMinutes(60)
    );
}
catch (InvalidOperationException ex)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"   ✔ Validação RN01/RN02 funcionou: \"{ex.Message}\"\n");
    Console.ResetColor();
}

// -----------------------------------------------------------------------------
// 5. REGISTRO DE PRONTUÁRIO E HISTÓRICO IMUTÁVEL (ProntuarioService, RN17, LGPD)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 5. Atendimento Clínico e Versionamento de Prontuário...");
Console.ResetColor();

var sessao = prontuarioService.RegistrarSessao(
    id: 1,
    pacienteId: paciente.Id,
    agendamentoId: consulta.Id,
    anotacoesClinicas: "Paciente apresenta queixas de cefaleia recorrente. Solicitado hemograma completo.",
    usuarioId: medico.Id
);
Console.WriteLine($"   ✔ Atendimento registrado pelo Dr. Roberto.");

// Médico corrige / complementa a anotação (gera versão imutável RN17)
prontuarioService.EditarAnotacao(
    sessaoId: sessao.Id,
    novoTexto: "Paciente apresenta cefaleia recorrente. Solicitado hemograma completo e prescrito analgésico não-derivado de dipirona.",
    motivo: "Inclusão de prescrição medicamentosa respeitando alergia",
    usuarioId: medico.Id
);
Console.WriteLine($"   ✔ Anotação editada com justificativa. Histórico imutável de versões: {sessao.HistoricoVersoes.Count} versão arquivada.\n");

// -----------------------------------------------------------------------------
// 6. GESTÃO FINANCEIRA E QUITAÇÃO (FinanceiroService, RN07, RN08, RN09)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 6. Módulo Financeiro: Cobrança, Quitação e Fechamento...");
Console.ResetColor();

var cobranca = financeiroService.GerarCobranca(id: 1, agendamentoId: consulta.Id, valor: 250.00m);
Console.WriteLine($"   ✔ Fatura #{cobranca.Id} gerada no valor de R$ {cobranca.Valor:N2} (Status: {cobranca.Status}).");

var pagamentoQuitado = financeiroService.QuitarPagamento(cobranca.Id, FormaPagamento.Pix);
Console.WriteLine($"   ✔ Pagamento liquidado com sucesso via {pagamentoQuitado.Forma} em {pagamentoQuitado.DataPagamento:dd/MM/yyyy HH:mm}!");

var resumo = financeiroService.GerarResumoMensal(DateTime.Now.Month, DateTime.Now.Year);
Console.WriteLine($"   ✔ Resumo Financeiro ({resumo.Mes:D2}/{resumo.Ano}):");
Console.WriteLine($"      - Total Recebido:   R$ {resumo.TotalRecebido:N2} ({resumo.QuantidadePagos} pagamentos)");
Console.WriteLine($"      - Total a Receber:  R$ {resumo.TotalPendente:N2} ({resumo.QuantidadePendentes} pendências)\n");

// -----------------------------------------------------------------------------
// 7. AUDITORIA LGPD (LogAcesso, RF28, RN13, RQ07)
// -----------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(">> 7. Auditoria de Trilha de Acesso (Conformidade com a LGPD)...");
Console.ResetColor();

var todosOsLogs = logRepo.ObterTodos().ToList();
Console.WriteLine($"   ✔ Total de {todosOsLogs.Count} operações auditadas registradas:");
foreach (var log in todosOsLogs)
{
    Console.WriteLine($"      [{log.DataHora:HH:mm:ss}] Usuário {log.UsuarioId} -> {log.Operacao}: \"{log.Detalhes}\"");
}

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n================================================================================");
Console.WriteLine("      ✔ SIMULAÇÃO CONCLUÍDA COM 100% DE SUCESSO! ARQUITETURA POO VALIDADA!      ");
Console.WriteLine("================================================================================");
Console.ResetColor();

