# 🧠 Memória Pessoal — Davi Felinto (Dev Principal)
> **Carregue este arquivo quando o usuário se identificar como Davi.**
> Leia também a memória compartilhada: [`../CONTEXTO_MEMORIA_PROJETO.md`](../CONTEXTO_MEMORIA_PROJETO.md).
> **Última Atualização:** 23/10/2026

---

## 1. Perfil
- **Nome:** Davi Felinto — **Dev principal / responsável pelo projeto**.
- **Formação:** CEUB, 2º semestre de Engenharia de Software.
- **Atuação:** OROS Soluções Educacionais (gestão de dados e plataformas educacionais).
- **Background técnico:** Forte em Python, aprendendo C# e POO especificamente para este projeto.
- **GitHub:** `github.com/Davi-Felinto` (git: `Davi Felinto <davifd0978@gmail.com>`).

## 2. Área de Responsabilidade
- **POO C# / .NET** (Domínio, Serviços, Testes xUnit) — 100% concluído (68 testes unitários xUnit com 100% de sucesso).
- **API `ClinicaApp.Api`** (Bloco 5 — MVP visual integrado com Controllers e static files).
- **Integração front ↔ API:** `api.js` e `index.html` integrados via `fetch()` assíncrono aos endpoints C#, exibindo erros de regra de negócio em tela (`RN01/RN02`, `RN10`, `RN16`, `RN18-RN23`).
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
6. **Validação Contínua de Frontend com `agent-browser` (MANDATÓRIO):** Sempre que houver qualquer alteração que interfira ou modifique o frontend (`index.html`, `api.js`, telas, formulários, modais ou integração com a API), executar obrigatoriamente a verificação automatizada ponta a ponta com a skill `agent-browser` para validar elementos visuais, fluxos de uso e ausência de erros no console antes de concluir a entrega.

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
- **Implementação Integral das 6 Demandas da Cliente Real Concluída com Sucesso:**
  - **(1) CEP e Endereço Estruturado (RF29, RN18):** `Paciente.cs` com propriedades de endereço completo, busca assíncrona automática via ViaCEP API no front e validação de CEP no backend.
  - **(2) Pagamento Misto / Dividido Entrada + Saldo (RF30, RN19):** `Pagamento.cs` enriquecido com tipo de lançamento e observação; `FinanceiroService.RegistrarPagamentoDividido(...)` com validação de soma; modal no front com cálculo automático de saldo e formas distintas.
  - **(3) Motivos de Cancelamento & Métricas (RF31, RN20):** Enum `MotivoCancelamento.cs` (6 categorias); `Agendamento.cs` e `AgendaService.CancelarConsulta(...)` registrando motivo e justificativa; modal dedicado na Agenda e badges de motivo.
  - **(4) Ouvidoria & Avaliação de Atendimento (RF32, RN21):** Entidade `AvaliacaoAtendimento.cs` com notas de 1 a 5 para Recepção, Atendimento Clínico e Instalações, cálculo de média e opção de anonimato; nova tela `OuvidoriaView` com cards de médias e depoimentos.
  - **(5) Recorrência Preventiva 180+ dias & Fidelidade 10% (RF33, RN22):** Detector de check-up semestral no frontend e dashboard; banner na Agenda com botão de 1 clique para WhatsApp gerando convite com cupom `CHECKUP10`.
  - **(6) Rastreamento de Amostras Laboratoriais Externas (RF34, RN23):** Entidade `PedidoExameLaboratorio.cs` com ciclo de vida (Coletado, Enviado, Em Análise, Laudo Disponível, Entregue); nova tela `LaboratorioView`; alerta visual pulsante em consultas que possuem exames pendentes.
  - **Testes Unitários:** Suíte expandida de 56 para **66 testes unitários xUnit passando com 100% de sucesso**.
- **Correção no Login e Rate Limiting (HTTP 429) Concluída:**
  - **Causa Raiz do Erro no Vídeo:** O rate limiter do ASP.NET Core (`Program.cs`) estava configurado com limite estrito de apenas 5 requisições por minuto (`PermitLimit = 5`). Ao alternar rapidamente entre os perfis de teste ou realizar cliques consecutivos, a API retornava `HTTP 429 Too Many Requests`. No frontend (`api.js`), respostas que não fossem `res.ok` sem mensagem de erro caíam no fallback genérico `"Não foi possível entrar: Login ou senha inválidos."`.
