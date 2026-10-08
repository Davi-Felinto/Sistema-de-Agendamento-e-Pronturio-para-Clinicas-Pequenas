# 🧠 Memória Pessoal — Davi Felinto (Dev Principal)
> **Carregue este arquivo quando o usuário se identificar como Davi.**
> Leia também a memória compartilhada: [`../CONTEXTO_MEMORIA_PROJETO.md`](../CONTEXTO_MEMORIA_PROJETO.md).
> **Última Atualização:** 08/10/2026

---

## 1. Perfil
- **Nome:** Davi Felinto — **Dev principal / responsável pelo projeto**.
- **Formação:** CEUB, 2º semestre de Engenharia de Software.
- **Atuação:** OROS Soluções Educacionais (gestão de dados e plataformas educacionais).
- **Background técnico:** Forte em Python, aprendendo C# e POO especificamente para este projeto.
- **GitHub:** `github.com/Davi-Felinto` (git: `Davi Felinto <davifd0978@gmail.com>`).

## 2. Área de Responsabilidade
- **POO C# / .NET** (Domínio, Serviços, Testes xUnit) — fase ativa.
- **API `ClinicaApp.Api`** (Bloco 5 — MVP visual integrado).
- **Integração front ↔ API:** trocar `localStorage` (`clinix_*`) por `fetch()` no `index.html` e ligar as mensagens de erro da API às telas (o Miguel cuida só do visual das telas).
- **Banco de Dados II (MySQL)** — junto com o Isaac (fase seguinte).
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
- **Bloco 5, etapa 2:** configurar o `Program.cs` da `ClinicaApp.Api` — repositórios InMemory como `AddSingleton`, serviços, seed de dados, `UseStaticFiles`, remover boilerplate `WeatherForecast`.
- Próximos: `PacientesController` → `AgendamentosController` (ver roadmap do Bloco 5 na memória compartilhada).
- Há commits locais ainda sem push (`027baa0`, `0492e68`).

## 5. Histórico Pessoal de Sessões

| Data | Agente / Ferramenta | O que foi feito | Próximo passo |
|---|---|---|---|
| 07/10/2026 | Antigravity AI | Blocos 1–3 concluídos (entidades, repositórios, Auth/Agenda/Prontuario/Financeiro services), 52 testes passando. | Banco de Dados II. |
| 08/10/2026 | Antigravity AI | Optou por MVP visual antes do MySQL; scaffold da `ClinicaApp.Api`. | Etapa 2 do Bloco 5 (`Program.cs` da API). |
| 08/10/2026 | Antigravity AI | Divisão da memória por membro da equipe (Davi e Miguel). | Continuar etapa 2 do Bloco 5. |

