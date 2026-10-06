# Roteiro de Apresentação do Sistema para Cliente
## Sistema de Agendamento, Prontuário e Gestão Clínica (Clinix)

> **Documento de Condução Comercial, Demonstração Prática e Status do Projeto**  
> **Autor:** Davi Felinto — Engenharia de Software (CEUB)  
> **Público-alvo:** Profissional de Saúde / Gestor(a) de Clínica Pequena interessado(a) na solução  
> **Duração estimada da reunião:** 35 a 45 minutos  
> **Versão:** 1.0 — Outubro / 2026  

---

## 1. Visão Geral e Estrutura da Apresentação

O objetivo desta apresentação é transformar o interesse da cliente em confiança e adoção prática do sistema. A reunião não deve focar apenas em detalhes puramente acadêmicos, mas em demonstrar **como o sistema resolve os gargalos diários do consultório** (faltas de pacientes, agenda desorganizada, insegurança jurídica no prontuário e descontrole financeiro), evidenciando a maturidade e a solidez da engenharia por trás do projeto.

### ⏱️ Cronograma Sugerido da Reunião

| Bloco | Etapa | Foco Principal | Tempo |
| :---: | :--- | :--- | :---: |
| **1** | **Checklist Pré-Apresentação** | Preparação de ambiente, telas e dados de teste | *Antes* |
| **2** | **Abertura & Diagnóstico das Dores** | Escuta ativa: entender a rotina real da cliente | 5 a 7 min |
| **3** | **Posicionamento & Proposta de Valor** | Apresentar o propósito e os 4 pilares do sistema | 3 a 5 min |
| **4** | **Demonstração Prática (Ao Vivo)** | Conduzir o fluxo completo na interface interativa | 15 a 20 min |
| **5** | **Andamento do Projeto & Engenharia** | Transparência: status atual, testes em C# e LGPD | 5 min |
| **6** | **Feedback, Objeções & Próximos Passos** | Alinhamento de piloto e proposta de implantação | 5 a 10 min |

---

## 2. Checklist Pré-Apresentação (Antes da Reunião)

Antes de entrar na chamada de vídeo ou reunião presencial:

- [ ] **Ambiente de Demonstração Aberto:** Abra o arquivo `index.html` em navegador moderno (Chrome, Edge ou Firefox). Pressione `F11` para colocar em tela cheia e evitar distrações.
- [ ] **Aba Inicial:** Deixe a tela posicionada na aba **"Visão Geral / Dashboard"**.
- [ ] **Dados de Demonstração Carregados:** Certifique-se de que há pacientes fictícios (ex.: Mariana Duarte, Carlos Neves, Beatriz Vasconcelos) e consultas para hoje.
- [ ] **Bloco de Anotações:** Mantenha um bloco à mão para anotar termos e rotinas específicas da cliente (ex.: se ela utiliza "anamnese", "evolução", "paciente" ou "cliente").
- [ ] **Postura Consultiva:** Lembre-se: o foco é resolver os problemas dela. Demonstre segurança e ouça antes de apresentar soluções.

---

## 3. Bloco 1: Abertura e Diagnóstico das Dores (5 a 7 min)

> **Regra de Ouro:** Não comece abrindo telas imediatamente. Quem ouve primeiro identifica os pontos críticos e conduz a demonstração exatamente nas dores que mais incomodam a cliente.

### 🗣️ Exemplo de Fala de Abertura:
> *"Olá, [Nome da Cliente], muito obrigado pela oportunidade de conversarmos hoje! Antes de abrir telas ou botões, quero entender um pouco da sua rotina prática. O Clinix foi projetado especificamente para profissionais e clínicas que querem fugir da burocracia de sistemas pesados e caros, focando em simplicidade, segurança jurídica e agilidade no atendimento. Como funciona o seu dia a dia hoje?"*

### ❓ Perguntas Investigativas:
1. **Agendamento e Faltas:**
   - *"Como você agenda suas consultas hoje? É por papel, WhatsApp ou planilha?"*
   - *"Você costuma ter problemas com pacientes faltando ou desmarcando em cima da hora?"*
2. **Prontuário e Histórico:**
   - *"Onde ficam salvas as anotações e evoluções de cada paciente? É fácil recuperar o histórico de atendimentos antigos durante uma consulta?"*
3. **Controle Financeiro:**
   - *"Como você controla quem já pagou, quem ficou devendo no final do dia e quanto você faturou no mês?"*
4. **Equipe e Sigilo:**
   - *"Você atende sozinha ou conta com recepcionista/secretária? Há preocupação com o sigilo das fichas médicas?"*

