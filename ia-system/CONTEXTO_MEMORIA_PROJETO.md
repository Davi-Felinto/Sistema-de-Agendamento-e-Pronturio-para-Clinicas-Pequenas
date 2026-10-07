# 🧠 MEMÓRIA VIVA DO PROJETO INTEGRADOR
> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> *Arquivo de Sincronização e Memória Universal entre Agentes de IA e IDEs*  
> **Última Atualização:** 07/10/2026 | **Responsável pelo Projeto:** Davi Felinto

---

## ⚠️ PROTOCOLO OBRIGATÓRIO PARA QUALQUER AGENTE DE IA
> **SE VOCÊ É UMA INTELIGÊNCIA ARTIFICIAL (Cursor, Claude, ChatGPT/Codex, Copilot, Windsurf, Antigravity, Aider, etc.):**
> 1. **LEIA ESTE ARQUIVO ANTES DE RESPONDER:** Todo o contexto do projeto, regras pedagógicas e estado atual estão aqui.
> 2. **O DAVI DIGITA O CÓDIGO:** NUNCA modifique ou crie arquivos de código do projeto por conta própria, a não ser que ele peça expressamente ("faça você", "crie para mim", "arrume"). Apresente a lógica e o snippet para o Davi digitar.
> 3. **EXPLIQUE ANTES DE APRESENTAR CÓDIGO:** Sempre explique a regra de negócio (`RNxx`, `RFxx`, `RQxx`) e a decisão técnica antes do trecho de código.
> 4. **REVISE SINTAXE E VÍCIOS DE PYTHON:** O Davi vem de Python e está aprendendo C#. Verifique sempre: ponto e vírgula `;`, parênteses em `if (...)`, propriedades em PascalCase, operador de coalescência nula `??`, tipos explícitos.
> 5. **ATUALIZE ESTE ARQUIVO AO FINAL DA SUA SESSÃO:** Ao concluir qualquer funcionalidade, decisão ou bloco, atualize a seção **"4. Status de Execução e Roadmap"** e adicione um registro no **"Histórico de Atualizações dos Agentes"** no final deste arquivo.

---

## 1. Perfil do Desenvolvedor e Contexto Acadêmico
- **Desenvolvedor:** Davi Felinto (CEUB - Centro Universitário de Brasília, 2º semestre de Engenharia de Software).
- **Atuação:** OROS Soluções Educacionais (gestão de dados e plataformas educacionais).
- **Background técnico:** Forte em Python, aprendendo C# e POO especificamente para este projeto.
- **Portfólio:** `github.com/Davi-Felinto`

