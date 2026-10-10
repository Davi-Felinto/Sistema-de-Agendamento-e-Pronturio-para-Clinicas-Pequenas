# 🧠 Memória Pessoal — Miguel (Desenvolvimento de Interface Web)
> **Carregue este arquivo quando o usuário se identificar como Miguel.**
> Leia também a memória compartilhada: [`../CONTEXTO_MEMORIA_PROJETO.md`](../CONTEXTO_MEMORIA_PROJETO.md).
> **Última Atualização:** 10/10/2026

---

## 1. Perfil
- **Nome:** Miguel — **Responsável pela disciplina de Desenvolvimento de Interface Web**.
- **Formação:** CEUB, Engenharia de Software (colega do Davi).
- **Background técnico:** ⚠️ *A preencher na primeira sessão.* Pergunte ao Miguel: experiência com HTML/CSS/JS, React, Tailwind, Git/GitHub, e se prefere digitar o código (modo pedagógico) ou receber arquivos prontos.

## 2. Área de Responsabilidade — **somente as TELAS**
- **Front-end visual:** `index.html` na raiz (React via CDN + Tailwind, ~2870 linhas), publicado no GitHub Pages:
  `https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/`
- **Missão atual:** analisar o `index.html` existente e propor/implementar melhorias de interface: layout, consistência visual, componentes, fluxo das telas, usabilidade, acessibilidade e responsividade (`RQxx` ligados à interface).
- Desenhar como as mensagens de erro de negócio aparecem na tela (ex.: conflito de horário RN01/RN02, cancelamento tardio RN10, mensagens amigáveis RQ11) — o Davi liga essas mensagens à API depois.

## 3. Fluxo de Trabalho Paralelo (Davi ↔ Miguel)
Para evitar retrabalho, o Davi e o Miguel trabalharão **em paralelo** usando o conceito de uma "camada de serviço do front" (`api.js`).
1. **O Davi** vai criar o `api.js` na branch principal e integrar as telas atuais com a API real em C#.
2. **O Miguel** vai usar os mesmos métodos do `api.js` (como `api.listarPacientes()`) nas telas novas, mas enquanto a API real não chega para ele, ele cria um arquivo `api.js` provisório (Mock) que devolve dados falsos. **NUNCA use `fetch()` ou `localStorage` diretamente nos componentes visuais.**
3. Quando as telas estiverem prontas, basta trocar o `api.js` falso do Miguel pelo verdadeiro do Davi, e tudo funcionará instantaneamente.

### Contrato de Métodos (Assinaturas do `api.js`)
Para não haver nenhum retrabalho, o Miguel **deve obrigatoriamente** usar estes exatos nomes de funções. Como a API real será assíncrona, o mock do Miguel também deve ser `async` e retornar `Promise`.

**Pacientes:**
- `async api.listarPacientes()` ➔ Retorna array de Pacientes.
- `async api.obterPaciente(id)` ➔ Retorna o objeto Paciente.
- `async api.criarPaciente(dadosPaciente)` ➔ Retorna `{ sucesso: true, dados: Paciente }` ou `{ sucesso: false, erro: "...", mensagem: "..." }`.
- `async api.atualizarPaciente(id, dadosPaciente)` ➔ Retorna sucesso ou erro.
- `async api.inativarPaciente(id)` ➔ Retorna sucesso ou erro (RN16).

**Agendamentos:**
- `async api.listarAgendamentos()` ➔ Retorna array de Agendamentos.
- `async api.agendarConsulta(dadosAgendamento)` ➔ Retorna sucesso ou erro (ex: RN01/RN02 - Conflito de horário).
- `async api.cancelarAgendamento(id)` ➔ Retorna sucesso ou erro (ex: RN10 - Cancelamento tardio).

*(Se o Miguel precisar de filtros de data ou busca, ele deve anotar como pedido na seção 6).*

### Contrato de Dados (O que o `api.js` retorna/recebe)
Os objetos trafegados devem respeitar o backend em C#.

**Paciente:**
```json
{
  "id": 1,
  "nome": "João Silva",
  "documentoIdentificacao": "123.456.789-00",
  "dataNascimento": "1990-05-15T00:00:00",
  "telefone": "11999999999",
  "email": "joao@email.com",
  "endereco": "Rua X, 123",
  "alergias": "Amendoim",
  "condicoesPreexistentes": "Hipertensão",
  "ativo": true
}
```