---

## 4. Bloco 2: Posicionamento e Proposta de Valor (3 a 5 min)

Após ouvir as respostas da cliente, conecte as dores citadas com a proposta de valor do sistema:

### 🗣️ Exemplo de Fala:
> *"Entendo perfeitamente o seu cenário. O profissional de saúde precisa gastar o seu tempo e energia cuidando de pessoas, não preenchendo formulários lentos ou apagando incêndios de horários duplicados. O nosso sistema foi concebido sobre 4 pilares centrais:*
> 
> 1. * **Agendamento em até 3 cliques:** Interface limpa e rápida com bloqueio automático de choques de horário.
> 2. * **Redução ativa de faltas:** Confirmação imediata e lembretes automáticos no WhatsApp 24 horas antes da consulta.
> 3. * **Prontuário com respaldo ético e legal:** Histórico imutável de anotações (cada edição mantém a versão anterior arquivada com data e motivo).
> 4. * **Financeiro integrado e sem planilhas:** Baixa de pagamento no encerramento da consulta e fechamento mensal automático."*

---

## 5. Bloco 3: Como o Sistema Vai Funcionar (Demonstração Prática - 15 a 20 min)

Aqui você compartilha a tela com a aplicação aberta. Siga o fluxo natural da clínica, da recepção ao pós-atendimento:

```
Fluxo da Demonstração:
┌────────────────────────┐    ┌────────────────────────┐    ┌────────────────────────┐
│ 1. Painel do Dia       │ -> │ 2. Agenda Inteligente  │ -> │ 3. Prontuário Seguro   │
│ (Dashboard de Métricas)│    │ (Anti-Conflito + Whats)│    │ (Histórico Imutável)   │
└────────────────────────┘    └────────────────────────┘    └────────────────────────┘
                                                                       │
┌────────────────────────┐    ┌────────────────────────┐               │
│ 5. Segurança & LGPD    │ <- │ 4. Baixa Financeira    │ <-------------┘
│ (Perfis e Auditoria)   │    │ (Pix/Cartão + Relatório│
└────────────────────────┘    └────────────────────────┘
```

---

### Passo 1: Abertura do Dia — Painel de Controle (Dashboard)
- **O que mostrar:** Aba **"Visão Geral"**. Destaque os cartões de métricas (Consultas de hoje, Faturamento previsto, Pacientes cadastrados) e a lista rápida de atendimentos do dia.
- **O que falar:**
  > *"Logo no início da manhã, ao abrir o sistema, você tem a fotografia completa do seu dia: quantos pacientes vai atender, horários, quem já confirmou e lembretes pendentes. Tudo em uma única tela, sem precisar folhear cadernos ou abrir várias abas."*

---

### Passo 2: O Agendamento em 3 Cliques e Proteção Anti-Conflito
- **O que mostrar:** Clique na aba **"Agenda Clínica"**.
- **Demonstração A (Agendamento rápido):**
  - Clique em um horário vago (ex.: 10:00).
  - Selecione o paciente, tipo de consulta e confirme.
  - Aponte que o agendamento foi concluído em segundos.
- **Demonstração B (Inteligência Anti-Conflito — RN01 / RN02):**
  - Tente propositalmente marcar outro paciente no mesmo horário das 10:00.
  - Mostre o sistema bloqueando a ação e emitindo o alerta de choque de horário.
- **O que falar:**
  > *"Aqui está uma das maiores proteções do sistema: se você ou sua recepcionista tentar agendar dois pacientes no mesmo horário por engano, o sistema impede a gravação e alerta na hora. Isso elimina o constrangimento de pacientes esperando ao mesmo tempo na sala de espera."*

---

### Passo 3: Automação de Confirmações e Lembretes via WhatsApp (RN03 / RN04)
- **O que mostrar:** O ícone e status de envio de mensagem ao lado da consulta na agenda.
- **O que falar:**
  > *"Assim que a consulta é confirmada, o sistema gera a confirmação direta no WhatsApp do paciente. Além disso, ele agenda automaticamente o disparo de um lembrete 24 horas antes do horário marcado. Estudos na área de saúde mostram que essa automação simples reduz as faltas e o esquecimento de pacientes em até 40%."*
- **Destaque extra (Cancelamento Tardio — RN10):** Mostre como o sistema detecta cancelamentos feitos a menos de 24 horas da consulta e marca na ficha do paciente para controle da taxa de desmarcação.

---

