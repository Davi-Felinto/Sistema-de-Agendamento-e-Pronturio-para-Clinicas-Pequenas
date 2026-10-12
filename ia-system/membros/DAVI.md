# 🧠 Memória Pessoal — Davi Felinto (Dev Principal)
> **Carregue este arquivo quando o usuário se identificar como Davi.**
> Leia também a memória compartilhada: [`../CONTEXTO_MEMORIA_PROJETO.md`](../CONTEXTO_MEMORIA_PROJETO.md).
> **Última Atualização:** 09/10/2026

---

## 1. Perfil
- **Nome:** Davi Felinto — **Dev principal / responsável pelo projeto**.
- **Formação:** CEUB, 2º semestre de Engenharia de Software.
- **Atuação:** OROS Soluções Educacionais (gestão de dados e plataformas educacionais).
- **Background técnico:** Forte em Python, aprendendo C# e POO especificamente para este projeto.
- **GitHub:** `github.com/Davi-Felinto` (git: `Davi Felinto <davifd0978@gmail.com>`).

## 2. Área de Responsabilidade
- **POO C# / .NET** (Domínio, Serviços, Testes xUnit) — 100% concluído.
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
- **Bloco 5 (MVP Visual Integrado) 100% Concluído:**
  - Telas novas do Miguel (`feature/novas-telas`) integradas com a API C# (`ClinicaApp.Api`) com fallback gracioso para localStorage / GitHub Pages.
  - Sincronização automática entre `index.html` e `src/ClinicaApp.Api/wwwroot/index.html`.
  - Criado módulo `api.js` com o contrato assíncrono de serviços.
  - Corrigidos bugs identificados pelo Miguel: regex `/\D/g` na exportação LGPD (RQ02) e cálculo local de data em `getTodayDateStr` evitando virada após as 21h em Brasília.
  - Endpoints validados: `PacientesController` (cadastro, listagem, inativação LGPD RN16/RQ03), `AgendamentosController` (agendamento com bloqueio RN01/RN02, cancelamento tardio RN10), `AuthController` (login seguro com salt RQ08).
- Próximo passo: Merge de `feature/novas-telas` em `main` e início do Bloco de Banco de Dados II (MySQL com o Isaac).

## 5. Histórico Pessoal de Sessões

| Data | Agente / Ferramenta | O que foi feito | Próximo passo |
|---|---|---|---|
| 07/10/2026 | Antigravity AI | Blocos 1–3 concluídos (entidades, repositórios, Auth/Agenda/Prontuario/Financeiro services), 52 testes passando. | Banco de Dados II. |
| 08/10/2026 | Antigravity AI | Optou por MVP visual antes do MySQL; scaffold da `ClinicaApp.Api`. | Etapa 2 do Bloco 5 (`Program.cs` da API). |
| 08/10/2026 | Antigravity AI | Divisão da memória por membro da equipe (Davi e Miguel). | Continuar etapa 2 do Bloco 5. |
| 09/10/2026 | Antigravity AI | Integração completa front ↔ API na branch `feature/novas-telas`, correção de bugs de lógica (LGPD e data UTC), sincronização com wwwroot e criação do `api.js`. | Revisão para merge em `main` e disciplina de Banco de Dados II (MySQL). |

