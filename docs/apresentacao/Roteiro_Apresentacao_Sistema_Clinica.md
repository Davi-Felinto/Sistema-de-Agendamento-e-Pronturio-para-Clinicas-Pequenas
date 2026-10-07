# Roteiro de Apresentação do Sistema para Cliente
## Sistema de Agendamento, Prontuário e Gestão Clínica (Clinix)

> **Documento de Condução Comercial, Demonstração Prática e Fechamento com a Cliente**  
> **Autor:** Davi Felinto — Engenharia de Software (CEUB)  
> **Público-alvo:** Profissional de Saúde / Gestor(a) de Clínica Pequena interessado(a) na solução  
> **Modelos Comerciais Integrados:** Assinatura Recorrente (SaaS) **e** Projeto Dedicado do Consultório (Não-SaaS)  
> **Duração estimada da reunião:** 35 a 45 minutos  
> **Versão:** 2.0 (100% Alinhada com a Proposta Comercial) — Outubro / 2026  

---

## 1. Visão Geral e Estrutura da Apresentação

O objetivo desta apresentação é transformar o interesse da cliente em confiança e adoção prática do sistema. A reunião não foca apenas em detalhes acadêmicos, mas em demonstrar **como o sistema resolve os gargalos diários do consultório** (faltas de pacientes, agenda desorganizada, insegurança jurídica no prontuário e descontrole financeiro), conectando a demonstração prática com uma proposta comercial flexível e sem barreiras.

### ⏱️ Cronograma da Reunião

| Bloco | Etapa | Foco Principal | Tempo |
| :---: | :--- | :--- | :---: |
| **1** | **Checklist Pré-Apresentação** | Preparação de ambiente, telas e dados de teste | *Antes* |
| **2** | **Abertura & Diagnóstico das Dores** | Escuta ativa: entender a rotina real da cliente | 5 a 7 min |
| **3** | **Posicionamento & Proposta de Valor** | Apresentar o propósito e os 4 pilares do sistema | 3 a 5 min |
| **4** | **Demonstração Prática (Ao Vivo)** | Conduzir o fluxo completo na interface interativa | 15 a 20 min |
| **5** | **Andamento do Projeto & Engenharia** | Transparência: status atual, testes em C# e LGPD | 5 min |
| **6** | **Proposta Comercial & Fechamento** | Apresentação das Duas Portas (SaaS vs Não-SaaS) e piloto | 5 a 10 min |

---

## 2. Checklist Pré-Apresentação (Antes da Reunião)

Antes de entrar na chamada de vídeo ou reunião presencial:

- [ ] **Ambiente de Demonstração Aberto:** Abra o arquivo `index.html` em navegador moderno (Chrome, Edge ou Firefox). Pressione `F11` para colocar em tela cheia e evitar distrações.
- [ ] **Aba Inicial:** Deixe a tela posicionada na aba **"Visão Geral / Dashboard"**.
- [ ] **Dados de Demonstração Carregados:** Certifique-se de que há pacientes fictícios (ex.: Mariana Duarte, Carlos Neves, Beatriz Vasconcelos) e consultas para hoje.
- [ ] **Documentos de Apoio:** Tenha à mão a [Proposta Comercial](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Proposta_Precificacao_Estrategia_Comercial.md) com a minuta de adesão.
- [ ] **Bloco de Anotações:** Mantenha um bloco para anotar termos e procedimentos específicos da cliente.
- [ ] **Postura Consultiva:** Lembre-se: o foco é resolver os problemas dela. Ouça antes de apresentar soluções.

---

## 3. Bloco 1: Abertura e Diagnóstico das Dores (5 a 7 min)

> **Regra de Ouro:** Não comece abrindo telas imediatamente. Quem ouve primeiro identifica os pontos críticos da rotina e conduz a demonstração exatamente nas dores que mais incomodam a cliente.

### 🗣️ Exemplo de Fala de Abertura:
> *"Olá, [Nome da Cliente], muito obrigado pela oportunidade de conversarmos hoje! Antes de abrir telas ou botões, quero entender um pouco da sua rotina prática. O Clinix foi projetado especificamente para profissionais e clínicas que querem fugir da burocracia de sistemas pesados e caros, focando em simplicidade, segurança jurídica e agilidade no atendimento. Como funciona o seu dia a dia no consultório hoje?"*

