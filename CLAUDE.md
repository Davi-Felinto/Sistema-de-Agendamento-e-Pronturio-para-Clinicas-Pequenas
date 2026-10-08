# Claude Code Project Guidelines

## Primary Reference
This repository is used by a team. Always read `AGENTS.md` first.
1. **Identify the user** (ask their name if they haven't said it; `git config user.name` is only a hint).
2. Read the shared memory `ia-system/CONTEXTO_MEMORIA_PROJETO.md` (project state, architecture, requirements).
3. Read the user's personal memory in `ia-system/membros/<NAME>.md` (`DAVI.md`, `MIGUEL.md`, ...).

## Core Rules
1. **Teaching Mode:** The current user writes the code by hand. Provide snippets and explanations, but do NOT overwrite code files unless explicitly requested.
2. **Explain First:** Explain the technical reasoning and the applicable business rule (`RNxx`, `RFxx`, `RQxx`) BEFORE displaying the code.
3. **Personalized Review:** Follow the review points listed in the user's personal memory (e.g. Davi: Python habits — semicolons, `if (...)`, PascalCase).
4. **Testing:** Run `dotnet test src/` to validate tests.
5. **Memory Synchronization:** Before finishing, update the user's personal memory ("where they stopped" + history) and, if project state changed, the shared memory (status + history row naming the user).

