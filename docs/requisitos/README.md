# 📋 Especificação Formal de Requisitos — Clinix

> **Projeto Integrador Multidisciplinar — Engenharia de Software (CEUB)**  
> **Disciplina:** Engenharia de Requisitos  
> **Responsável pelo Levantamento:** Lucas | **Dev Principal:** Davi Felinto  
> **Arquivo Original:** [`Documento_Especificacao_Requisitos_Final.docx`](Documento_Especificacao_Requisitos_Final.docx)

---

## 🎯 Escopo do Sistema

O **Clinix** é uma plataforma integrada de gestão para clínicas de pequeno porte e profissionais liberais da saúde (médicos, psicólogos, fisioterapeutas, nutricionistas). O foco é solucionar o problema crítico de **conflito de agendamentos**, **perda de registros clínicos** e adequação rigorosa à **Lei Geral de Proteção de Dados (LGPD - Lei 13.709/2018)**.

---

## 📌 Requisitos Funcionais (RF01 – RF28)

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

---

## ⚖️ Regras de Negócio (RN01 – RN17)

| Código | Nome da Regra | Descrição & Comportamento Esperado |
| :--- | :--- | :--- |
| **RN01** | Conflito de Agenda | Uma consulta não pode ser agendada ou remarcada se houver sobreposição de horário para o mesmo profissional. |
| **RN02** | Intervalo Válido | A data/hora final de uma consulta deve ser estritamente posterior à data/hora inicial. |
| **RN03** | Confirmação Imediata | Ao cadastrar uma consulta, o sistema deve acionar o disparador de notificação instantânea. |
| **RN04** | Lembrete de 24 Horas | O sistema deve agendar o lembrete preventivo 24 horas antes do início da consulta. |
| **RN05** | Sigilo do Prontuário | Somente profissionais de saúde autenticados podem visualizar e editar anotações clínicas. |
| **RN06** | Vínculo Clínico | Toda anotação de prontuário deve estar vinculada a um paciente existente. |
| **RN07** | Cobrança Única | Não pode haver mais de uma cobrança gerada para um mesmo agendamento. |
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
