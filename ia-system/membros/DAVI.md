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
- **Bloco Banco de Dados II (MySQL com o Isaac) — Modelagem Concluída:**
  - Modelo lógico relacional em `bancodedadosclinica/clínica.mwb` atualizado diretamente no XML com as 4 correções de engenharia.
  - Criado backup de segurança `bancodedadosclinica/clínica_original.mwb`.
  - Criado script DDL executável `bancodedadosclinica/01_schema_ddl.sql` (15 tabelas, chaves, índices, integridade e rastreabilidade RF/RN/RQ).
  - Criado script DML de seed `bancodedadosclinica/02_dados_iniciais.sql` compatível com a API C#.
  - Criada documentação técnica em `bancodedadosclinica/README.md` com DER em Mermaid e dicionário de dados.
- **Reorganização e Apresentação do Projeto:**
  - Criado índice centralizador em `docs/README.md` conectando requisitos, design SDD, diagramas, propostas comerciais e banco de dados.
  - Atualizado `README.md` raiz com badges, status das 4 fases, arquitetura, DER, instruções de execução e credenciais de teste.
- Próximo passo: Iniciar implementação dos repositórios MySQL em `ClinicaApp.Infrastructure.MySQL` (Dapper / EF Core).

## 5. Histórico Pessoal de Sessões

| Data | Agente / Ferramenta | O que foi feito | Próximo passo |
|---|---|---|---|
| 07/10/2026 | Antigravity AI | Blocos 1–3 concluídos (entidades, repositórios, Auth/Agenda/Prontuario/Financeiro services), 52 testes passando. | Banco de Dados II. |
| 08/10/2026 | Antigravity AI | Optou por MVP visual antes do MySQL; scaffold da `ClinicaApp.Api`. | Etapa 2 do Bloco 5 (`Program.cs` da API). |
| 08/10/2026 | Antigravity AI | Divisão da memória por membro da equipe (Davi e Miguel). | Continuar etapa 2 do Bloco 5. |
| 09/10/2026 | Antigravity AI | Integração completa front ↔ API na branch `feature/novas-telas`, correção de bugs de lógica (LGPD e data UTC), sincronização com wwwroot e criação do `api.js`. | Revisão da branch para merge em `main` e disciplina de Banco de Dados II (MySQL). |
| 10/10/2026 | Antigravity AI | Auditoria do modelo do Isaac; bateria de testes ponta a ponta (52 xUnit 100% OK); aplicação das 4 melhorias no `.mwb`; geração de `01_schema_ddl.sql`, `02_dados_iniciais.sql` e reorganização completa do repositório (`README.md`, `docs/README.md`). | Implementar repositórios MySQL concretos no backend C#. |
