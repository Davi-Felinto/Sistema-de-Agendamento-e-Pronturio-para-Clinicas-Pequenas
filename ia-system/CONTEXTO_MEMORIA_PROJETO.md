# 🧠 MEMÓRIA VIVA DO PROJETO INTEGRADOR
> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> *Arquivo de Sincronização e Memória Universal entre Agentes de IA e IDEs*  
> **Última Atualização:** 29/10/2026 | **Responsável pelo Projeto:** Davi Felinto
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
| **Vanessa** | Membro da equipe e apoio na gestão clínica | *(a criar — `membros/VANESSA.md`)* |

### 🔑 Credenciais Oficiais de Acesso (Dois Perfis Padronizados)

O sistema opera com os dois perfis fundamentais de controle de acesso (RBAC):

| Nome do Usuário | Login (E-mail) | Senha Padrão | Perfil RBAC | Atribuição / Escopo |
|---|---|---|---|---|
| **Dr. Davi Felinto** | `davi@clinix.com` | `123456` | **Profissional** | Atendimento médico, agenda clínica e prontuários eletrônicos (PEP) |
| **Juliana Costa** | `juliana@clinix.com` | `123456` | **Administrador** | Recepção clínica, cadastro de pacientes, financeiro e auditoria LGPD |

> Se a pessoa não tiver memória pessoal ainda, avise e use só a memória compartilhada (sem criar o arquivo sem o Davi pedir).


---

## 1. Contexto Acadêmico
- **Instituição:** CEUB - Centro Universitário de Brasília, Engenharia de Software (2º semestre).
- **Responsável pelo projeto:** Davi Felinto (`github.com/Davi-Felinto`).

