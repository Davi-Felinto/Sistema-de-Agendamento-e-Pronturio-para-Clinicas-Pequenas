# 🧠 Memória Pessoal — Davi Felinto (Dev Principal)
> **Carregue este arquivo quando o usuário se identificar como Davi.**
> Leia também a memória compartilhada: [`../CONTEXTO_MEMORIA_PROJETO.md`](../CONTEXTO_MEMORIA_PROJETO.md).
> **Última Atualização:** 10/10/2026

---

## 1. Perfil
- **Nome:** Davi Felinto — **Dev principal / responsável pelo projeto**.
- **Formação:** CEUB, 2º semestre de Engenharia de Software.
- **Atuação:** OROS Soluções Educacionais (gestão de dados e plataformas educacionais).
- **Background técnico:** Forte em Python, aprendendo C# e POO especificamente para este projeto.
- **GitHub:** `github.com/Davi-Felinto` (git: `Davi Felinto <davifd0978@gmail.com>`).

## 2. Área de Responsabilidade
- **POO C# / .NET** (Domínio, Serviços, Testes xUnit) — 100% concluído (52 testes passando).
- **API `ClinicaApp.Api`** (Bloco 5 — MVP visual integrado com Controllers e static files).
- **Integração front ↔ API:** `api.js` e `index.html` integrados via `fetch()` assíncrono aos endpoints C#, exibindo erros de regra de negócio em tela (`RN01/RN02`, `RN10`, `RN16`).
- **Banco de Dados II (MySQL)** — junto com o Isaac (próxima disciplina).
- Decisões de arquitetura e revisão final do que os demais membros entregam.

## 3. Regras Pedagógicas (específicas do Davi)
1. **O Davi DIGITA o código.** Não crie/altere arquivos de código da aplicação, a menos que ele peça expressamente ("faça você", "arrume no arquivo", "pode criar").
2. **Explique antes do código:** regra de negócio (`RNxx`/`RFxx`/`RQxx`) + motivo técnico.
3. **Revise vícios de Python:**
   - `;` no fim das declarações.
   - `if (condicao)` com parênteses.
   - Propriedades/métodos públicos em `PascalCase`.
   - `?? throw new ArgumentNullException(nameof(x))`.
4. **Comentários apenas de rastreabilidade** (`// RN01`, `// RQ08`).
5. **TDD com xUnit** — `dotnet test src/`.

## 4. Onde o Davi parou
- **Bloco 5 (MVP Visual Integrado) 100% Concluído e Validado:**
  - Telas integradas com a API C# (`ClinicaApp.Api`) com fallback gracioso para localStorage / GitHub Pages.
  - Sincronização entre `index.html` e `src/ClinicaApp.Api/wwwroot/index.html` validada.
  - Endpoints validados de ponta a ponta: `PacientesController` (CRUD e anonimização LGPD RN16/RQ03), `AgendamentosController` (bloqueio RN01/RN02 e cancelamento tardio RN10), `AuthController` (PBKDF2 + Salt RQ08), `ProntuariosController` (versionamento RN17 e auditoria RN13/RQ07) e `FinanceiroController` (cobrança RN07/RN08 e resumo).
  - 52 testes unitários de backend xUnit passando com 100% de sucesso.
- **Bloco Banco de Dados II (MySQL com o Isaac) — 100% Concluído e Integrado:**
  - Persistência relacional em MySQL com Dapper e MySqlConnector totalmente implementada em `src/ClinicaApp/Infrastructure/MySQL/`.
  - Configurado `dotnet user-secrets` para a string de conexão local do Davi (`ClinixDb`), mantendo `appsettings.Development.json` limpo e seguro contra vazamento de credenciais no Git.
  - Injeção de dependência híbrida testada e validada: conecta no MySQL em ambiente com segredos e faz fallback automático para `InMemory` caso o banco não esteja rodando.
  - **Bateria de testes de integração ponta a ponta na API validada com sucesso contra o MySQL local**:
    - `POST /api/auth/login`: autenticação com hash SHA-256 e RBAC para Dr. Davi e Juliana.
    - `GET /api/pacientes`: busca dos pacientes iniciais (Beatriz, Carlos, Mariana).
    - `GET /api/agendamentos`: listagem tipada com status enum deserializado pelo Dapper.
    - `GET /api/financeiro/resumo`: cálculo agregado de pendências diretamente do banco.
  - Todos os 52 testes xUnit passando (100% de sucesso).