### Passo 4: O Momento do Atendimento — Prontuário com Histórico Imutável (RN17)
- **O que mostrar:** Navegue para a aba **"Prontuário Eletrônico"** (ou use o atalho de busca rápida `Ctrl + K`).
- **Demonstração A (Ficha e Linha do Tempo):**
  - Selecione a paciente Mariana Duarte.
  - Aponte o cabeçalho clínico com alertas destacados de alergias e condições pré-existentes.
  - Mostre a linha do tempo com os atendimentos anteriores organizados cronologicamente.
- **Demonstração B (Edição com Versionamento Imutável — O Diferencial Ético/Jurídico):**
  - Clique para editar uma anotação já registrada.
  - Modifique o texto (ex.: acrescentando uma dosagem ou evolução) e informe o motivo.
  - Salve e abra o **Histórico de Versões** daquela sessão, mostrando o texto antigo preservado com data, hora e motivo.
- **O que falar:**
  > *"Este é um dos recursos mais avançados e importantes para o profissional de saúde: a conformidade ética e jurídica. Em muitos sistemas simples ou blocos de notas, se você edita um prontuário, o texto antigo se perde. Aqui não: cada edição gera um snapshot permanente. O histórico de tudo o que foi escrito fica arquivado com data e motivo. Isso oferece tranquilidade e segurança total em casos de auditoria do Conselho Regional ou disputas judiciais."*

---

### Passo 5: Fechamento da Consulta e Controle Financeiro (RN07 / RN08 / RN09)
- **O que mostrar:** Aba **"Financeiro & Relatórios"**.
- **Demonstração:**
  - Mostre a lista de atendimentos finalizados.
  - Dê baixa em uma consulta pendente selecionando a forma de pagamento (Pix, Cartão ou Dinheiro).
  - Mostre o resumo consolidado do mês atualizando em tempo real com total arrecadado, pendente e atendimentos realizados.
- **O que falar:**
  > *"Assim que o paciente sai da sala, você ou sua recepção clica em 'Baixar Pagamento' e escolhe se foi Pix, cartão ou dinheiro. Se ficar para pagar depois, o sistema mantém o alerta na lista de pendências. No final do mês, você não precisa somar comprovantes ou bater extratos: o relatório consolidado mostra o faturamento exato da clínica com um clique."*

---

### Passo 6: Segurança, Controle de Perfis e LGPD (RN11, RN12, RN13, RN16)
- **O que mostrar:** Alterne o perfil de acesso no menu lateral (Profissional vs Administrador) e mostre a aba **"Auditoria & Logs"**.
- **O que falar:**
  > *"O sistema separa rigorosamente quem acessa o quê. Se você tiver uma recepcionista, ela pode agendar, emitir recibos e cadastrar contatos, mas ela **não tem permissão para ler prontuários médicos ou sigilosos**. Além disso, cada acesso a prontuário fica registrado em uma trilha de auditoria inviolável, e o cadastro de pacientes é totalmente aderente à LGPD, permitindo inativação com anonimização caso o paciente solicite a exclusão dos seus dados."*

---

## 6. Bloco 4: Andamento do Projeto e Rigor Técnico (5 min)

Nesta etapa, você demonstra a maturidade e a seriedade da construção do software, transmitindo credibilidade total.

### 📊 Painel de Status do Projeto

```
┌─────────────────────────────────┬─────────────────────────────────┬─────────────────────────────────┐
│ 1. ENGENHARIA DE REQUISITOS     │ 2. ARQUITETURA & TESTES (C#)    │ 3. INTERFACE E USABILIDADE      │
│ [✅ 100% CONCLUÍDO E HOMOLOGADO]│ [✅ 23 TESTES xUnit APROVADOS]   │ [🚀 MVP INTERATIVO OPERACIONAL] │
└─────────────────────────────────┴─────────────────────────────────┴─────────────────────────────────┘
```

