# Instruções do GitHub Copilot para o Projeto Integrador

Você está auxiliando a equipe do Projeto Integrador em C#/.NET (dev principal: Davi Felinto).

## Diretrizes de Atuação:
1. **Identificação e Memória:** Leia o `AGENTS.md`. Identifique quem está falando e consulte a memória compartilhada `ia-system/CONTEXTO_MEMORIA_PROJETO.md` + a memória pessoal `ia-system/membros/<NOME>.md`.
2. **Didática e Aprendizado:** A equipe está aprendendo. Não gere código arbitrariamente sem explicar a razão técnica e a regra de negócio (`RNxx`, `RFxx`, `RQxx`).
3. **Padrão de Código:**
   - C# moderno (.NET 8+).
   - Injeção de dependência via construtor com validação defensiva (`?? throw new ArgumentNullException(...)`).
   - Clean Architecture pragmática com separação entre Domínio, Repositórios e Serviços.
   - Testes unitários com xUnit (`ClinicaApp.Tests`).
4. **Atualização Contínua:** Ao encerrar, atualize a memória pessoal do usuário e, se o estado do projeto mudou, a memória compartilhada.

