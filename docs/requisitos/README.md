# 📋 Especificação Formal de Requisitos — Clinix

> **Projeto Integrador Multidisciplinar — Engenharia de Software (CEUB)**  
> **Disciplina:** Engenharia de Requisitos  
> **Responsável pelo Levantamento:** Lucas | **Dev Principal:** Davi Felinto  
> **Arquivo Original:** [`Documento_Especificacao_Requisitos_Final.docx`](Documento_Especificacao_Requisitos_Final.docx)

---

## 🎯 Escopo do Sistema

O **Clinix** é uma plataforma integrada de gestão para clínicas de pequeno porte e profissionais liberais da saúde (médicos, psicólogos, fisioterapeutas, nutricionistas). O foco é solucionar o problema crítico de **conflito de agendamentos**, **perda de registros clínicos** e adequação rigorosa à **Lei Geral de Proteção de Dados (LGPD - Lei 13.709/2018)**.

---

## 📌 Requisitos Funcionais (RF01 – RF34)

### Módulo 1: Gestão de Pacientes & LGPD
- **RF01:** Cadastrar novos pacientes com dados cadastrais obrigatórios (Nome, CPF/RG, data de nascimento, contato e endereço).
- **RF02:** Listar todos os pacientes ativos e inativos no sistema com paginação e busca por nome ou documento.
- **RF03:** Atualizar informações cadastrais e de contato de pacientes existentes.
- **RF04:** Visualizar ficha cadastral completa com alertas destacados de alergias e condições preexistentes.
- **RF05:** Buscar pacientes por CPF, RG ou nome parcial com resposta imediata.

### Módulo 2: Agendamento & Gestão de Consultas
- **RF06:** Agendar novas consultas associando paciente, profissional de saúde, data e intervalo de horário.
- **RF07:** Visualizar histórico cronológico de consultas agendadas, realizadas ou canceladas por paciente.
- **RF08:** Confirmar a realização de uma consulta agendada pelo médico ou recepção.
- **RF09:** Remarcar consultas para novas datas/horários com verificação prévia de disponibilidade.
- **RF10:** Cancelar consultas registrando o motivo e momento exato do cancelamento.
- **RF11:** Bloquear horários e períodos na agenda do profissional (reuniões, férias, feriados).

### Módulo 3: Notificações & Lembretes
- **RF12:** Enviar confirmação imediata de agendamento via WhatsApp / canal digital cadastrado.
- **RF13:** Disparar lembrete automático 24 horas antes do horário previsto da consulta.
- **RF14:** Registrar log de tentativas de envio de notificações e reenvio em caso de falha.

### Módulo 4: Prontuário Eletrônico do Paciente (PEP)
- **RF15:** Registrar anotações clínicas, hipóteses diagnósticas e condutas em sessão de prontuário vinculada ao agendamento.
- **RF16:** Atualizar anotações de prontuário mantendo versionamento imutável e justificativa obrigatória de alteração.
- **RF17:** Consultar a linha do tempo completa do histórico médico do paciente em ordem cronológica.
- **RF18:** Exportar relatório médico ou cópia de prontuário em formato padronizado com registro de auditoria.

### Módulo 5: Gestão Financeira & Cobrança
- **RF19:** Gerar registro de cobrança financeira vinculado automaticamente a cada consulta agendada.
- **RF20:** Quitar cobrança pendente informando a forma de pagamento (Dinheiro, Cartão, PIX, Convênio).
- **RF21:** Listar contas a receber com filtros por status (Pendente, Pago, Cancelado) e período.
- **RF22:** Emitir demonstrativo de fechamento financeiro mensal com totalizadores e conciliação.

### Módulo 6: Autenticação, Perfis & Auditoria (LGPD)
- **RF26:** Autenticar usuários no sistema com controle de sessão e credenciais seguras.
- **RF27:** Controlar níveis de permissão baseados em perfil (RBAC: Profissional de Saúde vs. Administrador/Recepção).
- **RF28:** Registrar log imutável de todas as operações sensíveis realizadas sobre prontuários e dados cadastrais.

