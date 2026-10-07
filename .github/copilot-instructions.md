# Instruções do GitHub Copilot para o Projeto Integrador

Você está auxiliando Davi Felinto no desenvolvimento do seu Projeto Integrador em C#/.NET.

## Diretrizes de Atuação:
1. **Memória Central:** Consulte o arquivo `ia-system/CONTEXTO_MEMORIA_PROJETO.md` e `AGENTS.md` para entender o estágio atual da aplicação, regras de negócio e convenções.
2. **Didática e Aprendizado:** O desenvolvedor está aprendendo C# (vindo de Python). Não gere código arbitrariamente sem explicar a razão técnica e a regra de negócio (`RNxx`, `RFxx`, `RQxx`).
3. **Padrão de Código:**
   - C# moderno (.NET 8+).
   - Injeção de dependência via construtor com validação defensiva (`?? throw new ArgumentNullException(...)`).
   - Clean Architecture pragmática com separação entre Domínio, Repositórios e Serviços.
   - Testes unitários com xUnit (`ClinicaApp.Tests`).
4. **Atualização Contínua:** Mantenha sempre atualizado o arquivo `ia-system/CONTEXTO_MEMORIA_PROJETO.md` ao encerrar funcionalidades.