- **Correção da Piscada no Login / Materialização Dapper em `Paciente.cs` Concluída:**
  - **Causa Raiz Exata do Segundo Vídeo (`17-01-50`):** O login na rota `/api/auth/login` tinha sucesso e retornava o JWT. No React (`index.html`), `setIsAuthenticated(true)` disparava o hook `useEffect` (`conectarBackend()`). Este chamava `api.listarPacientes()`. No backend C#, o `MySqlPacienteRepository` executava `SELECT ... FROM pacientes` com 10 colunas e usava Dapper para instanciar `Paciente`. Como a entidade `Paciente` teve seu construtor ampliado para 17 parâmetros (novos campos de CEP/endereço) sem construtor padrão ou sobrecarga de 10 parâmetros, o Dapper lançava exceção de materialização (`A parameterless default constructor or one matching signature (Id, Nome, ...) is required`). A API retornava `400 Bad Request`. No front, ao receber erro em `listarPacientes()`, a lógica de segurança tratava como token inválido e chamava imediatamente `setIsAuthenticated(false)`, fazendo a tela "piscar" e voltar para o login em milissegundos.
  - **Correções Aplicadas:** Adicionado construtor sem parâmetros e sobrecarga do construtor de 10 parâmetros em `Paciente.cs`; corrigida ordem dos `useEffect` de `reviews` e `labSamples` em `index.html` e `wwwroot/index.html`; container Docker `clinix-api` rebuildado. Testado via script que todos os endpoints pós-login (`/api/pacientes`, `/api/agendamentos`, `/api/avaliacoes`, `/api/exameslaboratorio`) retornam `200 OK`.
- **Etapa 1 de Engenharia de Requisitos 100% Concluída, Validada e Enviada ao GitHub:**
  - Análise de Negócio completa elaborada seguindo rigorosamente o modelo do Prof. Sérgio Haddad (CEUB) e as orientações da disciplina.
  - Entregáveis gerados em `docs/Entregas-Eng.-de-Requisitos/Etapa-1/`: documento Word formatado (`Trabalho Final ER - 1 - Análise de Negócio - Clinix.docx`), versão Markdown completa e diagramas de processo BPMN AS-IS e TO-BE de alta resolução (`.png` e `.md`).
  - 66/66 testes unitários xUnit passando com 100% de sucesso.
  - Commits sincronizados no repositório remoto (`git push origin main` com commits `f65179d`, `a7f1872`, `9e2c74a`).