### ❓ Perguntas Investigativas:
1. **Agendamento e Faltas:**
   - *"Como você agenda suas consultas hoje? É por papel, WhatsApp ou planilha?"*
   - *"Você costuma ter problemas com pacientes faltando ou desmarcando em cima da hora?"*
2. **Prontuário e Histórico:**
   - *"Onde ficam salvas as anotações e evoluções de cada paciente? É fácil recuperar o histórico de atendimentos antigos durante uma consulta?"*
3. **Controle Financeiro:**
   - *"Como você controla quem já pagou, quem ficou pendente no final do dia e quanto você faturou no mês?"*
4. **Equipe e Sigilo:**
   - *"Você atende sozinha ou conta com recepcionista/secretária? Há preocupação com o sigilo das fichas médicas e a LGPD?"*

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

Aqui você compartilha a tela com o protótipo interativo aberto (`index.html`). Siga o fluxo natural do atendimento da clínica:

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

### Passo 2: O Agendamento em 3 Cliques e Proteção Anti-Conflito (RN01 / RN02)
- **O que mostrar:** Clique na aba **"Agenda Clínica"**.
- **Demonstração A (Agendamento rápido):**
  - Clique em um horário vago (ex.: 10:00).
  - Selecione o paciente, tipo de consulta e confirme.
  - Aponte que o agendamento foi concluído em segundos.
- **Demonstração B (Inteligência Anti-Conflito):**
  - Tente propositalmente marcar outro paciente no mesmo horário das 10:00.
  - Mostre o sistema bloqueando a ação e emitindo o alerta de choque de horário.
- **O que falar:**
  > *"Aqui está uma das maiores proteções do sistema: se você ou sua recepcionista tentar agendar dois pacientes no mesmo horário por engano, o sistema impede a gravação e alerta na hora. Isso elimina o constrangimento de pacientes esperando ao mesmo tempo na sala de espera."*

---

### Passo 3: Automação de Confirmações e Lembretes via WhatsApp (RN03 / RN04)
- **O que mostrar:** O ícone e status de envio de mensagem ao lado da consulta na agenda.
- **O que falar:**
  > *"Assim que a consulta é confirmada, o sistema gera a confirmação direta no WhatsApp do paciente. Além disso, ele agenda automaticamente o disparo de um lembrete 24 horas antes do horário marcado. Estudos na área de saúde mostram que essa comunicação reduz faltas em até 40%."*
- **Destaque extra (Cancelamento Tardio — RN10):** Mostre como o sistema detecta cancelamentos feitos a menos de 24 horas da consulta e marca na ficha do paciente para controle da taxa de desmarcação.

---

### Passo 4: O Momento do Atendimento — Prontuário com Histórico Imutável (RN17)
- **O que mostrar:** Navegue para a aba **"Prontuário Eletrônico"** (ou use a busca rápida `Ctrl + K`).
- **Demonstração A (Ficha e Linha do Tempo):**
  - Selecione a paciente Mariana Duarte.
  - Aponte os alertas destacados de alergias e condições pré-existentes e a linha do tempo cronológica.
- **Demonstração B (Edição com Versionamento Imutável — O Diferencial Ético/Jurídico):**
  - Clique para editar uma anotação já registrada.
  - Modifique o texto (ex.: acrescentando uma evolução) e informe o motivo da alteração.
  - Salve e abra o **Histórico de Versões** daquela sessão, mostrando o texto antigo preservado com data, hora e motivo.
- **O que falar:**
  > *"Este é um dos recursos mais avançados e importantes para o profissional de saúde: o respaldo ético e legal. Em sistemas comuns ou blocos de notas, se alguém edita uma anotação, o histórico antigo é sobrescrito. No Clinix, cada alteração gera uma versão arquivada definitiva com data e motivo. Isso oferece segurança total perante conselhos de classe e eventuais disputas judiciais."*

---

