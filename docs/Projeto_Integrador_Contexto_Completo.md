# Projeto Integrador — Eng. de Requisitos
## Contexto completo, memórias e especificação

> Sistema de Agendamento e Prontuário para Clínicas Pequenas
> Arquivo consolidado em 01/10/2026. Reúne o que está registrado na memória do projeto e o conteúdo do Documento de Especificação de Requisitos (versão final).

---

## 1. Visão geral

- **O que é:** projeto acadêmico integrador de um sistema de agendamento e prontuário eletrônico simplificado para clínicas pequenas e profissionais autônomos de saúde (ex.: psicólogos, nutricionistas, fisioterapeutas).
- **Origem:** disciplina de Engenharia / Análise de Requisitos. Foi estendido formalmente, por acordo com o professor, para abranger também:
  - POO (Programação Orientada a Objetos, em C#)
  - Banco de Dados II
  - Desenvolvimento de Interface
- **Definição de sucesso:** produzir artefatos estruturados e validados em cada fase antes de passar para a próxima.
- **Escopo exigido pelo professor:** cobrir todas as etapas da Engenharia de Requisitos, do entendimento/mapeamento do negócio até a construção de um protótipo ou MVP.

## 2. Pessoas e papéis

- **Davi** — estudante de Engenharia de Software no CEUB (2º semestre), responsável pelo projeto. Trabalha na OROS Soluções Educacionais. Vem de background em Python e está aprendendo C# para a fase de POO.
- **Professor** — busca um cliente real para o projeto; o grupo apresenta a ideia e depois levanta os requisitos com esse cliente.
- **Professor do exemplo de formato** — Antonio Sérgio Haddad Alves (disciplina Análise de Requisitos). O exemplo em sala foi um documento de requisitos de um Sistema de Monitoramento de UTI, usado como modelo.
- **Cliente (simulado)** — o grupo não tem cliente real; a entrevista foi simulada com um amigo que trabalha em uma clínica odontológica que já possui sistema próprio. O objetivo da entrevista foi **validar hipóteses** já levantadas, não descobrir requisitos do zero.

## 3. Estado atual do projeto

- A **fase de requisitos** é o foco ativo. As fases de banco de dados e de interface estão deliberadamente adiadas até a conclusão dos requisitos.
- **Validação com o cliente: concluída.**
- **Divisão em sprints (Scrum): deixada de lado por enquanto.** (Antes disso, o professor pediu condução estilo Scrum e foram escolhidas sprints de 1 semana.)
- **Aprendizado de C# / POO** em andamento em paralelo.

### Artefatos já produzidos

1. **Guia de requisitos** — passo a passo do processo, preparação de entrevista e boas práticas.
2. **Documento de visão inicial** (documento de hipóteses) — problema, tabela de stakeholders, requisitos do MVP por módulo, requisitos não funcionais, premissas a validar, perguntas em aberto para o professor e próximos passos.
3. **Documento de Especificação de Requisitos** (formal, no formato do exemplo do professor) — mapeamento BPMN com raias, funcionalidades, RFs, RDs, RNs e RQs conforme ISO/IEC 25010. Versão final detalhada na seção 7.
4. **Resumo de Validação de Requisitos** (revisão de pares/autoavaliação) — apontou 10 pontos de esclarecimento, todos resolvidos (seção 5).
5. **Material de estudo "De Python para C#"** — comparação de sintaxe e conceitos de POO ligados às entidades do projeto.

## 4. Feedback do professor sobre o documento de visão

- Confirmou que o escopo está totalmente alinhado ao esperado.
- **Orientação para a entrevista com o cliente:** primeiro entender como a clínica funciona hoje, principais problemas/dificuldades, necessidades e prioridades, **antes** de propor qualquer solução. O grupo deve ouvir primeiro, não vender solução.
- **Perguntas sugeridas:**
  - Como a clínica funciona hoje?
  - Quantos pacientes e profissionais?
  - Quais as maiores dificuldades/problemas?
  - Como é feito o agendamento (processo)?
  - Existe algum controle de pacientes e consultas?
- **Fluxo esperado:** entender o negócio como funciona hoje → identificar problemas/dores/necessidades/prioridades → propor solução → validar os requisitos identificados com o cliente.

### Processo planejado pelo grupo

1. Entender o negócio como um todo e mapear o processo atual / cliente.
2. Mapear a solução proposta.
3. Levantar os requisitos da solução.
4. Verificar se o cliente concorda com esses requisitos.

## 5. Decisões tomadas na validação (10 pontos resolvidos)

| # | Ponto | Decisão |
|---|-------|---------|
| 1 | Exclusão de paciente | Exclusão lógica (inativação), preservando prontuário e histórico |
| 2 | Canal de notificação | WhatsApp |
| 3 | Perfis de usuário | Profissional e Administrador (resolveu a inconsistência RF27/RN12) |
| 4 | Múltiplos profissionais | Fora do MVP, vai para o escopo futuro |
| 5 | Falha de notificação | Uma nova tentativa automática após 15 min; se falhar de novo, alerta visual na agenda |
| 6 | Relatórios | Filtráveis por período, status de pagamento e paciente |
| 7 | Edição de prontuário | Mantém histórico de versões |
| 8 | Backup | Saiu do MVP, vai para o escopo futuro |
| 9 | Horário comercial (disponibilidade de 99%) | Seg–sex 07h–19h e sáb 08h–12h |
| 10 | LGPD | Decomposta em 5 critérios objetivos: consentimento/base legal, portabilidade, anonimização na exclusão lógica, criptografia em trânsito e em repouso, limitação de finalidade |

Após essas decisões, RF/RD/RN/RQ foram renumerados em sequência: **RF01–RF28, RN01–RN17, RQ01–RQ16**, com 7 funcionalidades (F1–F7).

## 6. Como o Davi conduz o projeto

### Abordagem
- Trabalha **fase por fase** e adia assuntos deliberadamente: não trazer banco de dados ou UI durante a fase de requisitos, a menos que ele peça.
- Iterativo e pragmático: um artefato/fase por vez, com checkpoints explícitos antes de avançar.
- **Artefatos (documentos Word e entregáveis estruturados)** são a saída principal de cada etapa; a geração programática desses documentos é a abordagem estabelecida.
- O material de estudo de POO foi baseado em entidades do próprio domínio do projeto (não exemplos genéricos); manter essa abordagem.
- O C# fica em **aplicação console** (sem Web API).

### Comunicação
- Português brasileiro, informal e iterativo.
- Prefere direção concisa e entregas tangíveis a explicações exaustivas.
- Prefere que o raciocínio seja explicado **antes** do código.
- Preferência forte por entender o porquê das decisões técnicas, não só receber a solução.
- Preferência por implementações simples e pythônicas (quando em Python).

### Ferramentas e recursos
- Aplicações console em C# (ambiente de desenvolvimento não especificado).
- Biblioteca Node.js `docx` para gerar documentos Word; documentos entregues como arquivos Word nos marcos importantes.
- Material de estudo: "De Python para C#".

## 7. Aprendizado de C# / POO

- Está aprendendo C# (vindo de Python) especificamente para implementar a fase de POO.
- Entidades de domínio usadas: `Paciente`, `Agendamento`, `ProfissionalSaude`.
- Já estudou as diferenças fundamentais Python → C#: tipagem, sintaxe, modificadores de acesso, interfaces e polimorfismo.
- Concluiu **10 exercícios de lógica em aplicação console**, com explicações.

### No horizonte
- Implementação em POO com entidades e interfaces do projeto (ex.: `INotificavel`, `IRepositorioPaciente`).
- Integração com Banco de Dados II e Desenvolvimento de Interface: escopo definido, ainda não ativos.
- **Decisão em aberto:** consolidar os exercícios de C# em um único projeto com menu, ou revisá-los um a um.

## 8. Contexto acadêmico e profissional relacionado

- Estudante do 2º semestre de Engenharia de Software no CEUB (Centro Universitário de Brasília).
- Disciplinas: POO (C#), bancos de dados, engenharia de requisitos, desenvolvimento de interface e álgebra linear.
- Estudou Clean Code e o Método iRON (metodologia brasileira de engenharia de requisitos) para disciplinas e carreira.
- Trabalha na OROS Soluções Educacionais (edtech que atende escolas públicas do Piauí), com gestão de conteúdo da plataforma e relatórios semanais.
- Portfólio no GitHub: github.com/Davi-Felinto

---

# 9. Documento de Especificação de Requisitos (versão final)

## 9.1 Necessidades do cliente

O cliente deseja um sistema para organizar a agenda e o prontuário do consultório, hoje controlado de forma manual, por planilha e WhatsApp.

- **Pacientes** cadastrados com nome, documento de identificação, data de nascimento, telefone, e-mail, endereço e informações clínicas relevantes (alergias e condições preexistentes).
- **Agendamento** feito direto no sistema, que deve impedir dois pacientes no mesmo horário. Ao agendar, o sistema envia confirmação automática ao paciente e, um dia antes, um lembrete, para reduzir faltas.
- **Atendimento:** registrar anotações da sessão vinculadas ao paciente e consultar rapidamente o histórico de atendimentos anteriores.
- **Financeiro:** ao final de cada consulta, registrar se o pagamento foi feito ou está pendente; no fim do mês, ver resumo com total de atendimentos e faturamento.
- **Segurança e LGPD:** acesso protegido por login e senha, restrito a profissionais autorizados, em conformidade com a Lei Geral de Proteção de Dados. Se houver perfil administrativo, cada perfil acessa só o necessário.
- **Usabilidade:** funcionar em computador e celular, com interface simples, pois nem sempre há tempo ou familiaridade técnica entre um atendimento e outro.

## 9.2 Processo "TO-BE" (BPMN)

Três raias: **Paciente, Profissional e Sistema**. O fluxo começa com a solicitação de agendamento pelo paciente, passa por cadastro e verificação de disponibilidade, confirmação e lembrete automático, atendimento e registro em prontuário, e termina com o registro do pagamento e a geração do resumo financeiro do período.

## 9.3 Funcionalidades (F)

| Código | Funcionalidade | Tipo |
|--------|----------------|------|
| F1 | Cadastro de Pacientes | Entrada |
| F2 | Agendamento de Consultas | Entrada/Processamento |
| F3 | Notificação de Consultas | Processamento/Saída |
| F4 | Prontuário Eletrônico | Processamento |
| F5 | Controle Financeiro | Processamento |
| F6 | Histórico e Relatórios | Saída |
| F7 | Autenticação e Controle de Acesso | Entrada |

## 9.4 Requisitos Funcionais (RF)

**F1 – Cadastro de Pacientes**
- RF01 – incluir dados do paciente
- RF02 – alterar dados do paciente
- RF03 – inativar o paciente (exclusão lógica), preservando prontuário e histórico
- RF04 – consultar dados do paciente
- RF05 – buscar paciente por nome ou documento de identificação

**F2 – Agendamento de Consultas**
- RF06 – agendar consulta para um paciente
- RF07 – remarcar consulta
- RF08 – cancelar consulta
- RF09 – verificar conflito de horário antes de confirmar o agendamento
- RF10 – consultar a agenda por dia ou por semana
- RF11 – bloquear horários na agenda (intervalos, folgas, indisponibilidades)

**F3 – Notificação de Consultas**
- RF12 – gerar confirmação automática do agendamento ao paciente via WhatsApp
- RF13 – gerar lembrete automático via WhatsApp, 24 horas antes do horário agendado
- RF14 – registrar o status de envio de cada notificação (enviado, falha, pendente) e reenviar automaticamente uma vez em caso de falha

**F4 – Prontuário Eletrônico**
- RF15 – registrar anotação de sessão vinculada ao paciente
- RF16 – alterar anotação de sessão, mantendo o histórico de versões anteriores
- RF17 – consultar o histórico de sessões de um paciente
- RF18 – registrar informações clínicas gerais do paciente (alergias, condições preexistentes)

**F5 – Controle Financeiro**
- RF19 – registrar o valor da consulta
- RF20 – registrar o status de pagamento (pago/pendente)
- RF21 – consultar pendências financeiras por paciente
- RF22 – gerar resumo financeiro mensal (número de atendimentos e faturamento)

**F6 – Histórico e Relatórios**
- RF23 – exibir o histórico de atendimentos do paciente
- RF24 – exibir relatório consolidado de consultas, filtrável por período, status de pagamento e paciente
- RF25 – exportar relatórios em PDF ou planilha

**F7 – Autenticação e Controle de Acesso**
- RF26 – autenticar o usuário por login e senha
- RF27 – controlar o acesso conforme o perfil (Profissional ou Administrador)
- RF28 – registrar log de acessos e operações por usuário

## 9.5 Requisitos de Dados (RD)

- **RD01 (RF01–RF05):** paciente com nome, documento de identificação, data de nascimento, telefone, e-mail, endereço, alergias, condições preexistentes e status do cadastro (ativo/inativo).
- **RD02 (RF06–RF11):** agendamento com identificador do paciente, identificador do profissional, data, horário, status (confirmado, remarcado, cancelado) e observações.
- **RD03 (RF12–RF14):** notificação com identificador do agendamento, tipo (confirmação ou lembrete), canal (WhatsApp), data/hora de envio e status (pendente, enviado, falha, reenviado). "Reenviado" é atribuído quando a nova tentativa (RN14) é bem-sucedida; "falha" é mantido se a nova tentativa também falhar (RN15).
- **RD04 (RF15–RF18):** sessão com identificador do paciente, identificador do agendamento, data, anotações do profissional, informações clínicas relevantes e histórico de versões da anotação.
- **RD05 (RF19–RF22):** pagamento com identificador da consulta, valor, status, data de pagamento e forma de pagamento.
- **RD06 (RF23–RF25):** relatório com período de referência, status de pagamento, identificador do paciente, lista de atendimentos, lista de pagamentos e formato de exportação.
- **RD07 (RF26–RF28):** usuário com identificador, nome, perfil de acesso (Profissional ou Administrador), senha protegida por hash e log de acessos (data, hora e operação).

## 9.6 Regras de Negócio (RN)

- **RN01** – Ao receber uma solicitação de agendamento, verificar conflito de horário antes de confirmar. (RF06, RF09)
- **RN02** – Havendo conflito, não permitir a confirmação e solicitar outro horário. (RF09)
- **RN03** – Ao confirmar um agendamento, gerar automaticamente a confirmação ao paciente. (RF06, RF12)
- **RN04** – Ao confirmar um agendamento, programar o lembrete automático para 24 horas antes da consulta. (RF06, RF13)
- **RN05** – Quando uma consulta for realizada, o profissional deve registrar a sessão antes de encerrar o atendimento no sistema. (RF15)
- **RN06** – Ao registrar uma sessão, atualizar o histórico do paciente. (RF15, RF17, RF23)
- **RN07** – Quando uma consulta for concluída, permitir o registro do status de pagamento. (RF20)
- **RN08** – Pagamento pendente permanece na consulta de pendências até ser atualizado para pago. (RF20, RF21)
- **RN09** – Ao final de cada mês, consolidar atendimentos e pagamentos do período no resumo financeiro. (RF22)
- **RN10** – Cancelamento com menos de 24 horas de antecedência deve ser sinalizado como cancelamento tardio no histórico do paciente. (RF08)
- **RN11** – Acesso a dados de prontuário e informações do paciente somente para usuários autenticados. (RF26)
- **RN12** – Profissional vê apenas pacientes e agendamentos vinculados a ele; Administrador tem acesso completo. (RF27)
- **RN13** – Todo acesso a dados de prontuário gera registro no log de acessos. (RF28)
- **RN14** – Falha no envio de notificação: reenviar automaticamente uma única vez, 15 minutos após a falha. (RF14)
- **RN15** – Se a nova tentativa também falhar, exibir alerta visual na agenda do profissional responsável pelo paciente. (RF14)
- **RN16** – Ao inativar um paciente (exclusão lógica), preservar integralmente prontuário e histórico de atendimentos. (RF03)
- **RN17** – Ao alterar uma anotação de prontuário, manter a versão anterior no histórico de alterações. (RF16)

## 9.7 Requisitos de Qualidade (RQ) — ISO/IEC 25010

Quatro das oito características foram especificadas, por serem as mais relevantes para um sistema com dados sensíveis de saúde operado por profissionais autônomos sem suporte técnico dedicado: **Segurança, Usabilidade, Confiabilidade e Adequação Funcional**.

**Segurança**
- RQ01 – Registrar a base legal aplicável ao tratamento dos dados pessoais e de saúde do paciente, quando necessário.
- RQ02 – Permitir exportar dados cadastrais e registros do paciente em formato estruturado, mediante solicitação.
- RQ03 – Ao inativar um paciente, anonimizar dados pessoais identificadores não essenciais (telefone, e-mail), preservando prontuário e histórico (RF03, RN16).
- RQ04 – Dados pessoais e de saúde criptografados em trânsito e em repouso.
- RQ05 – Usar os dados dos pacientes exclusivamente para a gestão da clínica, sem compartilhamento com terceiros não autorizados.
- RQ06 – Acesso restrito por usuário e senha, com dois perfis: Profissional (próprios pacientes e agenda) e Administrador (acesso completo).
- RQ07 – Todo acesso a dados de prontuário registrado em log com identificador do usuário, data e hora.
- RQ08 – Senhas armazenadas com função de hash com salt, nunca em texto legível.

**Usabilidade**
- RQ09 – Interface simples e de fácil aprendizado para profissionais de saúde sem familiaridade técnica avançada.
- RQ10 – Principais ações (agendar, registrar sessão, consultar histórico) em no máximo 3 cliques a partir da tela inicial.
- RQ11 – Mensagens de erro e confirmação em linguagem clara e não técnica.

**Confiabilidade**
- RQ12 – Nenhum agendamento pode ser perdido ou sobrescrito em caso de falha durante a confirmação.
- RQ13 – Disponibilidade mínima de 99% em horário comercial (seg–sex 07h–19h; sáb 08h–12h).

**Adequação Funcional**
- RQ14 – As funcionalidades devem corresponder integralmente aos requisitos funcionais especificados.
- RQ15 – Lembretes automáticos com taxa de sucesso mínima de 95%, considerando falhas de canal.
- RQ16 – O resumo financeiro mensal deve refletir com exatidão os valores e status de pagamento do período.

## 9.8 Escopo futuro (pós-MVP)

- Portal/aplicativo para o paciente marcar, remarcar ou cancelar consultas.
- Confirmação de presença pelo paciente diretamente no lembrete.
- Emissão automática de recibo ou nota fiscal.
- Integração com pagamento online (Pix, cartão).
- Anexação de arquivos ao prontuário (exames, laudos, imagens).
- Múltiplos profissionais na mesma clínica, com agendas independentes.
- Política formal de backup diário, com retenção definida e procedimento de restauração documentado.
- Integração com calendários externos (Google Calendar, Outlook).
- Módulo de telemedicina com chamada de vídeo integrada.