- **Elaboração do Contrato Base Oficial de Licenciamento e Prestação de Serviços (Clinix):**
  - Sessão estruturada de Grilling (estresse e blindagem de cláusulas jurídicas e operacionais).
  - Estrutura híbrida com escolha formal no Anexo I: **Modalidade A (SaaS / Assinatura Recorrente)** com R$ 290 de adesão + R$ 179/mês (nuvem e backups inclusos) vs. **Modalidade B (Projeto Dedicado Fechado)** por R$ 2.400 em 3 marcos (nuvem por conta da clínica + 90 dias de garantia).
  - Blindagem de Propriedade Intelectual (código-fonte 100% de Davi Felinto), divisão LGPD (Clínica = Controladora / Davi = Operador), SLA profissional (2h para críticos / 24h operacionais) e cláusula mandatória de portabilidade de dados (SQL e Excel/CSV em 15 dias úteis sem taxas).
  - Arquivos gerados e prontos para uso em `docs/comercial/`: [`Contrato_Prestacao_Servicos_Clinix.docx`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Contrato_Prestacao_Servicos_Clinix.docx) e [`Contrato_Prestacao_Servicos_Clinix.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Contrato_Prestacao_Servicos_Clinix.md).
- **Instalação das Skills no Projeto e Globalmente:**
  - `mattpocock/skills` (`grill-me` e `grilling`) e `vercel-labs/agent-browser` (`agent-browser`) instaladas localmente em `.agents/skills/`.
  - `find-skills`, `security-review` (Sentry) e `security-and-hardening` (Addy Osmani) instaladas e ativas.
- **Auditoria de Segurança e Hardening 100% Concluída (Classificação A+):**
  - SAST completo com OWASP Top 10 e frameworks Sentry + Addy Osmani.
  - 3 correções de segurança aplicadas: `#if DEBUG` em `AuthService.cs` para isolar atalhos de senha de desenvolvimento de builds Release; sanitização defensiva de erros 500 em `CepController.cs` evitando information disclosure; substituição de `Random` por CSPRNG criptográfico `RandomNumberGenerator` em `ProntuariosController.cs`.
  - Relatório oficial consolidado em `docs/seguranca/Relatorio_Auditoria_Seguranca_Clinix.md`.
  - 66/66 testes unitários xUnit 100% aprovados e compilação em Release com 0 erros e 0 warnings.
- **Levantamento de Custos de Infraestrutura e Manutenção (TCO) Concluído:**
  - Elaborado levantamento completo de custos operacionais (OPEX/TCO) para manter o Clinix no ar 24/7.
  - Dimensionamento detalhado da carga de trabalho para clínicas pequenas (15 a 40 atendimentos/dia, 500 a 2.500 req/dia, ~450 MB a 700 MB de RAM no total).
  - Comparativo estruturado em 4 cenários: Piloto Gratuito (R$ 3 a 15/mês), VPS Docker Recomendada (R$ 35 a 50/mês), PaaS Gerenciado (R$ 90 a 130/mês) e Enterprise (R$ 180 a 280/mês).
  - Análise de viabilidade financeira e Unit Economics: no modelo SaaS (R$ 179/mês), o custo de R$ 35/mês gera margem líquida de ~80% logo no 1º cliente; taxa de adesão de R$ 290 antecipa 8 meses de custos de servidores.
  - Documentos oficiais gerados em `docs/comercial/`: [`Levantamento_Custos_Infraestrutura_Clinix.docx`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Levantamento_Custos_Infraestrutura_Clinix.docx) e [`Levantamento_Custos_Infraestrutura_Clinix.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Levantamento_Custos_Infraestrutura_Clinix.md).
- **Sincronização e Purificação Conceitual de Documentos de Requisitos e Design (SDD) Concluída:**
  - Varredura completa realizada em todo o repositório (entidades, serviços, enums, controllers, views, triggers e regras).
  - Atualizado [`docs/requisitos/Documento_Especificacao_Requisitos_Final.docx`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/requisitos/Documento_Especificacao_Requisitos_Final.docx): sincronizadas todas as 13 macrofuncionalidades (F1 a F13), os 34 Requisitos Funcionais (RF01 a RF34), os 10 Requisitos de Dados (RD01 a RD10), as 23 Regras de Negócio (RN01 a RN23) e os 16 Requisitos de Qualidade ISO/IEC 25010 e LGPD, redigidos estritamente em linguagem de negócios e clínica, sem qualquer menção a tecnologias transitórias.
  - Reescrito integralmente [`docs/design/SDD_Software_Design_Document.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/design/SDD_Software_Design_Document.md): transformado em um Documento de Design Conceitual de Domínio 100% agnóstico de tecnologias de implementação (removidas referências a C#, .NET, MySQL, Dapper, Docker, React, etc.), contendo diagramas Mermaid de arquitetura conceitual, entidades de domínio com atributos e comportamentos, 6 diagramas de sequência de fluxos críticos de negócio, especificação textual detalhada das 23 Regras de Negócio e matriz de rastreabilidade conceitual completa para os 34 RFs.
  - Sincronizado [`docs/requisitos/README.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/requisitos/README.md).
  - 66/66 testes unitários xUnit passando com 100% de sucesso (`dotnet test src/`).
- **Controle de Acesso RBAC / Sigilo Médico (LGPD Art. 11 & Resolução CFM 2.299/2021) Concluído e Validado:**
  - **Decisão Arquitetural:** Recepcionista/Administrador tem acesso administrativo a Pacientes (nome, telefone, CPF, endereço para agendamento), Agenda e Financeiro, mas **bloqueio total a Prontuários Médicos** (notas clínicas, anamneses, CID, prescrições).
  - **Backend C#:** Adicionado `[Authorize(Roles = "Profissional")]` na classe `ProntuariosController.cs`. Requisições de não-profissionais retornam `403 Forbidden` na API.
  - **Frontend UI (`index.html` e `wwwroot/index.html`):** Ocultado item "Prontuário" na Sidebar para perfil não-profissional; removido botão "Prontuário" nos cards de Pacientes e no Busca Rápida (SearchModal); no Dashboard, botão de atendimento agora exibe "Ver agenda" em vez de "Prontuário"; rota direta de prontuário protegida com card de aviso legal LGPD/CFM.
  - **Validação Automatizada com `agent-browser` (Chromium Real):**
    1. Login como Recepcionista (Juliana): Sidebar sem "Prontuário", lista de Pacientes apenas com botões "Agendar/Editar/Inativar/Exportar", Dashboard com botão "Ver agenda" (Screenshot `08_reception_sidebar_no_prontuario.png` e `09_reception_patients_no_prontuario.png`).
    2. Tentativa direta de acesso à API `/api/prontuarios/paciente/1` com JWT de recepção: retorno verificado **HTTP 403 Forbidden**.
    3. Login como Dr. Davi (Profissional): "Prontuário" visível na Sidebar e nos pacientes, abrindo ficha clínica e evoluções perfeitamente (Screenshot `10_doctor_prontuario_accessible.png`).
    4. Suíte xUnit: 68/68 testes unitários passando com 100% de sucesso.
- **Atualização do Canal de WhatsApp do Feedback do Piloto Concluída:**
  - Configurado o número oficial de WhatsApp do desenvolvedor Davi Felinto (`+55 61 99588-7443`) no botão de envio direto do modal `FeedbackModal` (`https://wa.me/5561995887443?text=...`).
  - Sincronização rigorosa mantida entre `index.html` e `src/ClinicaApp.Api/wwwroot/index.html`.
  - Validação ponta a ponta com a skill `agent-browser`: abertura do modal no Chromium real e verificação de que o link gerado aponta corretamente com mensagem pré-formatada codificada para `+55 61 99588-7443`.
  - 68/68 testes unitários xUnit passando com 100% de sucesso.
- **Próximo passo:** Apresentação da plataforma e condução dos testes de usabilidade com a cliente real.

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
| 12/10/2026 | Antigravity AI | Corrigida causa raiz de codificação no MySQL (`SET NAMES utf8mb4` nos scripts SQL e connection string); dados do MySQL reimportados limpos; adicionada função defensiva `sanitizeData` no front-end para auto-higienização de cache e responses. | Restaurar badge de status do banco de dados no frontend. |
| 12/10/2026 | Antigravity AI | Restaurado badge duplo de monitoramento de banco de dados (`MySQL 8.0 Ativo` / `Memória Mock`) no header e na tela de login via `GET /api/status`. Commits enviados para a `main`. | Testar no navegador e validar com a equipe. |
| 19/10/2026 | Antigravity AI | Atualização estratégica da proposta comercial e roteiro de reunião: Opção 1 (SaaS) configurada com Taxa de Adesão de R$ 290,00 (setup, importação de dados e treinamento da equipe) + Mensalidade de R$ 179,00 / mês (1ª mensalidade após 30 dias de uso); Opção 2 mantida em R$ 2.400,00 (3x R$ 800); Documentos Word (`.docx`) e Markdown sincronizados e prontos. | Apresentação e fechamento com a cliente real. |
| 19/10/2026 | Antigravity AI | Implementação de ponta a ponta (Backend C# + Frontend React + Testes xUnit) das 6 demandas da cliente real: CEP ViaCEP, Pagamento Dividido, Motivos de Cancelamento, Ouvidoria, Retorno Preventivo 180d e Exames Laboratoriais. Suíte expandida para 66 testes unitários com 100% de sucesso. | Apresentação das telas e validação final com a cliente. |
| 20/10/2026 | Antigravity AI | Correção e homologação ponta a ponta no ambiente Docker/MySQL: criação de CepController (proxy backend) e liberação do ViaCEP no CSP; remoção da constraint UNIQUE de agendamento em pagamentos para permitir divisão (RF30); normalização resiliente de tipos de IDs de pacientes (`matchPatientId`/`findPatient`) e status de exames (1 a 5); inclusão de agendamento de Mariana Duarte na data atual; rebuild do container `clinix-api`. 66 testes xUnit 100% OK. | Validação visual no navegador com a cliente. |
| 20/10/2026 | Antigravity AI | Diagnóstico do vídeo 1: correção de Rate Limiting (429) no ASP.NET Core (elevado de 5 para 30/min), tratamento amigável de 429 no frontend, auto-preenchimento e reset de erro ao alternar usuários no login. Rebuild da API e validação dos 6 logins via API com 200 OK. | Apresentação das telas para a cliente. |
| 20/10/2026 | Antigravity AI | Diagnóstico do vídeo 2 (tela piscando): identificada falha de materialização do Dapper em `Paciente.cs` (erro 400 em `/api/pacientes` no hook pós-login `conectarBackend()`, forçando logout imediato). Adicionado construtor sem parâmetros e sobrecarga de 10 parâmetros em `Paciente.cs`. Rebuild da API e testes 100% OK. | Validação visual no navegador pelo Davi. |
| 20/10/2026 | Antigravity AI | Concluída a Etapa 1 de Engenharia de Requisitos (Prof. Sérgio Haddad - CEUB): elaboração da Análise de Negócio completa (estudo de caso com Amanda Souza Peixoto), modelagem BPMN AS-IS e TO-BE (PNG + Mermaid), mapeamento de 8 problemas, objetivos, benefícios, 11 macrofuncionalidades e requisitos ISO/IEC 25010 e LGPD. Gerados Word oficial (`.docx`), versão Markdown e pasta organizada `docs/Entregas-Eng.-de-Requisitos/`. | Realizar a Etapa 2 quando solicitada. |
| 21/10/2026 | Antigravity AI | Refinamento do layout do Word da Etapa 1, execução da suíte de 66 testes xUnit (100% de sucesso) e push de 3 commits para o repositório remoto (`origin/main`). Branch atualizada e árvore de trabalho limpa. | Iniciar Etapa 2 de Requisitos (Personas, Histórias de Usuário e Backlog) quando solicitado. |
| 21/10/2026 | Antigravity AI | Instalação local das skills `grill-me`, `grilling` e `agent-browser`; condução de sessão completa de Grilling para parametrização contratual; criação do Contrato Base Oficial do Clinix (SaaS e Projeto Dedicado) em `.docx` e `.md` em `docs/comercial/`. | Fechamento comercial / Iniciar Etapa 2 de Engenharia de Requisitos. |
| 21/10/2026 | Antigravity AI | Instalação global de `find-skills`, `security-review` (Sentry) e `security-and-hardening` (Addy Osmani); auditoria de segurança completa do código e infraestrutura; aplicação de 3 blindagens (`#if DEBUG` em `AuthService.cs`, sanitização de erro em `CepController.cs` e CSPRNG em `ProntuariosController.cs`). 66 testes unitários xUnit 100% OK e compilação Release com 0 erros. | Apresentação da minuta contratual à cliente e início da Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI | Auditoria e saneamento global de documentação: padronização de autoria e contratado para "Davi Felinto" em todos os documentos (Word .docx e Markdown); sincronização universal para 66 testes unitários xUnit (100% OK), 34 RFs, 23 RNs e 16 RQs no README mestre, `src/README.md`, `docs/README.md`, `AGENTS.md`, `bancodedadosclinica/README.md`, propostas comerciais e roteiros da banca. | Apresentação da minuta à cliente e início da Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI | Simplificação dos perfis de login para apenas 2 (Dr. Davi Felinto - Profissional e Juliana Costa - Administrador) em `index.html`, `wwwroot/index.html` e `DadosIniciais.cs`. 66 testes unitários xUnit 100% OK. | Apresentação da minuta à cliente e início da Etapa 2 de Requisitos. |
| 21/10/2026 | Antigravity AI | Levantamento executivo de custos de infraestrutura e TCO para manter o sistema em produção: dimensionamento de workload, comparativo em 4 cenários (Cenário VPS Recomendado de R$ 35 a 50/mês, gerando margem de ~80% na assinatura de R$ 179/mês), unit economics de escala e geração de documentos Word (.docx) e Markdown em `docs/comercial/`. | Apresentação comercial à cliente e início da Etapa 2 de Requisitos. |
| 22/10/2026 | Antigravity AI (usuário: Davi) | Varredura completa do repositório e sincronização conceitual dos documentos oficiais: `Documento_Especificacao_Requisitos_Final.docx` e `SDD_Software_Design_Document.md` atualizados com 34 RFs, 23 RNs, 10 RDs, 16 RQs e novos comportamentos de sistema, 100% purificados e agnósticos de tecnologias de implementação. 66 testes xUnit OK. | Apresentação comercial à cliente e início da Etapa 2 de Requisitos. |
| 22/10/2026 | Antigravity AI (usuário: Davi) | Criação e separação oficial do novo repositório acadêmico do Projeto Integrador (`Clinix-Projeto-Integrador-Academico`): escopo estritamente delimitado a F1-F7 (RF01-RF28, RN01-RN17, RQ01-RQ16), expurgadas entidades e telas comerciais (Ouvidoria, Laboratório, CEP, Pagamento Dividido e Motivos de Cancelamento), suíte xUnit ajustada com 56 testes unitários 100% aprovados, BD II MySQL com as 15 tabelas originais do Isaac, docs comerciais expurgados e README acadêmico destacando os 5 membros do CEUB. Repositório comercial intacto com 66 testes xUnit. | Apresentação na banca do CEUB / Etapa 2 de Requisitos. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Transição oficial do repositório para Piloto Comercial de testes com cliente real: expurgadas todas as marcas acadêmicas e CEUB, implementada nova tela de login (email e senha), nova tela de cadastro (registro) integrada a `POST /api/auth/registrar` com JWT, widget flutuante de feedback do piloto com notas/comentários e integração com WhatsApp, além de controle de dados modelo vs clínica vazia. Suíte expandida para 68 testes xUnit 100% aprovados. | Conduzir testes práticos de usabilidade com a cliente e coletar feedbacks. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Validação ponta a ponta com a skill `agent-browser` no Chromium real: validados login (email/senha, toggle de visibilidade e pills de 1 clique), tela de cadastro (registro de Dra. Renata Silva via `POST /api/auth/registrar` com auto-login), modal de Feedback do Piloto (5 estrelas, sugestão e integração de métrica de 4.8★ para 4.9★), modal de dados do piloto (limpeza para clínica vazia e restauração de dados modelo) e logout. Corrigidos bugs críticos descobertos pelo browser (mapeamento do retorno de `api.login`, sincronização de `api.js` e persistência de sessão no `localStorage`). 68 testes xUnit 100% OK. | Implementar controle RBAC no prontuário para perfil recepção conforme LGPD/CFM. |
| 23/10/2026 | Antigravity AI (usuário: Davi) | Implementação e validação integral de RBAC / Sigilo Médico (LGPD Art. 11 & CFM 2.299/2021): bloqueio de acesso a Prontuários para não-profissionais (Recepção/Administrador) na API (`[Authorize(Roles = "Profissional")]` com HTTP 403) e na UI (ocultação na Sidebar, Pacientes, Busca Rápida e Dashboard; guard route de aviso legal). Validação com `agent-browser` comprovando bloqueio para Juliana e liberação completa para Dr. Davi. 68 testes unitários xUnit 100% OK. | Conduzir testes de usabilidade com a cliente real. |
| 26/10/2026 | Antigravity AI (usuário: Davi) | Atualização do número de WhatsApp do Feedback do Piloto para `+55 61 99588-7443` (com o 9º dígito obrigatório do WhatsApp para o celular de Brasília informado pelo Davi) em `index.html` e `wwwroot/index.html`. Validação automatizada no navegador via `agent-browser` confirmando link gerado com mensagem pré-formatada. 68 testes xUnit 100% OK. | Conduzir testes de usabilidade com a cliente real. |