### Passo 5: Fechamento da Consulta e Controle Financeiro (RN07 / RN08 / RN09)
- **O que mostrar:** Aba **"Financeiro & Relatórios"**.
- **Demonstração:**
  - Dê baixa em uma consulta pendente selecionando a forma de pagamento (Pix, Cartão ou Dinheiro).
  - Mostre o resumo consolidado do mês atualizando em tempo real com total arrecadado, pendências e atendimentos realizados.
- **O que falar:**
  > *"Assim que o paciente sai da sala, com um clique você dá baixa informando se foi Pix, cartão ou dinheiro. Se ficar para acertar depois, o valor permanece visível na lista de pendências. No final do mês, você não precisa somar comprovantes ou bater planilhas: o sistema fecha o faturamento automaticamente."*

---

### Passo 6: Segurança, Controle de Perfis e LGPD (RN11, RN12, RN13, RN16)
- **O que mostrar:** Alterne o perfil de acesso no menu lateral (Profissional vs Administrador) e mostre a aba **"Auditoria & Logs"**.
- **O que falar:**
  > *"O sistema separa rigorosamente quem acessa o quê. Se você tiver uma recepcionista, ela pode agendar, emitir recibos e cadastrar contatos, mas ela **não tem permissão para ler prontuários médicos sigilosos**, que ficam restritos ao profissional. Além disso, cada acesso a prontuário fica registrado em uma trilha de auditoria inviolável, e o cadastro permite anonimização imediata para atender à LGPD."*

---

## 6. Bloco 4: Andamento do Projeto e Rigor Técnico (5 min)

Nesta etapa, você demonstra a maturidade e a seriedade da construção do software:

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

## 7. Bloco 5: Proposta Comercial e Fechamento (5 a 10 min)

Aqui você apresenta a proposta comercial alinhada com as **duas opções de contratação** (SaaS vs Não-SaaS):

### 🗣️ Script de Transição e Apresentação das Duas Opções:
> *"Dra. [Nome], agora que você viu como o sistema funciona na prática, quero te apresentar como nós estruturamos a contratação. Para que você tenha total conforto financeiro, nós trabalhamos com **dois formatos**:*
>
> ***Opção 1 — Assinatura Leve (Plano Parceiro Fundador):***  
> *Você não paga nada de taxa de desenvolvimento. Nós liberamos **30 dias de uso piloto gratuito** no seu consultório para você e sua equipe testarem na rotina real. Se você aprovar, você mantém uma mensalidade especial fixada de apenas **R$ 119,00 por mês** [ou R$ 97,00], com servidores em nuvem, backups diários de segurança, suporte direto comigo e todas as novas atualizações inclusas, sem taxa de cancelamento.*
>
> ***Opção 2 — Projeto Dedicado do Consultório (Sem Mensalidades):***  
> *Se você prefere não ter mensalidades fixas e quer que o sistema seja um investimento próprio do consultório, nós fazemos a entrega como um projeto fechado por **R$ 2.400,00**. Dividimos esse valor em etapas conforme as entregas: 40% de entrada, 30% na validação dos dados reais e 30% após o treinamento da equipe. Essa opção inclui a instalação exclusiva, importação dos seus pacientes e **90 dias de garantia técnica total**, sem mensalidades obrigatórias."*

### ❓ Pergunta de Fechamento:
> *"Dra. [Nome], considerando a rotina e o momento atual do seu consultório, **qual desses dois formatos faz mais sentido para você começar?**"*

### 🎯 Como Conduzir as Respostas:
* **Se escolher a Opção 1 (SaaS / Piloto):**  
  > *"Excelente! O piloto de 30 dias é a melhor forma de você sentir o ganho de tempo sem nenhum risco. Vou coletar seus horários e serviços para parametrizar o sistema e na próxima semana já liberamos o seu acesso para você começar a usar."*
* **Se escolher a Opção 2 (Projeto Fechado):**  
  > *"Perfeito! Vou emitir o nosso termo de projeto com o cronograma das três entregas e o detalhamento da garantia de 90 dias. Podemos programar a entrada para [data de preferência dela] para iniciarmos a configuração?"*
