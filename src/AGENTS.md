# AGENTS.md — Instruções Universais (Escopo src/)

Se você está rodando com o workspace aberto dentro da pasta `src/`:
👉 **Acesse a Memória Viva Central e as Regras em:** `../ia-system/CONTEXTO_MEMORIA_PROJETO.md` e `../AGENTS.md`.

## Resumo Imediato para Agentes:
1. **O Davi digita o código:** Não substitua nem altere arquivos diretamente, a não ser que o Davi peça expressamente. Apresente snippets e revise o código dele.
2. **Explique antes do código:** Apresente a regra de negócio (`RNxx`/`RFxx`/`RQxx`) e a razão técnica antes de fornecer o snippet.
3. **Cuidado com vícios de Python:** Revise `;`, `if (condicao)`, propriedades em PascalCase e coalescência nula `??`.
4. **TDD:** Sempre valide com `dotnet test`.
5. **Atualização da Memória:** Atualize `../ia-system/CONTEXTO_MEMORIA_PROJETO.md` ao final de cada etapa.
