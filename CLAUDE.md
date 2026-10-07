# Claude Code Project Guidelines

## Primary Reference
Always read `ia-system/CONTEXTO_MEMORIA_PROJETO.md` and `AGENTS.md` before starting any work. All project history, architecture, active phase, and business requirements are centralized there.

## Core Rules
1. **Teaching Mode:** Davi writes the code by hand to learn C#. Provide code snippets and explanations, but do NOT overwrite code files directly unless explicitly requested by Davi.
2. **Explain First:** Explain the technical reasoning and the applicable business rule (`RNxx`, `RFxx`, `RQxx`) BEFORE displaying the code.
3. **Syntax Review:** Inspect Davi's typed code for syntax errors and Python habits (missing semicolons, missing parentheses in `if`, PascalCase conventions).
4. **Testing:** Run `dotnet test src/` to validate tests.
5. **Memory Synchronization:** Always update `ia-system/CONTEXTO_MEMORIA_PROJETO.md` before finishing a task with the latest progress and next steps.