* **Se ela hesitar ("Preciso pensar..."):**  
  > *"Compreendo perfeitamente, Dra. [Nome]! Para você não precisar tomar nenhuma decisão financeira hoje, o que acha de aproveitarmos os 30 dias de piloto gratuito da Opção 1? Você usa o sistema no consultório neste mês e, no final dos 30 dias, você decide com calma se prefere continuar na assinatura ou converter para o projeto fechado. O que acha?"*

---

## 8. Guia de Respostas para Objeções de Preço e Técnicas

| Objeção da Cliente | Resposta Estruturada Recomendada |
| :--- | :--- |
| **No Modelo Fechado: "Achei R$ 2.400 pesado para pagar agora"** | *"Compreendo perfeitamente! É exatamente por isso que nós temos a Opção 1 de Assinatura. Você não desembolsa absolutamente nada agora, ganha 30 dias de piloto gratuito e depois mantém apenas R$ 119,00 por mês, que é menos que o valor de uma única consulta particular da sua clínica."* |
| **No Modelo SaaS: "Não gosto de pagar mensalidade pra sempre"** | *"Sem problemas! Nós podemos formalizar a entrega como Projeto Fechado por R$ 2.400,00 (divididos em 3 etapas de entrega), com 90 dias de garantia técnica inclusa e sem qualquer cobrança mensal obrigatória."* |
| **Em Qualquer Modelo: "Por que não é de graça se é projeto de faculdade?"** | *"O projeto acadêmico serviu para validar o rigor de engenharia com 23 testes automatizados. Para operar no seu consultório, o sistema consome servidores profissionais, rotinas diárias de backup e suporte técnico dedicado, garantindo que os dados dos seus pacientes estejam 100% seguros na LGPD."* |
| **"E se a internet cair durante o atendimento?"** | *"O sistema é leve e opera com cache local seguro no navegador. Em caso de oscilações breves, seus dados na tela permanecem preservados."* |
| **"Consigo acessar pelo celular ou tablet?"** | *"Sim! A interface é 100% responsiva. Você pode consultar a agenda ou prontuários tanto no computador do consultório quanto no celular."* |
| **"E se der algum problema ou bug no sistema?"** | *"No modelo de projeto fechado, você conta com 90 dias de garantia técnica com correção prioritária sem custo. No modelo de assinatura, essa garantia e suporte são permanentes enquanto o plano estiver ativo."* |

---

## 9. Anexo: Resumo Rápido para a Reunião (Guia de Bolso)

Mantenha esta tabela visível em uma segunda tela durante a apresentação:

```
┌───────┬───────────────────────────────────┬───────────────────────────────────────────────┐
│ MIN   │ ETAPA DA REUNIÃO                  │ AÇÃO NA TELA / MENSAGEM-CHAVE                 │
├───────┼───────────────────────────────────┼───────────────────────────────────────────────┤
│ 00-05 │ Diagnóstico e Perguntas           │ Ouvir: "Como agenda?", "Faltas?", "Prontuário?"│
│ 05-08 │ Proposta de Valor                 │ Destacar: 3 cliques, WhatsApp, Prontuário, Fin│
│ 08-12 │ Agenda & Anti-Conflito            │ Agendar em 3 cliques -> Forçar marcação dupla │
│ 12-16 │ Prontuário & Versionamento        │ Abrir paciente -> Editar anotação -> Histórico│
│ 16-20 │ Financeiro & Relatórios           │ Baixar Pix/Cartão -> Resumo mensal em 1 clique│
│ 20-24 │ Perfis, LGPD e Auditoria          │ Alternar Profissional/Admin -> Trilha de Logs │
│ 24-28 │ Andamento & Engenharia C#         │ Citar os 23 testes TDD xUnit e maturidade     │
│ 28-35 │ Apresentação das Duas Portas      │ Oferecer Opção 1 (R$ 119/mês) vs Opção 2      │
│       │ & Fechamento Comercial            │ (Projeto R$ 2.400) -> Conduzir p/ Piloto 30d  │
└───────┴───────────────────────────────────┴───────────────────────────────────────────────┘
```
