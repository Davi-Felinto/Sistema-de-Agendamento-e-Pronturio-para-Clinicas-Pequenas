# 🧠 MEMÓRIA VIVA DO PROJETO INTEGRADOR
> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> *Arquivo de Sincronização e Memória Universal entre Agentes de IA e IDEs*  
> **Última Atualização:** 10/10/2026 | **Responsável pelo Projeto:** Davi Felinto
> 📌 **Este arquivo é a memória COMPARTILHADA (estado do projeto).** Perfis, preferências e "onde parou" de cada pessoa ficam em `ia-system/membros/<NOME>.md`.

---

## ⚠️ PROTOCOLO OBRIGATÓRIO PARA QUALQUER AGENTE DE IA
> **SE VOCÊ É UMA INTELIGÊNCIA ARTIFICIAL (Cursor, Claude, ChatGPT/Codex, Copilot, Windsurf, Antigravity, Aider, etc.):**
> 1. **IDENTIFIQUE QUEM ESTÁ FALANDO:** Se a pessoa ainda não se identificou na conversa, pergunte o nome antes de agir. (Dica: `git config user.name` pode sugerir, mas confirme.)
> 2. **CARREGUE DUAS MEMÓRIAS:** este arquivo (compartilhado) + a memória pessoal em `ia-system/membros/` (ver tabela da Seção 0).
> 3. **SIGA AS REGRAS DA PESSOA:** regras pedagógicas (quem digita o código, vícios a revisar etc.) estão na memória pessoal de cada um.
> 4. **EXPLIQUE ANTES DE APRESENTAR CÓDIGO:** Sempre explique a regra de negócio (`RNxx`, `RFxx`, `RQxx`) e a decisão técnica antes do trecho de código.
> 5. **ATUALIZE AO FINAL DA SESSÃO:**
>    - Memória **pessoal**: "Onde parou" + linha no histórico pessoal.
>    - Este arquivo: somente se o **estado do projeto** mudou (Seção 4) + linha no "Histórico de Atualizações dos Agentes" indicando **quem** era o usuário.

---

## 0. Equipe e Memórias Pessoais

| Membro | Papel / Disciplina principal | Memória pessoal |
|---|---|---|
| **Davi Felinto** | Dev principal, POO C#/.NET, API, arquitetura e integração front ↔ API | [`membros/DAVI.md`](membros/DAVI.md) |
| **Miguel** | Desenvolvimento de Interface Web — apenas as telas (melhorias visuais/UX do `index.html`) | [`membros/MIGUEL.md`](membros/MIGUEL.md) |
| **Isaac** | Apoio em Banco de Dados II (MySQL) | *(a criar — `membros/ISAAC.md`)* |
| **Lucas** | Engenharia de Requisitos | *(a criar — `membros/LUCAS.md`)* |

> Se a pessoa não tiver memória pessoal ainda, avise e use só a memória compartilhada (sem criar o arquivo sem o Davi pedir).

---

## 1. Contexto Acadêmico
- **Instituição:** CEUB - Centro Universitário de Brasília, Engenharia de Software (2º semestre).
- **Responsável pelo projeto:** Davi Felinto (`github.com/Davi-Felinto`).