### Disciplinas Integradas no Projeto:
1. **Engenharia de Requisitos:** **Etapa 1 (Análise de Negócio) 100% CONCLUÍDA** em Word (.docx), Markdown e BPMN AS-IS/TO-BE. Matriz ampliada para 34 RFs, 23 RNs e 16 RQs (ISO/IEC 25010 e LGPD) incorporando demandas de clientes reais.
2. **Programação Orientada a Objetos (C# / .NET 8 / .NET 10):** **100% CONCLUÍDO**. Foco em Clean Architecture simplificada, Domain-Driven Design pragmático, TDD com xUnit (66 testes unitários com 100% de sucesso).
3. **Banco de Dados II (MySQL):** **100% CONCLUÍDO**. Modelo lógico relacional com 15 tabelas no MySQL Workbench (`clínica.mwb`), DDL (`01_schema_ddl.sql`), DML (`02_dados_iniciais.sql`), views, triggers LGPD e procedures (`03_procedures_views_triggers.sql`), ativo em cluster MySQL 8.4 na nuvem Aiven e Docker Compose.
4. **Desenvolvimento de Interface Web:** **100% CONCLUÍDO**. Interface responsiva com React, Tailwind, usabilidade em até 3 cliques (RQ10), integrada à API ASP.NET Core no Render, MySQL no Aiven e GitHub Pages com suporte offline.

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
│   │   │   ├── Paciente.cs (RF01-RF05, RN16, RQ03, RF29, RN18)
│   │   │   ├── Agendamento.cs (RF06-RF11, RN01, RN02, RN10, RF31, RN20)
│   │   │   ├── SessaoProntuario.cs (RF15-RF18, RN05, RN06, RN17)
│   │   │   ├── VersaoAnotacao.cs (RN17 - histórico de versões)
│   │   │   ├── Pagamento.cs (RF19-RF22, RN07, RN08, RF30, RN19)
│   │   │   ├── AvaliacaoAtendimento.cs (RF32, RN21)
│   │   │   ├── PedidoExameLaboratorio.cs (RF34, RN23)
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
│   │   │   ├── MotivoCancelamento.cs (Desistencia, Conflito, Financeiro, Saude, Satisfacao, Outro)
│   │   │   ├── StatusExameLaboratorio.cs (Coletado, Enviado, EmAnalise, LaudoDisponivel, Entregue)
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
│   │   ├── InMemory/ (Repositórios em memória thread-safe para testes/fallback)
│   │   ├── MySQL/ (Repositórios relacionais Dapper mapeando para o schema de 15 tabelas)
│   │   └── External/
│   │       └── NotificadorWhatsApp.cs (implementa INotificador)
│   └── Services/
│       ├── AuthService.cs (RF26, RQ08 - PBKDF2-SHA256 + Salt e atalho isolado em #if DEBUG)
│       ├── AgendaService.cs (RF06, RF08, RN01, RN02, RN03, RN10, RF31, RN20)
│       ├── ProntuarioService.cs (RF15, RF16, RN05, RN06, RN13, RN17)
│       └── FinanceiroService.cs (RF19-RF22, RN07-RN09, RF30, RN19)
├── ClinicaApp.Api/
│   ├── Controllers/
│   │   ├── AgendamentosController.cs
│   │   ├── AuthController.cs
│   │   ├── AvaliacoesController.cs
│   │   ├── CepController.cs (proxy ViaCEP seguro)
│   │   ├── ExamesLaboratorioController.cs
│   │   ├── FinanceiroController.cs
│   │   ├── LogsController.cs
│   │   ├── PacientesController.cs
│   │   ├── ProntuariosController.cs
│   │   └── StatusController.cs
│   ├── Data/DadosIniciais.cs
│   ├── Program.cs
│   └── wwwroot/
└── ClinicaApp.Tests/ (66 testes unitários xUnit com 100% de aprovação)
    ├── Domain/ (32 testes unitários cobrindo todas as entidades de domínio)
    └── Services/ (34 testes unitários cobrindo fluxos de negócio e segurança)
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

### ✅ Bloco 6: Banco de Dados II (MySQL) — 100% CONCLUÍDO
- Modelo lógico relacional entregue pelo Isaac em `bancodedadosclinica/clínica.mwb` (15 tabelas em 3FN).
- 4 melhorias técnicas aplicadas no Workbench e validadas.
- Scripts completos gerados: `01_schema_ddl.sql`, `02_dados_iniciais.sql` (seed para outubro/2026) e `03_procedures_views_triggers.sql` (3 views, 3 triggers LGPD e 2 stored procedures).
- Deploy em nuvem concluído com sucesso em cluster MySQL 8.4 gerenciado no Aiven Cloud e ambiente Docker local com phpMyAdmin. Repositórios Dapper 100% integrados no C#.

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
| **RF29, RN18** | CEP e Endereço Estruturado com busca ViaCEP | `Paciente.cs`, `PacientesController.cs`, `api.js` |
| **RF30, RN19** | Pagamento Dividido / Misto (Entrada + Saldo) | `Pagamento.cs`, `FinanceiroService.cs`, `FinanceiroController.cs` |
| **RF31, RN20** | Motivos de Cancelamento & Métricas de Desistência | `MotivoCancelamento.cs`, `Agendamento.cs`, `AgendaService.cs` |
| **RF32, RN21** | Ouvidoria da Clínica e Avaliação (1 a 5 estrelas) | `AvaliacaoAtendimento.cs`, `AvaliacoesController.cs` |
| **RF33, RN22** | Recorrência Preventiva (180+ dias) e Fidelidade 10% | `AgendaView`, `DashboardView`, template WhatsApp |
| **RF34, RN23** | Rastreamento de Amostras de Exames Externos | `PedidoExameLaboratorio.cs`, `ExamesLaboratorioController.cs` |
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
| 10/10/2026 | Antigravity AI (usuário: Davi) | Implementados 6 repositórios MySQL concretos com Dapper em `src/ClinicaApp/Infrastructure/MySQL/`; ajustada hidratação de entidades (`Paciente`, `Agendamento`); configurada injeção de dependência híbrida (MySQL / InMemory) no `Program.cs` e `appsettings.Development.json`. 52 testes xUnit passando (100% OK). | Testar conexão ponta a ponta com MySQL local / Docker e commit no Git. |
| 10/10/2026 | Antigravity AI (usuário: Davi) | Adicionado endpoint `GET /api/status` e sistema duplo de badges visuais no cabeçalho do frontend (`API .NET C# Conectada` + `MySQL 8.0 Ativo` / `Memória Mock`); implementada resolução dinâmica de URL (`API_BASE`) para suporte simultâneo a Live Server (porta 5500) e servidor ASP.NET (porta 5055). Sincronizados `index.html`, `wwwroot/index.html`, `api.js` e `wwwroot/api.js`. 52 testes xUnit OK. | Commit e envio ao repositório remoto (`git push origin main`). |
| 10/10/2026 | Antigravity AI (usuário: Davi) | Expansão massiva do seed (`02_dados_iniciais.sql`) para o mês todo de outubro/2026 (12 pacientes, 37 consultas, 12 prontuários, 37 pagamentos); configuração do Docker Compose com phpMyAdmin (porta 8085) e bind mount de `wwwroot`; criação do guia didático `docs/guias/GUIA_DOCKER_MIGUEL.md` e adição na fila de tarefas do Miguel em `MIGUEL.md`. | Implementar Opção 2 de Banco de Dados II (`03_procedures_views_triggers.sql`: Views, Procedures e Triggers de auditoria). |
| 10/10/2026 | Antigravity AI (usuário: Davi) | Concluída a Opção 2 de Banco de Dados II: criado `03_procedures_views_triggers.sql` (3 views analíticas, 3 triggers automáticas LGPD e 2 stored procedures); provisionado cluster MySQL 8.4 gerenciado na nuvem Aiven (`clinix_db`); executados os 3 scripts SQL na nuvem com 100% de sucesso (12 pacientes, 37 agendamentos, views e triggers ativos). 52 testes xUnit OK. | Hospedar a API C# no Render.com conectada ao Aiven Cloud. |
| 10/10/2026 | Antigravity AI (usuário: Davi) | Deploy full-stack na nuvem 100% concluído: API C# no ar no Render (`https://clinix-api-c58x.onrender.com`), conectada ao MySQL 8.4 no Aiven Cloud e integrada ao GitHub Pages via `api.js` com resolução dinâmica de API_BASE. 52 testes xUnit OK. | Apresentação acadêmica para a banca do CEUB e apoio ao Miguel. |
| 12/10/2026 | Antigravity AI (usuário: Davi) | Integração cirúrgica do PR #1 do Miguel: adicionados autenticação JWT (`TokenService`), Rate Limiting no login, filtro de exceções amigáveis `ErrosDeNegocioFilter` (RQ11), cabeçalhos CSP/HSTS, validações HTML5, máscaras de CPF/telefone e exportação CSV compatível com Excel, preservando 100% da arquitetura MySQL Dapper na nuvem, compatibilidade com hashes legados e elevando a suíte para 55 testes xUnit passando (100% OK). | Davi commitar, enviar ao GitHub (`git push origin main`) e responder ao PR #1. |
| 12/10/2026 | Antigravity AI (usuário: Davi) | Correção completa de codificação UTF-8 em `index.html` e `wwwroot/index.html` (mojibake eliminado com restauração pura); disponibilização dos 6 perfis de teste da equipe no frontend com senha preenchida (`123456`); aliases e atalho de senha em `AuthService.cs` com 56 testes xUnit 100% OK. Container Docker rebuildado e testado. | Corrigir codificação dos dados dos pacientes no MySQL. |
| 12/10/2026 | Antigravity AI (usuário: Davi) | Resolução definitiva da codificação no MySQL: adicionados `SET NAMES utf8mb4` nos scripts SQL (`01_schema_ddl.sql`, `02_dados_iniciais.sql`, `03_procedures_views_triggers.sql`) e `CharSet=utf8mb4` no Docker Compose; dados reimportados limpos no MySQL; implementada função defensiva `sanitizeData` no front-end para higienização em memória e no `localStorage`. | Restaurar badge de status do banco de dados no frontend. |
| 12/10/2026 | Antigravity AI (usuário: Davi) | Restaurado badge duplo de status no header e na tela de login (`API .NET C# Conectada` + `MySQL 8.0 Ativo` / `Memória Mock`) consultando `GET /api/status`. Commits enviados para a `main`. | Testar no navegador e validar com a equipe. |
| 19/10/2026 | Antigravity AI (usuário: Davi) | Implementação completa ponta a ponta (Backend C# + Frontend React + Testes xUnit) das 6 novas demandas do feedback da cliente real: (1) CEP & Endereço Estruturado ViaCEP (RF29/RN18); (2) Pagamento Misto/Dividido Entrada + Saldo (RF30/RN19); (3) Motivos de Cancelamento & Métricas (RF31/RN20); (4) Ouvidoria & Avaliação de Atendimento 1-5 estrelas (RF32/RN21); (5) Recorrência Preventiva 180+ dias com cupom 10% no WhatsApp (RF33/RN22); (6) Rastreamento de Amostras Laboratoriais Externas e alerta clínico em consultas pendentes (RF34/RN23). Suíte de testes expandida para 66 testes unitários xUnit com 100% de sucesso. | Apresentação das novas telas para a cliente e homologação. |
| 20/10/2026 | Antigravity AI (usuário: Davi) | Correção e homologação ponta a ponta no ambiente Docker/MySQL: criação de CepController (proxy backend) e liberação do ViaCEP no CSP; remoção da constraint UNIQUE de agendamento em pagamentos para permitir divisão (RF30); normalização resiliente de tipos de IDs de pacientes (`matchPatientId`/`findPatient`) e status de exames (1 a 5); inclusão de agendamento de Mariana Duarte na data atual; rebuild do container `clinix-api`. 66 testes xUnit 100% OK. | Validação visual no navegador com a cliente. |
| 20/10/2026 | Antigravity AI (usuário: Davi) | Diagnóstico do vídeo 1: correção de Rate Limiting (429) no ASP.NET Core (elevado de 5 para 30 req/min), tratamento amigável de 429 no frontend, auto-preenchimento e reset de erro ao alternar perfis no login. Rebuild da API e validação dos 6 logins via API com 200 OK. | Apresentação das telas para a cliente. |
| 20/10/2026 | Antigravity AI (usuário: Davi) | Diagnóstico do vídeo 2 (tela piscando no login): identificada exceção de materialização do Dapper em `Paciente.cs` ao chamar `GET /api/pacientes` no hook pós-login `conectarBackend()` (devido à ampliação do construtor para 17 parâmetros de CEP). Adicionado construtor sem parâmetros e sobrecarga de 10 parâmetros em `Paciente.cs`. Rebuild da API e todos os endpoints testados com 200 OK. | Validação visual no navegador pelo Davi. |
| 20/10/2026 | Antigravity AI (usuário: Davi) | Concluída a Etapa 1 de Engenharia de Requisitos (Prof. Sérgio Haddad - CEUB): elaboração da Análise de Negócio completa (estudo de caso Amanda Souza Peixoto), modelagem BPMN AS-IS e TO-BE (PNG + Mermaid), 8 problemas, objetivos, benefícios, 11 macrofuncionalidades e requisitos ISO/IEC 25010 e LGPD. Gerados Word oficial (`.docx`), versão Markdown e pasta organizada `docs/Entregas-Eng.-de-Requisitos/`. | Realizar a Etapa 2 quando solicitada. |
| 21/10/2026 | Antigravity AI (usuário: Davi) | Refinamento do documento Word da Etapa 1, verificação dos 66 testes unitários xUnit (100% OK) e envio ao repositório remoto via `git push origin main` (commits `f65179d`, `a7f1872`, `9e2c74a`). Repositório limpo e sincronizado. | Iniciar a Etapa 2 de Engenharia de Requisitos (Personas, Histórias de Usuário e Backlog) quando solicitado. |
| 21/10/2026 | Antigravity AI (usuário: Davi) | Instalação local e global das skills `find-skills`, `security-review` (Sentry) e `security-and-hardening` (Addy Osmani); condução de auditoria de segurança completa (SAST/OWASP e Hardening); aplicação de 3 correções de segurança (`#if DEBUG` em atalho de senha em `AuthService.cs`, sanitização de erro 500 em `CepController.cs` e CSPRNG `RandomNumberGenerator` em `ProntuariosController.cs`). 66 testes unitários xUnit 100% aprovados e compilação Release com 0 avisos e 0 erros. | Apresentação comercial da minuta de contrato / Início da Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI (usuário: Davi) | Auditoria e saneamento global de documentação: padronização de autoria e contratado para "Davi Felinto" em todos os documentos (Word .docx e Markdown); sincronização universal de contagem para 66 testes unitários xUnit (100% aprovados), 34 RFs, 23 RNs e 16 RQs no README mestre, `src/README.md`, `docs/README.md`, `AGENTS.md`, `bancodedadosclinica/README.md`, propostas comerciais e roteiros acadêmicos da banca. | Apresentação da minuta à cliente / Iniciar Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI (usuário: Davi) | Simplificação dos perfis de acesso na tela de login para apenas 2 opções (Profissional: Dr. Davi Felinto e Administrador: Juliana Costa) em `index.html`, `wwwroot/index.html` e `DadosIniciais.cs`. 66 testes unitários xUnit 100% OK. | Apresentação da minuta à cliente / Iniciar Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI (usuário: Davi) | Levantamento de custos de infraestrutura e TCO em produção: dimensionamento da carga de trabalho (450-700 MB RAM, 15-40 consultas/dia), comparativo de 4 cenários (VPS Docker Recomendada de R$ 35-50/mês gerando ~80% de margem na assinatura de R$ 179/mês), viabilidade financeira de escala e geração de documentos oficiais (`.docx` e `.md`) em `docs/comercial/`. | Apresentação da minuta à cliente / Iniciar Etapa 2 de Requisitos. |
| 22/10/2026 | Antigravity AI (usuário: Davi) | Varredura completa do repositório e sincronização conceitual dos documentos de requisitos e design: `Documento_Especificacao_Requisitos_Final.docx` e `SDD_Software_Design_Document.md` atualizados com 34 RFs, 23 RNs, 10 RDs, 16 RQs e novos comportamentos de sistema, 100% purificados e agnósticos de tecnologias de implementação. 66 testes unitários xUnit 100% OK. | Apresentação comercial à cliente / Iniciar Etapa 2 de Requisitos. |
| 22/10/2026 | Antigravity AI (usuário: Davi) | Bifurcação e criação do repositório acadêmico oficial do CEUB (`Clinix-Projeto-Integrador-Academico`): escopo delimitado a F1-F7 (RF01-RF28, RN01-RN17, RQ01-RQ16), expurgadas entidades e telas comerciais (Ouvidoria, Laboratório, CEP, Pagamento Dividido, Motivos de Cancelamento e docs comerciais), 56 testes unitários xUnit 100% aprovados, 15 tabelas MySQL do Isaac e créditos aos 5 membros da equipe. Repositório comercial mantido intacto com 66 testes unitários xUnit. | Apresentação na banca acadêmica do CEUB / Etapa 2 de Requisitos. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Transição do repositório para Piloto Comercial de testes práticos com cliente real: expurgadas todas as marcas e links acadêmicos/CEUB, implementada tela de login com email/senha e atalhos rápidos de demo, tela de cadastro de usuários com endpoint backend `POST /api/auth/registrar` e JWT, widget flutuante de feedback para o piloto clínico (com WhatsApp) e controle de dados (clínica vazia vs dados modelo). Suíte com 68 testes xUnit 100% aprovados. | Conduzir testes com cliente real e receber feedbacks de usabilidade. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Validação ponta a ponta com a skill `agent-browser` no Chromium real: validados login (email/senha, toggle de visibilidade e pills de 1 clique), tela de cadastro (registro de Dra. Renata Silva via `POST /api/auth/registrar` com auto-login), modal de Feedback do Piloto (5 estrelas, sugestão e integração de métrica de 4.8★ para 4.9★), modal de dados do piloto (limpeza para clínica vazia e restauração de dados modelo) e logout. Corrigidos bugs críticos descobertos pelo browser (mapeamento do retorno de `api.login`, sincronização de `api.js` e persistência de sessão no `localStorage`). 68 testes xUnit 100% OK. | Implementar controle RBAC no prontuário para perfil recepção conforme LGPD/CFM. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Implementação e homologação de RBAC / Sigilo Médico (LGPD Art. 11 & CFM 2.299/2021): bloqueio de Prontuários para perfil Recepcionista/Administrador no backend C# (`[Authorize(Roles = "Profissional")]` com HTTP 403 Forbidden) e na interface (ocultação na Sidebar, Pacientes, Dashboard e Busca Rápida). Validação automatizada com `agent-browser` no Chromium real confirmando bloqueio para Juliana e acesso liberado para Dr. Davi. 68 testes xUnit 100% OK. | Conduzir testes com a cliente real. |
| 26/10/2026 | Antigravity AI (usuário: Davi) | Atualização do número de WhatsApp do Feedback do Piloto para `+55 61 99588-7443` (com o 9º dígito obrigatório do WhatsApp para celulares do Brasil) em `index.html` e `wwwroot/index.html`. Validação automatizada no navegador via `agent-browser` confirmando link de mensagem pré-formatada. 68 testes unitários xUnit 100% OK. | Conduzir testes com a cliente real. |
| 29/10/2026 | Antigravity AI (usuário: Davi) | Diagnóstico do banco MySQL na nuvem (Render/Aiven); correção de Dapper em `LogAcesso.cs` (`GET /api/logs`); rastreamento automático de IP (`X-Forwarded-For`), dispositivo (`User-Agent`) e `ultimo_acesso` em logins/cadastros; blindagem de `POST /api/auth/registrar` com Código de Convite da clínica; endpoint `GET /api/auth/usuarios` e painel de monitoramento de contas na aba Auditoria. Validado com `agent-browser` e 70 testes unitários xUnit 100% aprovados. | Push para `origin/main` para deploy automático no Render. |
| 29/10/2026 | Antigravity AI (usuário: Davi) | Exclusão definitiva de contas de teste #7 e #8 no MySQL de produção (Render/Aiven); blindagem RBAC de `GET /api/logs` e `GET /api/auth/usuarios` restrita a Administrador (`[Authorize(Roles = "Administrador")]`); endpoint `DELETE /api/auth/usuarios/{id}`; privilégio de Admin atribuído a Dr. Davi Felinto no JWT; UI ajustada para ocultar auditoria de profissionais/clientes e incluir botão `🗑️ Apagar` na listagem de contas; tela de login com 3 atalhos; 71 testes xUnit aprovados e validados no navegador com `agent-browser`. | Apresentar à cliente com segurança e controle de licença ativos. |

*(Todo novo agente que assumir o projeto deve acrescentar uma linha nesta tabela ao final de sua sessão, indicando qual membro da equipe era o usuário).*







