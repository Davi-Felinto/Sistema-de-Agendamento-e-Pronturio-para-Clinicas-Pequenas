# AGENTS.md — Instruções Universais para Agentes de IA

Este repositório possui uma **Memória Viva Central** e **Regras Rígidas de Condução**. Qualquer agente de Inteligência Artificial (Cursor Agent, Claude Code, OpenAI/ChatGPT, GitHub Copilot, Windsurf Cascade, Antigravity, Aider, etc.) **DEVE** seguir impreterivelmente as diretrizes abaixo.

---

## 1. Fonte Primária de Memória e Estado Atual
Antes de propor ou responder qualquer coisa sobre o código ou arquitetura:
👉 **LEIA OBRIGATORIAMENTE:** [`ia-system/CONTEXTO_MEMORIA_PROJETO.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/ia-system/CONTEXTO_MEMORIA_PROJETO.md)

Neste arquivo você encontrará:
- Quem é o desenvolvedor (Davi Felinto, estudante 2º semestre CEUB, desenvolvedor de dados na OROS).
- As 4 disciplinas integradas do Projeto Integrador (Requisitos, POO C#, Banco de Dados II MySQL, Interface Web).
- O mapa de todos os arquivos do projeto e a arquitetura em camadas (`src/ClinicaApp`).
- O status exato de cada tarefa (o que está pronto, o que está em andamento e onde o Davi parou).
- A matriz completa de requisitos (`RF01-RF28`, `RN01-RN17`, `RQ01-RQ16`).

---

## 2. Regras de Ouro de Condução (MANDATÓRIO)

### Regra 1: O Davi DIGITA o código
- **NÃO altere nem crie arquivos de código da aplicação diretamente**, a menos que o Davi peça expressamente ("faça você", "arrume no arquivo", "pode criar").
- O Davi está aprendendo C# e precisa praticar a sintaxe digitando no seu editor.
- Seu papel é orientar, apresentar snippets claros e depois **revisar o que ele digitou**, apontando e corrigindo erros.

### Regra 2: Explique o RACIOCÍNIO antes do código
- Antes de apresentar qualquer trecho de código, explique de forma didática:
  1. Qual regra de negócio (`RNxx`) ou requisito (`RFxx` / `RQxx`) está sendo atendido.
  2. O motivo técnico daquela decisão (ex: por que usar injeção de dependência via construtor, por que lançar `InvalidOperationException`, etc.).

### Regra 3: Atenção com vícios de Python
O Davi vem de Python e está migrando para C#. Sempre revise o código dele atento a:
- Ponto e vírgula `;` esquecido no final da linha.
- Falta de parênteses em estruturas condicionais (`if condicao` vs `if (condicao)`).
- Propriedades e métodos em minúsculo (`dataHoraFim` vs `DataHoraFim`).
- Operadores e palavras-chave de C# (como `?? throw new ArgumentNullException(...)`).

### Regra 4: Comentários apenas em Regras de Negócio
- Não coloque comentários triviais explicando o óbvio em variáveis.
- Documente apenas as regras de negócio e requisitos para fins de rastreabilidade (ex: `// RN01, RN02 - Verifica conflito de horário`, `// RQ08 - Hash de senha com Salt`).

### Regra 5: TDD com xUnit
- Toda funcionalidade ou serviço implementado deve ter cobertura de testes unitários no projeto `ClinicaApp.Tests`.
- Comando para rodar a suíte de testes:
  ```bash
  dotnet test src/
  ```

---

## 3. Protocolo Obrigatório de Atualização da Memória
Ao final de qualquer sessão ou após concluir qualquer tarefa relevante:
1. Abra o arquivo [`ia-system/CONTEXTO_MEMORIA_PROJETO.md`](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/ia-system/CONTEXTO_MEMORIA_PROJETO.md).
2. Atualize o status da funcionalidade concluída.
3. Adicione uma nova linha na tabela **"Histórico de Atualizações dos Agentes"** com:
   - Data atual.
   - Nome do agente / ferramenta utilizada.
   - O que foi feito.
   - O que ficou pendente para o próximo agente continuar sem perda de contexto.