### 🗣️ Exemplo de Fala:
> *"Para que você tenha total clareza sobre o momento atual do projeto e a nossa metodologia:
> 
> 1. **Fase 1 — Levantamento e Regras de Negócio (100% Concluída):** Mapeamos e homologamos 28 requisitos funcionais e 17 regras de negócio essenciais para o fluxo de consultórios de saúde.
> 2. **Fase 2 — Núcleo do Sistema e Testes de Confiabilidade (100% Concluída em C# / .NET 8):** Não construímos apenas telas; desenvolvemos o motor de regras com arquitetura profissional. Utilizamos a metodologia **TDD (desenvolvimento orientado por testes)**, com **23 testes automatizados rodando com 100% de sucesso**. Regras críticas como proteção contra choques de horário, cancelamentos tardios e histórico de versões são testadas por código antes de qualquer operação.
> 3. **Fase 3 — Experiência do Usuário (Fase Atual):** Desenvolvemos este protótipo interativo completo de alta fidelidade para validar a ergonomia e usabilidade diretamente com profissionais reais como você.
> 4. **Próximo Passo:** Estamos preparando a integração final com banco de dados dedicado na nuvem (MySQL) com rotina de backup diário para disponibilizar os primeiros acessos piloto."*

---

## 7. Bloco 5: Coleta de Feedback e Fechamento Comercial (5 a 10 min)

Conclua a reunião transformando a cliente em parceira de validação (co-autora da solução):

### ❓ Perguntas Estratégicas de Fechamento:
1. *"Olhando o fluxo que demonstramos agora, como você enxerga isso funcionando na sua rotina diária?"*
2. *"Teve alguma tela ou detalhe que chamou mais a sua atenção?"*
3. *"Na sua especialidade, existe algum campo clínico ou documento específico (ex.: anamnese personalizada, atestado ou recibo) que seria indispensável no seu primeiro dia de uso?"*

### 🎯 Proposta de Próximo Passo (Chamada para Ação):
> *"O nosso próximo passo ideal é cadastrar as configurações do seu consultório (seus horários de atendimento, serviços e valores) e liberar um período de testes piloto de 15 dias para você experimentar na prática e nos dar suas impressões. O que você acha de alinharmos os dados para iniciarmos esse piloto?"*

---

## 8. Guia de Respostas para Objeções e Dúvidas Comuns

| Dúvida da Cliente | Resposta Estruturada Recomendada |
| :--- | :--- |
| **"E se a internet cair durante o atendimento?"** | *"O sistema é leve e pode operar em modo local ou sincronizado na nuvem. Em caso de oscilações breves, seus dados na tela permanecem preservados."* |
| **"Consigo acessar pelo celular ou tablet?"** | *"Sim! A interface foi desenhada com layout totalmente responsivo. Você pode consultar a agenda ou o histórico de pacientes tanto no computador da clínica quanto no celular."* |
| **"Meus dados e prontuários estão seguros contra vazamentos?"** | *"Totalmente. Adotamos padrões rigorosos da LGPD: senhas criptografadas, perfis de acesso separados para que recepcionistas não vejam prontuários, e trilha de auditoria que registra cada consulta realizada."* |
| **"Já posso começar a usar hoje?"** | *"As telas e regras já estão 100% operacionais como você viu. Estamos finalizando a configuração da base de dados com rotina de backup automático para garantir segurança jurídica absoluta aos seus pacientes. Nosso cronograma prevê a liberação do piloto nas próximas semanas."* |
| **"O sistema emite nota fiscal ou faz teleconsulta?"** | *"Essas funcionalidades estão no nosso roadmap para as próximas versões. Essa parceria com você é excelente justamente para mapearmos as prioridades do seu consultório e incluirmos nas próximas entregas."* |

---

## 9. Anexo: Resumo Rápido para a Reunião (Guia de Bolso)

Mantenha esta tabela visível em uma segunda tela durante a apresentação:

```
┌───────┬───────────────────────────────────┬───────────────────────────────────────────────┐
│ MIN   │ ETAPA                             │ AÇÃO NA TELA / FALHA CHAVE                    │
├───────┼───────────────────────────────────┼───────────────────────────────────────────────┤
│ 00-05 │ Diagnóstico e Perguntas           │ Ouvir: "Como agenda?", "Faltas?", "Prontuário?"│
│ 05-08 │ Proposta de Valor                 │ Destacar: 3 cliques, WhatsApp, Prontuário, Fin│
│ 08-12 │ Agenda & Anti-Conflito            │ Agendar em 3 cliques -> Forçar conflito propos│
│ 12-16 │ Prontuário & Versionamento        │ Ver paciente -> Editar evolução -> Mostrar his│
│ 16-20 │ Financeiro & Relatórios           │ Baixar Pix/Cartão -> Ver faturamento consolid │
│ 20-24 │ Perfis, LGPD e Auditoria          │ Alternar Profissional/Admin -> Mostrar Logs   │
│ 24-28 │ Andamento & Engenharia C#         │ Citar 23 testes TDD xUnit, robustez e futuro  │
│ 28-35 │ Fechamento & Proposta de Piloto   │ "O que achou?" -> Propor teste de 15 dias     │
└───────┴───────────────────────────────────┴───────────────────────────────────────────────┘
```