- **Preparação do Ambiente de Testes Docker para o Miguel:**
  - Seed do banco expandido para cobrir todo o mês de outubro de 2026: 12 pacientes, 37 consultas, 12 prontuários com versionamento (RN17), 37 pagamentos (15 pagos / 22 pendentes), 10 notificações WhatsApp, 4 bloqueios de agenda e logs de auditoria (RN13/RQ07).
  - Criado arquivo `.dockerignore` e configurado `docker-compose.yml` com MySQL 8.0, API ASP.NET Core, volume de hot-reload para o frontend e phpMyAdmin na porta 8085 para visualização das tabelas via navegador.
  - Criado guia completo passo a passo: [`docs/guias/GUIA_DOCKER_MIGUEL.md`](../../docs/guias/GUIA_DOCKER_MIGUEL.md).
  - Registrada a tarefa como prioridade na fila de trabalho do Miguel ([`MIGUEL.md`](MIGUEL.md)).
- **Banco de Dados II na Nuvem (Aiven Cloud) e Script 03 Concluídos:**
  - Criado [`bancodedadosclinica/03_procedures_views_triggers.sql`](../../bancodedadosclinica/03_procedures_views_triggers.sql) com 3 Views analíticas (`vw_agenda_detalhada`, `vw_resumo_financeiro_mensal`, `vw_prontuario_historico_completo`), 3 Triggers de auditoria LGPD (`trg_auditoria_prontuario_insert`, `trg_auditoria_prontuario_update`, `trg_auditoria_paciente_inativacao`) e 2 Stored Procedures (`sp_cancelar_consulta` RN10 e `sp_fechamento_mensal` RF22).
  - Provisionado cluster gerenciado MySQL 8.4 na nuvem Aiven (`clinix-davifd0978-afc9.b.aivencloud.com:10178`).
  - Executados com sucesso na nuvem os 3 scripts (`01_schema_ddl.sql`, `02_dados_iniciais.sql` e `03_procedures_views_triggers.sql`), deixando o banco na nuvem 100% povoado com 12 pacientes, 37 consultas e fechamentos consolidados.
  - 52 testes xUnit passando com 100% de sucesso.
- **Hospedagem Full-Stack na Nuvem 100% Concluída e Operacional:**
  - **Banco de Dados (Aiven Cloud):** Cluster MySQL 8.4 (`clinix_db`) ativo com 15 tabelas, views analíticas, triggers de auditoria LGPD e procedures.
  - **API Backend (Render.com):** Container ASP.NET Core .NET 8 no ar em `https://clinix-api-c58x.onrender.com` com Swagger e endpoints validados.
  - **Front-end (GitHub Pages):** `api.js` configurado com resolução dinâmica, conectando o GitHub Pages diretamente ao Render com persistência relacional.
  - 52 testes xUnit passando com 100% de sucesso.
- **Integração Cirúrgica do PR #1 (Miguel / Claude Opus 5.5) Concluída com Sucesso:**
  - **Segurança e Robustez:** Adicionados autenticação JWT (`TokenService`), Rate Limiting no login (5 req/min), filtro global de erros de negócio amigáveis (`ErrosDeNegocioFilter`), cabeçalhos de segurança HTTP (CSP, HSTS, nosniff, frame-options deny) e `USER $APP_UID` no Dockerfile.
  - **Criptografia Retrocompatível (RQ08):** `Usuario.cs` atualizado para PBKDF2 com Salt (210.000 iterações), mantendo compatibilidade transparente com os hashes legados SHA-256 das contas existentes no MySQL (Aiven Cloud e local).
  - **Interface Web:** Incorporadas validações HTML5, máscaras de CPF/telefone, contadores de caracteres, atalho de agendamento por paciente e exportação CSV com BOM UTF-8 (`\uFEFF`), preservando a conexão à nuvem (`API_BASE` para o Render), os 6 logins da equipe e o badge de status do MySQL.
  - **Testes Unitários:** 55 testes xUnit passando com 100% de sucesso (3 novos testes adicionados).
- **Correção de Codificação (UTF-8) e Login dos 6 Membros Concluída com Sucesso:**
- **Correção de Codificação (UTF-8) no Banco MySQL e Higienização de Dados Concluída:**
  - **Causa Raiz Identificada:** Os scripts SQL (`01_schema_ddl.sql`, `02_dados_iniciais.sql`, `03_procedures_views_triggers.sql`) não continham `SET NAMES utf8mb4;`, fazendo o cliente MySQL interpretar os arquivos UTF-8 como Latin-1 no entrypoint do Docker e gravar caracteres double-encoded nas tabelas relacionais (`crÃ´nica`, `alÃ©rgica`, `ReabilitaÃ§Ã£o`).
  - **Correção no Banco:** Adicionados `SET NAMES utf8mb4; SET CHARACTER SET utf8mb4;` em todos os scripts SQL e `CharSet=utf8mb4;` na connection string do Docker Compose. Banco reimportado com 100% de integridade nos acentos.
  - **Higienização Defensiva no Front-end (`sanitizeData`):** Implementada função de reversão automática de Mojibake em `index.html` e `wwwroot/index.html` que limpa strings tanto ao carregar dados do `localStorage` quanto nas respostas da API.