### Módulo 7: Demandas Clínicas & Inteligência de Gestão (Feedback Real)
- **RF29 (CEP e Endereço Estruturado):** Buscar e preencher automaticamente logradouro, bairro, cidade e UF a partir do CEP via integração com ViaCEP / proxy backend, mantendo campos atômicos para contratos e relatórios.
- **RF30 (Pagamento Misto / Dividido):** Permitir dividir a quitação de consultas em duas etapas e modalidades distintas (ex.: Entrada no Cartão de Débito e Saldo no PIX ou Boleto), com conciliação automática.
- **RF31 (Motivos de Cancelamento & Métricas):** Registrar obrigatoriamente a categoria do cancelamento (Desistência, Conflito, Financeiro, Saúde, Satisfação, Outro) para cálculo da taxa de atrito (churn) e melhoria contínua.
- **RF32 (Ouvidoria & Avaliação do Atendimento):** Coletar avaliações dos pacientes com notas de 1 a 5 para Recepção, Atendimento Clínico e Instalações, calculando média global e permitindo depoimentos anônimos.
- **RF33 (Recorrência Preventiva 180+ Dias):** Identificar automaticamente pacientes sem consultas nos últimos 6 meses e disponibilizar ação em 1 clique para envio de convite via WhatsApp com cupom de 10% de desconto (`CHECKUP10`).
- **RF34 (Rastreamento de Amostras Laboratoriais):** Acompanhar o ciclo de vida de exames externos (Coletado, Enviado, Em Análise, Laudo Disponível, Entregue) com alerta visual pulsante na Agenda do médico quando houver laudo pendente.

---

## ⚖️ Regras de Negócio (RN01 – RN23)

| Código | Nome da Regra | Descrição & Comportamento Esperado |
| :--- | :--- | :--- |
| **RN01** | Conflito de Agenda | Uma consulta não pode ser agendada ou remarcada se houver sobreposição de horário para o mesmo profissional. |
| **RN02** | Intervalo Válido | A data/hora final de uma consulta deve ser estritamente posterior à data/hora inicial. |
| **RN03** | Confirmação Imediata | Ao cadastrar uma consulta, o sistema deve acionar o disparador de notificação instantânea. |
| **RN04** | Lembrete de 24 Horas | O sistema deve agendar o lembrete preventivo 24 horas antes do início da consulta. |
| **RN05** | Sigilo do Prontuário | Somente profissionais de saúde autenticados podem visualizar e editar anotações clínicas. |
| **RN06** | Vínculo Clínico | Toda anotação de prontuário deve estar vinculada a um paciente existente. |
| **RN07** | Cobrança Única | Não pode haver mais de uma cobrança gerada para um mesmo agendamento (exceto desdobramento por pagamento dividido). |
| **RN08** | Quitação com Forma | Para marcar uma cobrança como Paga, a forma de pagamento e a data devem ser preenchidas. |
| **RN09** | Conciliação Mensal | O fechamento financeiro mensal calcula estritamente as cobranças geradas ou pagas dentro daquele mês. |
| **RN10** | Cancelamento Tardio | Cancelamentos efetuados com menos de 24 horas de antecedência da consulta devem ser marcados como tardios no histórico. |
| **RN11** | Unicidade de Documento | Não é permitido cadastrar dois pacientes ativos com o mesmo número de CPF ou RG. |
| **RN12** | Auditoria Restrita | Logs de acesso ao prontuário só podem ser auditados por usuários com permissão de administrador. |
| **RN13** | Trilha de Auditoria LGPD | Qualquer consulta ou alteração de prontuário gera imediatamente um registro no log com IP, data e autor. |
| **RN14** | Bloqueio Prioritário | Nenhum agendamento pode ser marcado em períodos cobertos por um bloqueio ativo de agenda. |
| **RN15** | Preservação de Alergias | Alertas de alergias e condições preexistentes não podem ser apagados na anonimização cadastral. |
| **RN16** | Inativação Lógica LGPD | A exclusão de paciente deve ser lógica (`ativo = 0`), anonimizando telefone, e-mail e endereço. |
| **RN17** | Imutabilidade Clínica | Anotações anteriores do prontuário nunca são sobrescritas; uma nova versão é gravada com o motivo da edição. |
| **RN18** | Consistência do Endereço | O CEP deve conter 8 dígitos válidos e os campos preenchidos devem corresponder à localização da clínica/paciente. |
| **RN19** | Conciliação de Pagamento Dividido | A soma das parcelas (Entrada + Saldo) em pagamento dividido deve igualar o valor total estipulado para a consulta. |
| **RN20** | Rastreabilidade do Cancelamento | Todo cancelamento deve registrar uma das categorias predefinidas e justificativa detalhada para fins de governança. |
| **RN21** | Integridade da Avaliação | As notas da ouvidoria devem situar-se no intervalo fechado de 1 a 5 e permitir opção de anonimato para proteção do paciente. |
| **RN22** | Janela de Retorno Preventivo | Pacientes com última consulta há mais de 180 dias são elegíveis à campanha de fidelização com cupom semestral. |
| **RN23** | Alerta Clínico de Amostra Pendente | Consultas com exames em processamento externo exibem alerta de alta prioridade na Agenda até a entrega do laudo. |