### Disciplinas Integradas no Projeto:
1. **Engenharia de Requisitos:** Concluída formalmente. Especificação com 28 RFs, 17 RNs, 16 RQs (ISO/IEC 25010), BPMN e LGPD.
2. **Programação Orientada a Objetos (C# / .NET 8 / .NET 10):** **100% CONCLUÍDO**. Foco em Clean Architecture simplificada, Domain-Driven Design pragmático, TDD com xUnit (52 testes).
3. **Banco de Dados II (MySQL):** **FASE ATIVA ATUAL**. Modelo lógico relacional com 15 tabelas no MySQL Workbench (`clínica.mwb`), DDL (`01_schema_ddl.sql`) e DML (`02_dados_iniciais.sql`).
4. **Desenvolvimento de Interface Web:** Interface MVP em HTML/CSS/JS com Tailwind, React e suporte híbrido integrada à API e no GitHub Pages (`index.html`).

---

## 2. Diretrizes de Código (valem para toda a equipe)

1. **Método de Trabalho:**
   - **TDD (Test-Driven Development):** Testes unitários com xUnit (`ClinicaApp.Tests`).
   - **Sem "Over-engineering":** Arquitetura limpa e pragmática, sem camadas desnecessárias para o porte do projeto.
   - **Comentários nos códigos:** Apenas para rastreabilidade de requisitos (`// RN01`, `// RQ08`, `// RF12`). NUNCA comente variáveis óbvias.
   - **Validações defensivas:** Lançar exceções específicas (`ArgumentException`, `InvalidOperationException`) com mensagens amigáveis (RQ11).

2. **Regras pedagógicas individuais** (quem digita o código, vícios a revisar etc.): ver a memória pessoal de cada membro em `ia-system/membros/`.

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
│   │   │   ├── StatusAgendamento.cs (Pendente, Confirmado, Remarcado, Cancelado, Concluido)
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
│       ├── AgendaService.cs (RF06, RF08, RN01, RN02, RN03, RN10)
│       ├── ProntuarioService.cs (RF15, RF16, RN05, RN06, RN13, RN17)
│       └── FinanceiroService.cs (RF19-RF22, RN07-RN09)
├── ClinicaApp.Api/
│   ├── Controllers/
│   │   ├── AgendamentosController.cs
│   │   ├── AuthController.cs
│   │   ├── FinanceiroController.cs
│   │   ├── LogsController.cs
│   │   ├── PacientesController.cs
│   │   └── ProntuariosController.cs
│   ├── Data/DadosIniciais.cs
│   ├── Program.cs
│   └── wwwroot/
└── ClinicaApp.Tests/
    ├── Domain/ (7 arquivos de teste cobrindo todas as entidades)
    └── Services/ (AuthServiceTests, AgendaServiceTests, ProntuarioServiceTests, FinanceiroServiceTests)
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

### ✅ Bloco 3: Serviços de Aplicação (Application Services) — 100% Concluído
- `AuthService.cs`: ✅ 100% Concluído. Possui hash criptográfico com salt (RQ08), validação de e-mail duplicado e 7 testes unitários cobrindo todos os cenários.
- `AgendaService.cs`: ✅ 100% Concluído.
  - Regras atendidas: agendamento com validação de paciente ativo (RN16), colisão/conflito de horários (RN01, RN02), notificação imediata via WhatsApp (RF12, RN03) e cancelamento tardio com menos de 24 horas (RF08, RN10).
  - Testes: 8 testes unitários passando em `ClinicaApp.Tests/Services/AgendaServiceTests.cs`.
- `ProntuarioService.cs`: ✅ 100% Concluído.
  - Regras atendidas: registro de sessão clínica com paciente ativo (RF15, RN05, RN16), edição com versionamento imutável (RF16, RN17) e trilha de auditoria LGPD em todas as operações de leitura e escrita (RF28, RN13, RQ07).
  - Testes: 6 testes unitários passando em `ClinicaApp.Tests/Services/ProntuarioServiceTests.cs`.
- `FinanceiroService.cs`: ✅ 100% Concluído.
  - Regras atendidas: geração de cobrança vinculada a agendamento sem duplicidade (RF19, RN07), quitação com registro de timestamp e forma de pagamento (RF20, RN07, RN08), consulta de pendências gerais e filtradas por paciente (RF21, RN08) e resumo financeiro mensal consolidado (RF22, RN09).
  - Testes: 8 testes unitários passando em `ClinicaApp.Tests/Services/FinanceiroServiceTests.cs`.

### ✅ Bloco 4: Web / Interface
- `index.html` na raiz do projeto, implantado no GitHub Pages:
  `https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/`
- Servido também pela API C# com sincronização direta em `src/ClinicaApp.Api/wwwroot/index.html`.
- Suporte a conexão assíncrona com `api.js` e fallback transparente para `localStorage`.

### ✅ Bloco 5: MVP Visual Integrado (API + index.html) — 100% CONCLUÍDO
- Apresentação funcional integrada servida pela API ASP.NET Core (`ClinicaApp.Api`) via `wwwroot` com um único `dotnet run` e fallback transparente para localStorage quando hospedado no GitHub Pages.
- Controllers em ASP.NET Core com DI via construtor, DTOs explícitos e CORS ativado.

### 🚀 Bloco 6: Banco de Dados II (MySQL) — FASE ATIVA
- Modelo lógico relacional entregue pelo Isaac em `bancodedadosclinica/clínica.mwb` (15 tabelas).
- Modelo auditado e atualizado diretamente no Workbench com 4 otimizações técnicas.
- Criados `01_schema_ddl.sql` e `02_dados_iniciais.sql` para deploy no MySQL 8+.

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
| 07/10/2026 | Antigravity AI | Implementou `ProntuarioService.cs` e suíte completa `ProntuarioServiceTests.cs` (6 testes). Total de 44 testes passando. Ensinou execução de testes no Visual Studio. | Implementar `FinanceiroService.cs` (RF19-RF22, RN07-RN09) e seus testes unitários. |
| 07/10/2026 | Antigravity AI | Concluiu `FinanceiroService.cs` e testes (8 testes). Implementou simulação interativa ponta a ponta no `Program.cs`. 52 testes unitários passando (100% de cobertura de POO). | Avançar para a disciplina de Banco de Dados II (MySQL - Modelagem DDL e repositórios relacionais). |
| 08/10/2026 | Antigravity AI | Davi optou por MVP visual antes do MySQL. Verificado projeto `ClinicaApp.Api` (build OK, 52 testes passando). Commits: `027baa0` (Prontuario/Financeiro) e `0492e68` (scaffold da API). Não foi feito push. | Etapa 2 do Bloco 5: configurar DI Singleton + seed + static files no `Program.cs` da API. |
| 08/10/2026 | Antigravity AI (usuário: Davi) | Memória dividida por membro: criada `ia-system/membros/` com `DAVI.md` e `MIGUEL.md`; tabela de equipe (Seção 0); protocolo de identificação em `AGENTS.md`, `CLAUDE.md`, `.cursorrules` e `copilot-instructions.md`. | Criar `ISAAC.md` e `LUCAS.md` quando o Davi pedir. Davi: etapa 2 do Bloco 5. |
| 09/10/2026 | Antigravity AI (usuário: Davi) | Bloco 5 Concluído: integração front ↔ backend C# na branch `feature/novas-telas`, criação de `api.js`, sincronização em `wwwroot/index.html`, correção de bugs de lógica (LGPD e data UTC) e testes de endpoints. | Revisão da branch para merge em `main` e avanço para Banco de Dados II (MySQL) com o Isaac. |
| 10/10/2026 | Antigravity AI (usuário: Davi) | Auditoria do modelo lógico do Isaac; bateria de testes ponta a ponta (52 xUnit 100% OK); aplicação das 4 melhorias no `.mwb`; geração de `01_schema_ddl.sql`, `02_dados_iniciais.sql` e reorganização completa do repositório (`README.md`, `docs/README.md`). | Implementar repositórios MySQL concretos no backend C# (Dapper / EF Core). |

*(Todo novo agente que assumir o projeto deve acrescentar uma linha nesta tabela ao final de sua sessão, indicando qual membro da equipe era o usuário).*
