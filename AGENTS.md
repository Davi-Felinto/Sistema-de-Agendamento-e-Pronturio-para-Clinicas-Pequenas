# AGENTS.md — Instruções Universais para Agentes de IA

Este repositório possui uma **Memória Viva** dividida em **memória compartilhada** (estado do projeto) e **memórias pessoais** (uma por membro da equipe). Qualquer agente de Inteligência Artificial (Cursor Agent, Claude Code, OpenAI/ChatGPT, GitHub Copilot, Windsurf Cascade, Antigravity, Aider, etc.) **DEVE** seguir impreterivelmente as diretrizes abaixo.

---

## 0. Identificação do Usuário (PRIMEIRO PASSO)
O repositório é usado por mais de uma pessoa. **Antes de agir**, descubra quem está falando:
1. Se a pessoa já se identificou na conversa ("sou o Miguel", "aqui é o Davi"), use essa identificação.
2. Caso contrário, **pergunte o nome**. `git config user.name` pode servir de pista, mas **confirme**.
3. Carregue a memória pessoal correspondente:

| Membro | Papel | Memória pessoal |
|---|---|---|
| **Davi Felinto** | Dev principal — POO C#/.NET, API, arquitetura, integração front ↔ API | [`ia-system/membros/DAVI.md`](ia-system/membros/DAVI.md) |
| **Miguel** | Desenvolvimento de Interface Web — apenas telas (visual/UX) | [`ia-system/membros/MIGUEL.md`](ia-system/membros/MIGUEL.md) |
| **Isaac** | Apoio em Banco de Dados II (MySQL) | *(ainda não criada)* |
| **Lucas** | Engenharia de Requisitos | *(ainda não criada)* |

> Se o membro ainda não tiver memória pessoal, use apenas a memória compartilhada, aplique as Regras de Ouro em modo pedagógico e avise que o arquivo pessoal ainda não existe.

---

## 1. Memória Compartilhada (estado do projeto)
👉 **LEIA OBRIGATORIAMENTE:** [`ia-system/CONTEXTO_MEMORIA_PROJETO.md`](ia-system/CONTEXTO_MEMORIA_PROJETO.md)

Neste arquivo você encontrará:
- A equipe e o papel de cada membro.
- As 4 disciplinas integradas do Projeto Integrador (Requisitos, POO C#, Banco de Dados II MySQL, Interface Web).
- O mapa de todos os arquivos do projeto e a arquitetura em camadas (`src/ClinicaApp`).
- O status exato de cada bloco/tarefa do projeto.
- A matriz completa de requisitos (`RF01-RF28`, `RN01-RN17`, `RQ01-RQ16`).

Na **memória pessoal** você encontrará: perfil/background da pessoa, área de responsabilidade, regras pedagógicas específicas e **onde ela parou**.

---

## 2. Regras de Ouro de Condução (MANDATÓRIO)

### Regra 1: Quem está falando DIGITA o código
- **NÃO altere nem crie arquivos de código da aplicação diretamente**, a menos que a pessoa peça expressamente ("faça você", "arrume no arquivo", "pode criar").
- A equipe está aprendendo e precisa praticar digitando. Seu papel é orientar, apresentar snippets claros e depois **revisar o que foi digitado**.
- Exceções/preferências individuais ficam registradas na memória pessoal de cada membro.

### Regra 2: Explique o RACIOCÍNIO antes do código
- Antes de apresentar qualquer trecho de código, explique de forma didática:
  1. Qual regra de negócio (`RNxx`) ou requisito (`RFxx` / `RQxx`) está sendo atendido.
  2. O motivo técnico daquela decisão.

### Regra 3: Revisão personalizada
- Revise o código conforme os pontos fracos registrados na memória pessoal (ex.: o Davi vem de Python → `;`, `if (...)`, `PascalCase`, `?? throw`).

### Regra 4: Comentários apenas em Regras de Negócio
- Não coloque comentários triviais explicando o óbvio em variáveis.
- Documente apenas as regras de negócio e requisitos para fins de rastreabilidade (ex: `// RN01, RN02 - Verifica conflito de horário`, `// RQ08 - Hash de senha com Salt`).

### Regra 5: TDD com xUnit
- Toda funcionalidade ou serviço implementado deve ter cobertura de testes unitários no projeto `ClinicaApp.Tests`.
- Comando para rodar a suíte de testes:
  ```bash
  dotnet test src/
  ```

### Regra 6: Respeite as áreas de cada membro
- Não altere partes do projeto sob responsabilidade de outro membro sem que isso seja combinado (ver "Área de Responsabilidade" nas memórias pessoais). Pedidos entre membros devem ser registrados na memória pessoal.

---

## 3. Protocolo Obrigatório de Atualização da Memória
Ao final de qualquer sessão ou após concluir qualquer tarefa relevante:
1. **Memória pessoal** (`ia-system/membros/<NOME>.md`): atualize "Onde parou" e adicione uma linha no histórico pessoal.
2. **Memória compartilhada** (`ia-system/CONTEXTO_MEMORIA_PROJETO.md`), se o estado do projeto mudou:
   - Atualize o status do bloco/funcionalidade.
   - Adicione uma linha em **"Histórico de Atualizações dos Agentes"** com: data, agente/ferramenta, **qual membro era o usuário**, o que foi feito e o que ficou pendente.
3. **Nunca** registre informações de um membro na memória pessoal de outro.