- **Próximo passo:** Commitar na `main`, enviar ao GitHub (`git push origin main`) e avisar a equipe.

## 5. Histórico Pessoal de Sessões

| Data | Agente / Ferramenta | O que foi feito | Próximo passo |
|---|---|---|---|
| 07/10/2026 | Antigravity AI | Blocos 1–3 concluídos (entidades, repositórios, Auth/Agenda/Prontuario/Financeiro services), 52 testes passando. | Banco de Dados II. |
| 08/10/2026 | Antigravity AI | Optou por MVP visual antes do MySQL; scaffold da `ClinicaApp.Api`. | Etapa 2 do Bloco 5 (`Program.cs` da API). |
| 08/10/2026 | Antigravity AI | Divisão da memória por membro da equipe (Davi e Miguel). | Continuar etapa 2 do Bloco 5. |
| 09/10/2026 | Antigravity AI | Integração completa front ↔ API na branch `feature/novas-telas`, correção de bugs de lógica (LGPD e data UTC), sincronização com wwwroot e criação do `api.js`. | Revisão da branch para merge em `main` e disciplina de Banco de Dados II (MySQL). |
| 10/10/2026 | Antigravity AI | Auditoria do modelo do Isaac; bateria de testes ponta a ponta (52 xUnit 100% OK); aplicação das 4 melhorias no `.mwb`; geração de `01_schema_ddl.sql`, `02_dados_iniciais.sql` e reorganização completa do repositório (`README.md`, `docs/README.md`). | Implementar repositórios MySQL concretos no backend C#. |
| 10/10/2026 | Antigravity AI | Concluídos os 6 repositórios MySQL com Dapper; configurado `dotnet user-secrets`; corrigido mismatch de índices no ENUM do MySQL; integração ponta a ponta da API com o MySQL local 100% testada e aprovada. 52 testes xUnit OK. | Commit e envio para o GitHub. |
| 10/10/2026 | Antigravity AI | Criado endpoint `GET /api/status`, sistema duplo de badges no frontend (`API .NET C# Conectada` + `MySQL 8.0 Ativo`/`Memória`), flag `--in-memory` e povoamento completo de dados de demonstração (seed) em todas as 15 tabelas do MySQL. | Apresentação acadêmica. |
| 10/10/2026 | Antigravity AI | Expansão massiva do seed de outubro/2026 (37 consultas, 12 pacientes); estruturação do Docker Compose com phpMyAdmin e hot-reload; criação do guia `GUIA_DOCKER_MIGUEL.md` e inclusão na fila do Miguel. | Implementar Opção 2 (Views, Procedures e Triggers em `03_procedures_views_triggers.sql`). |
| 10/10/2026 | Antigravity AI | Criado `03_procedures_views_triggers.sql` (3 views, 3 triggers LGPD, 2 procedures); provisionado cluster MySQL 8.4 na nuvem Aiven e carregados todos os 3 scripts com 100% de sucesso. 52 testes xUnit OK. | Hospedar API C# no Render.com conectada ao Aiven Cloud. |
| 10/10/2026 | Antigravity AI | Deploy da API C# concluído no Render (`https://clinix-api-c58x.onrender.com`); integração do `api.js` apontando para o Render em produção; validação ponta a ponta com banco Aiven Cloud. | Apresentação acadêmica e apoio ao Miguel no UI/UX. |
| 12/10/2026 | Antigravity AI | Integração cirúrgica do PR #1: JWT (`TokenService`), Rate Limiting, CSP/HSTS, `ErrosDeNegocioFilter` (RQ11), PBKDF2 com Salt retrocompatível, máscaras de CPF/telefone, validações e CSV com BOM UTF-8; 55 testes xUnit 100% OK; persistência MySQL (Aiven/Render) preservada. | Corrigir codificação UTF-8 e login dos 6 membros. |
| 12/10/2026 | Antigravity AI | Correção de codificação UTF-8 (mojibake eliminado), inclusão dos 6 membros no frontend com preenchimento da senha (123456), aliases resilientes e atalho de senha em `AuthService.cs`. 56 testes xUnit 100% OK. Container Docker rebuildado e testado. | Corrigir codificação dos dados dos pacientes no MySQL. |
| 12/10/2026 | Antigravity AI | Corrigida causa raiz de codificação no MySQL (`SET NAMES utf8mb4` nos scripts SQL e connection string); dados do MySQL reimportados limpos; adicionada função defensiva `sanitizeData` no front-end para auto-higienização de cache e responses. | Commitar na `main` e dar push no Git. |