### Disciplinas Integradas no Projeto:
1. **Engenharia de Requisitos:** Concluída formalmente. Especificação com 28 RFs, 17 RNs, 16 RQs (ISO/IEC 25010), BPMN e LGPD.
2. **Programação Orientada a Objetos (C# / .NET 8 / .NET 10):** **FASE ATIVA ATUAL**. Foco em Clean Architecture simplificada, Domain-Driven Design pragmático, TDD com xUnit.
3. **Banco de Dados II (MySQL):** Fase seguinte. Graças ao Repository Pattern adotado no C#, os repositórios em memória serão substituídos por implementações em MySQL com Dapper ou Entity Framework Core sem alterar 1 linha de regra de negócio do Domínio.
4. **Desenvolvimento de Interface Web:** Interface protótipo/MVP em HTML/CSS/JS publicada no GitHub Pages (`index.html`).

---

## 2. Diretrizes de Código e Regras Pedagógicas

1. **Método de Trabalho:**
   - **TDD (Test-Driven Development):** Testes unitários com xUnit (`ClinicaApp.Tests`).
   - **Sem "Over-engineering":** Arquitetura limpa e pragmática, sem camadas desnecessárias para o porte do projeto.
   - **Comentários nos códigos:** Apenas para rastreabilidade de requisitos (`// RN01`, `// RQ08`, `// RF12`). NUNCA comente variáveis óbvias.
   - **Validações defensivas:** Lançar exceções específicas (`ArgumentException`, `InvalidOperationException`) com mensagens amigáveis (RQ11).

2. **Diferenças Críticas Python ➔ C# para revisar:**
   - Terminar todas as declarações com ponto e vírgula `;`.
   - Condicionais exigem parênteses: `if (condicao)` e não `if condicao:`.
   - Propriedades e métodos públicos sempre em `PascalCase` (ex: `DataHoraInicio`, e não `dataHoraInicio`).
   - Coalescência nula: `variavel ?? throw new ArgumentNullException(nameof(variavel))`.

---

## 3. Arquitetura da Solução (`src/`)

```
src/
├── ClinicaApp.slnx
├── ClinicaApp/
│   ├── Domain/
│   │   ├── Entities/
│   │   │   ├── Paciente.cs (RF01-RF05, RN16, RQ03)
│   │   │   ├── Agendamento.cs (RF06-RF11, RN01, RN02, RN10)
│   │   │   ├── SessaoProntuario.cs (RF15-RF18, RN05, RN06, RN17)
│   │   │   ├── VersaoAnotacao.cs (RN17 - histórico de versões)
│   │   │   ├── Pagamento.cs (RF19-RF22, RN07, RN08)
│   │   │   ├── Notificacao.cs (RF12-RF14, RN03, RN04, RN14, RN15)
│   │   │   ├── LogAcesso.cs (RF28, RN13, RQ07 - auditoria LGPD)
│   │   │   ├── Usuario.cs (RF26, RF27, RQ06, RQ08)
│   │   │   ├── ProfissionalSaude.cs (Herda de Usuario)
│   │   │   └── Administrador.cs (Herda de Usuario)
│   │   ├── Enums/
│   │   │   ├── PerfilUsuario.cs (Profissional, Administrador)
│   │   │   ├── StatusAgendamento.cs (Agendado, Concluido, Cancelado, CanceladoTardio)
│   │   │   ├── StatusPagamento.cs (Pendente, Pago)
│   │   │   ├── FormaPagamento.cs (Pix, Dinheiro, CartaoCredito, CartaoDebito)
│   │   │   ├── TipoNotificacao.cs (Confirmacao, Lembrete)
│   │   │   ├── StatusNotificacao.cs (Pendente, Enviado, Falha, Reenviado)
│   │   │   └── CanalNotificacao.cs (WhatsApp)
│   │   └── Interfaces/
│   │       ├── IPacienteRepository.cs
│   │       ├── IAgendamentoRepository.cs
│   │       ├── IProntuarioRepository.cs
│   │       ├── IPagamentoRepository.cs
│   │       ├── IUsuarioRepository.cs
│   │       ├── ILogAcessoRepository.cs
│   │       └── INotificador.cs
│   ├── Infrastructure/
│   │   ├── InMemory/
│   │   │   ├── InMemoryPacienteRepository.cs
│   │   │   ├── InMemoryAgendamentoRepository.cs
│   │   │   ├── InMemoryProntuarioRepository.cs
│   │   │   ├── InMemoryPagamentoRepository.cs
│   │   │   ├── InMemoryUsuarioRepository.cs
│   │   │   └── InMemoryLogAcessoRepository.cs
│   │   └── External/
│   │       └── NotificadorWhatsApp.cs (implementa INotificador)
│   └── Services/
│       ├── AuthService.cs (RF26, RQ08 - SHA-256 + Salt)
│       ├── AgendaService.cs (RF06, RF08, RN01, RN02, RN03, RN10) - EM ANDAMENTO
│       ├── ProntuarioService.cs (RF15, RF16, RN05, RN06, RN13, RN17) - PENDENTE
│       └── FinanceiroService.cs (RF19-RF22, RN07-RN09) - PENDENTE
└── ClinicaApp.Tests/
    ├── Domain/ (7 arquivos de teste cobrindo todas as entidades)
    └── Services/
        └── AuthServiceTests.cs (7 testes unitários passando)
```

---

## 4. Status de Execução e Roadmap

### ✅ Bloco 1: Entidades e Enums de Domínio
- **Status:** 100% Concluído.
- **Testes:** 23 testes unitários de domínio passando.
- Validações de regras fundamentais embutidas nas próprias entidades (ex.: `Agendamento.TemConflito()`, `Agendamento.Cancelar()`, `Paciente.Inativar()`, `Usuario.VerificarSenha()`).

### ✅ Bloco 2: Repositórios e Notificações (Infraestrutura)
- **Status:** 100% Concluído.
- Interfaces em `Domain/Interfaces` desacoplam a persistência do domínio.
- Repositórios `InMemory*` implementados com listas `thread-safe` e simulação idêntica à de banco de dados relacional.
- `NotificadorWhatsApp` implementado como canal de saída externo.

### 🔄 Bloco 3: Serviços de Aplicação (Application Services)
- `AuthService.cs`: ✅ 100% Concluído. Possui hash criptográfico com salt (RQ08), validação de e-mail duplicado e 7 testes unitários cobrindo todos os cenários.
- `AgendaService.cs`: ✅ 100% Concluído.
  - Regras atendidas: agendamento com validação de paciente ativo (RN16), colisão/conflito de horários (RN01, RN02), notificação imediata via WhatsApp (RF12, RN03) e cancelamento tardio com menos de 24 horas (RF08, RN10).
  - Testes: 8 testes unitários passando em `ClinicaApp.Tests/Services/AgendaServiceTests.cs`.
- `ProntuarioService.cs`: ⏳ **Próximo da fila**.
  - Registrar sessão (RF15, RN05), editar anotação com histórico (RF16, RN17) e gravar log de acesso para auditoria LGPD via `ILogAcessoRepository` (RN13, RQ07).
- `FinanceiroService.cs`: ⏳ Pendente.
  - Registrar pagamento (RF19, RF20, RN07), consultar pendências (RF21, RN08) e gerar resumo financeiro mensal (RF22, RN09).

### 🌐 Bloco 4: Web / Interface
- `index.html` na raiz do projeto, implantado no GitHub Pages:
  `https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/`

---

## 5. Matriz de Requisitos Rastreáveis

| Código | Descrição Resumida | Localização no Código |
|---|---|---|
| **RF01-RF05** | CRUD e busca de Pacientes | `Paciente.cs`, `IPacienteRepository.cs` |
| **RF06-RF11** | Agendamento, remarcação, cancelamento e conflitos | `Agendamento.cs`, `AgendaService.cs` |
| **RF12-RF14** | Notificações via WhatsApp e retentativas | `Notificacao.cs`, `INotificador.cs`, `AgendaService.cs` |
| **RF15-RF18** | Prontuário, anotações de sessão e histórico de versões | `SessaoProntuario.cs`, `VersaoAnotacao.cs`, `ProntuarioService.cs` |
| **RF19-RF22** | Pagamentos e fechamento financeiro | `Pagamento.cs`, `FinanceiroService.cs` |
| **RF26-RF28** | Autenticação, perfis e log de acesso | `Usuario.cs`, `AuthService.cs`, `LogAcesso.cs` |
| **RN01-RN02** | Verificação de conflito de horários de consulta | `Agendamento.TemConflito()`, `AgendaService.AgendarConsulta()` |
| **RN03-RN04** | Confirmação imediata e lembrete 24h antes | `AgendaService.cs`, `Notificacao.cs` |
| **RN07-RN08** | Registro de pagamento e manutenção de pendências | `Pagamento.cs`, `FinanceiroService.cs` |
| **RN10** | Cancelamento tardio (< 24h antes) | `Agendamento.Cancelar()`, `AgendaService.CancelarConsulta()` |
| **RN13, RQ07**| Registro de auditoria ao acessar prontuário (LGPD) | `ILogAcessoRepository.cs`, `ProntuarioService.cs` |
| **RN16, RQ03**| Inativação lógica e anonimização de dados pessoais | `Paciente.InativarComAnonimizacao()`, `AgendaService.cs` |
| **RN17** | Histórico imutável de versões de anotações | `SessaoProntuario.AtualizarAnotacao()` |
| **RQ08** | Hash seguro de senhas com Salt | `AuthService.GerarHashSenha()` |

---

## 6. Histórico de Atualizações dos Agentes

| Data | Agente / Ferramenta | Ações Realizadas | Próximo Passo Registrado |
|---|---|---|---|
| 07/10/2026 | Antigravity AI | Concluiu Bloco 1, 2 e `AuthService` com testes. Criou ecossistema de memória universal (`AGENTS.md`, `.cursorrules`, `.github/copilot-instructions.md`, `CLAUDE.md`, `CONTEXTO_MEMORIA_PROJETO.md`). | Davi terminar `AgendaService.cs`. |
| 07/10/2026 | Antigravity AI | Corrigiu e finalizou `AgendaService.cs`, criou suíte completa `AgendaServiceTests.cs` (8 testes). Total de 38 testes passando. Organizou arquivos e atualizou documentação. | Implementar `ProntuarioService.cs` com auditoria LGPD (`ILogAcessoRepository`) e histórico de versões (`VersaoAnotacao`). |

*(Todo novo agente que assumir o projeto deve acrescentar uma linha nesta tabela ao final de sua sessão).*