**Agendamento:**
```json
{
  "id": 10,
  "pacienteId": 1,
  "profissionalId": 2,
  "dataHoraInicio": "2026-10-15T14:00:00",
  "dataHoraFim": "2026-10-15T15:00:00",
  "status": 0, /* 0: Pendente, 1: Concluido, 2: Cancelado */
  "observacoes": "Primeira consulta",
  "cancelamentoTardio": false
}
```

**Erros de Regra de Negócio (ex: RN01 - Conflito):**
Sempre que uma ação falhar, a API retornará um erro padrão. O Miguel deve desenhar o alerta/modal que exibe a propriedade `mensagem`.
```json
{
  "sucesso": false,
  "erro": "ConflitoHorario",
  "mensagem": "Já existe uma consulta marcada neste horário para este profissional."
}
```

## 4. Limites e Coordenação
- ❌ **Integração com a API NÃO é do Miguel.** A troca de `localStorage` por `fetch()` (Bloco 5, etapa 5) é do **Davi**. O Miguel deve apenas focar nas telas e usar chamadas para os métodos do `api.js` Mock.
- **Não altere** `src/` (Domínio, Serviços, `ClinicaApp.Api`) — responsabilidade do Davi.
- ⚠️ Trabalhar em branch separada (ex: `feature/novas-telas`). O Davi fará a integração na branch dele (`feature/integracao-api`).

## 5. Regras de Condução (a confirmar com o Miguel)
1. Explicar o raciocínio (requisito `RFxx`/`RQxx` + motivo de UX/técnico) antes de apresentar código.
2. Modo pedagógico (Miguel digita) **por padrão**, até ele dizer o contrário — registre a preferência aqui.
3. Comentários no código apenas para rastreabilidade de requisitos.

## 6. Onde o Miguel parou
- Ainda não iniciou sessões com agentes de IA. O Davi preparou todo o ambiente backend e banco de dados via Docker para permitir testes fáceis das telas com dados reais.

### 📋 Fila de Tarefas Pendentes do Miguel

1. **[PRIORIDADE] Subir o Ambiente Docker e Testar o Sistema Povoado:**
   - 📖 **Guia Passo a Passo:** [`docs/guias/GUIA_DOCKER_MIGUEL.md`](../../docs/guias/GUIA_DOCKER_MIGUEL.md)
   - Instalar o Docker Desktop para Windows (marcando a opção WSL 2).
   - Rodar `git pull origin main` e executar `docker compose up -d` na raiz do projeto.
   - Acessar a aplicação em [http://localhost:5055](http://localhost:5055) e testar os logins:
     - **Dr. Davi Felinto:** `Davi` / `Davi123!` (médico/admin)
     - **Juliana Lima:** `Juliana` / `Juliana123!` (recepcionista)
   - Inspecionar os dados do banco no navegador via phpMyAdmin em [http://localhost:8085](http://localhost:8085) (`clinix_db`).
   - Navegar pelos módulos com os dados de outubro de 2026: 12 pacientes, 37 consultas na agenda, 12 prontuários e 37 lançamentos financeiros.

2. **Mapeamento de Telas e Melhorias de UI/UX:**
   - Conhecer o `index.html`, mapear o fluxo das telas existentes e listar pontos de melhoria de usabilidade, responsividade mobile e acessibilidade.
   - Desenhar tratamento visual amigável para regras de negócio (modais e avisos para `RN01/RN02` conflito de horário, `RN10` cancelamento tardio, validação de CPF e campos obrigatórios).
   - Manter as chamadas desacopladas via contrato `api.js`.

### Pedidos pendentes ao Davi
- *(nenhum ainda)*

## 7. Histórico Pessoal de Sessões

| Data | Agente / Ferramenta | O que foi feito | Próximo passo |
|---|---|---|---|
| 08/10/2026 | Antigravity AI (a pedido do Davi) | Criada a memória pessoal do Miguel. Escopo definido: apenas telas; integração com API fica com o Davi. | Primeira sessão: preencher perfil e levantar melhorias das telas do `index.html`. |
| 10/10/2026 | Antigravity AI (a pedido do Davi) | Preparado ambiente Docker (MySQL 8.0 com seed completo de outubro/2026, API C# e phpMyAdmin) e criado guia detalhado em `docs/guias/GUIA_DOCKER_MIGUEL.md`. | Subir ambiente com `docker compose up -d`, testar sistema com dados reais e listar melhorias de UI/UX. |