---

## 🛡️ Requisitos de Qualidade & Não Funcionais (RQ01 – RQ16)

Classificados conforme a norma internacional **ISO/IEC 25010** e diretrizes da **LGPD**:

- **RQ01 (Desempenho):** O tempo de resposta para operações comuns (busca de horários e pacientes) deve ser inferior a 1,5 segundos.
- **RQ02 (Confiabilidade):** O sistema deve operar com tolerância a falhas na camada externa de mensagens sem interromper o fluxo clínico.
- **RQ03 (LGPD / Privacidade):** Anonimização de dados pessoais sensíveis garantida em conformidade com o Artigo 18 da Lei 13.709/2018.
- **RQ04 (Compatibilidade):** Suporte completo aos navegadores modernos (Chrome, Firefox, Safari, Edge) em desktops, tablets e smartphones.
- **RQ05 (Portabilidade):** Backend em .NET 8 conteinerizado com suporte nativo a execução multiplataforma (Linux, Windows, macOS).
- **RQ06 (Segurança - RBAC):** Controle de acesso baseado em papéis (Role-Based Access Control) impedindo escalada de privilégios.
- **RQ07 (Segurança - Não Repúdio):** Trilha de auditoria em tabela somente-leitura (append-only) com timestamps UTC precisos.
- **RQ08 (Criptografia):** Senhas armazenadas com algoritmo PBKDF2/SHA-256 e Salt criptográfico de alta entropia.
- **RQ09 (Segurança Relacional):** Todas as consultas em banco de dados devem utilizar queries estritamente parametrizadas, prevenindo SQL Injection.
- **RQ10 (Usabilidade):** Fluxo de agendamento e atendimento realizável em até 3 cliques a partir do dashboard principal.
- **RQ11 (Tratamento de Exceções):** Mensagens de erro de validação devem ser claras, orientativas e sem exposição de stack traces ao usuário final.
- **RQ12 (Manutenibilidade):** Arquitetura orientada a serviços desacoplados e aderência ao Repository Pattern.
- **RQ13 (Testabilidade):** Cobertura de 100% das regras de negócio através de testes unitários automatizados com xUnit.
- **RQ14 (Escalabilidade Relacional):** Esquema relacional modelado em 3ª Forma Normal (3FN) com índices compostos para busca eficiente.
- **RQ15 (Disponibilidade):** O frontend deve possuir fallback gracioso para funcionamento local caso a API esteja temporariamente indisponível.
- **RQ16 (Auditoria Fiscal):** Preservação do histórico financeiro por prazo mínimo de 5 anos para cumprimento de obrigações contábeis.

