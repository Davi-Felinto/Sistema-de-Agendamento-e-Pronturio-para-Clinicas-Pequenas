# CONTRATO DE LICENCIAMENTO DE SOFTWARE, HOSPEDAGEM E PRESTAÇÃO DE SERVIÇOS TÉCNICOS

---

## INSTRUMENTO PARTICULAR DE CONTRATO DE LICENCIAMENTO DE SOFTWARE, HOSPEDAGEM EM NUVEM E PRESTAÇÃO DE SERVIÇOS TÉCNICOS DE GESTÃO CLÍNICA

Pelo presente instrumento particular, de um lado:

**CONTRATADO:**  
**DAVI FELINTO DOS SANTOS**, brasileiro, solteiro, desenvolvedor e engenheiro de software, inscrito no CPF sob o nº `[000.000.000-00]`, residente e domiciliado em Brasília, Distrito Federal, com endereço eletrônico de contato: `[davifd0978@gmail.com]`, doravante denominado simplesmente **"CONTRATADO"** ou **"DESENVOLVEDOR"**;

E, de outro lado:

**CONTRATANTE:**  
**[RAZÃO SOCIAL DA CLÍNICA OU NOME DO PROFISSIONAL DE SAÚDE]**, pessoa jurídica de direito privado / pessoa física, inscrita no CNPJ/CPF sob o nº `[00.000.000/0001-00]`, com sede/consultório profissional situado em `[LOGRADOURO, NÚMERO, COMPLEMENTO, BAIRRO, CIDADE - UF, CEP]`, endereço de e-mail institucional `[email@clinica.com.br]` e telefone de contato `[(61) 90000-0000]`, neste ato representada por seu responsável legal, **[NOME DO REPRESENTANTE / RESPONSÁVEL TÉCNICO]**, portador(a) do documento de identidade RG nº `[0000000]` e inscrito(a) no CPF sob o nº `[000.000.000-00]`, devidamente registrado(a) no Conselho Profissional `[CRM/CRO/CRP nº 00000-DF]`, doravante denominada simplesmente **"CONTRATANTE"**;

Ambas as partes, de comum acordo, têm entre si justo e contratado o que se regerá pelas cláusulas e condições seguintes, em consonância com o Código Civil Brasileiro (Lei nº 10.406/2002), a Lei de Software (Lei nº 9.609/1998), a Lei de Direitos Autorais (Lei nº 9.610/1998), a Lei Geral de Proteção de Dados Pessoais - LGPD (Lei nº 13.709/2018) e as resoluções ético-profissionais aplicáveis ao setor de saúde.

---

### CLÁUSULA PRIMEIRA — DO OBJETO DO CONTRATO

**1.1.** O presente contrato tem por objeto o **licenciamento do direito de uso** da plataforma computacional denominada **CLINIX** (Sistema de Agendamento, Prontuário Eletrônico e Gestão para Clínicas e Consultórios), bem como a **prestação de serviços técnicos de implantação, parametrização, migração inicial de dados, treinamento operacional e suporte técnico**, de acordo com a modalidade comercial formalmente assinalada pelas partes no **ANEXO I** deste instrumento.

**1.2.** A plataforma computacional licenciada contempla as seguintes funcionalidades e módulos integrados:
* **Módulo de Agenda Inteligente:** Controle de consultas, bloqueios de horário de profissionais, verificação algorítmica antecipada de conflitos (regras anti-overbooking), controle de presença em sala de espera e registro obrigatório de motivos de cancelamento;
* **Módulo de Gestão de Pacientes:** Cadastro de pacientes integrado com autocompletar de endereços via API ViaCEP, rastreamento de dados sociodemográficos, histórico de agendamentos e inativação lógica com anonimização em observância à LGPD;
* **Módulo de Prontuário Eletrônico do Paciente (PEP):** Registro digital de consultas, anamneses, queixas clínicas, condutas, prescrições e atestados, dotado de versionamento imutável de anotações (trilha histórica que impede a adulteração ou apagamento de registros clínicos anteriores) e logs automatizados de auditoria de acessos;
* **Módulo de Notificações e Comunicação:** Gatilhos automatizados para envio de confirmações e lembretes de consultas aos pacientes via WhatsApp, com links diretos de confirmação ou remarcação;
* **Módulo Financeiro e Fluxo de Caixa:** Lançamentos de despesas e receitas, controle de status de cobrança (pendente/pago), fechamento mensal e suporte a pagamentos divididos/mistos (ex: entrada em dinheiro e saldo no cartão);
* **Módulo de Fidelização, Ouvidoria e Laboratório:** Alerta automatizado de retorno preventivo semestral (check-up 180+ dias), registro de pesquisas de satisfação de atendimento de 1 a 5 estrelas e rastreamento de status de amostras biológicas encaminhadas a laboratórios externos de apoio.

---

### CLÁUSULA SEGUNDA — DAS MODALIDADES DE CONTRATAÇÃO (ESCOLHA NO ANEXO I)

**2.1.** As partes declaram que a contratação poderá operar sob uma de duas modalidades comerciais distintas, cuja escolha vinculante é formalizada no **ANEXO I** deste contrato:

#### PARÁGRAFO PRIMEIRO — DA MODALIDADE A: SOFTWARE COMO SERVIÇO (SaaS / ASSINATURA RECORRENTE)
Caso eleita a **Modalidade A (SaaS)**:
1. **Taxa Única de Adesão e Implantação (Setup):** A CONTRATANTE pagará ao CONTRATADO o valor único e não reembolsável de **R$ 290,00 (duzentos e noventa reais)**, a ser quitado na data da assinatura deste contrato, cobrindo os serviços de:
   * Parametrização cadastral completa dos profissionais e horários da clínica;
   * Higienização e importação da base preexistente de pacientes (a partir de planilha Excel/CSV fornecida pela CONTRATANTE);
   * Provisionamento e isolamento do banco de dados relacional dedicado na nuvem;
   * Sessão de Onboarding e Treinamento operacional da equipe (profissionais de saúde e recepcionistas).
2. **Mensalidade de Licenciamento Recorrente:** A CONTRATANTE pagará o valor fixo mensal de **R$ 179,00 (cento e setenta e nove reais)**.
   * *Carência da Primeira Mensalidade:* O vencimento da primeira mensalidade ocorrerá impreterivelmente **30 (trinta) dias após a liberação do sistema em produção**, permitindo à CONTRATANTE o usufruto da plataforma durante o primeiro mês antes da primeira cobrança recorrente;
3. **Escopo dos Serviços Contínuos Inclusos na Mensalidade:**
   * Hospedagem de alta disponibilidade da aplicação e do banco de dados em infraestrutura de nuvem gerenciada, com custos de tráfego e servidores integralmente absorvidos pelo CONTRATADO;
   * Rotina automatizada e diária de cópias de segurança (backups) na nuvem;
   * Suporte técnico operacional contínuo e direito ao recebimento automático de todas as atualizações evolutivas, correções e melhorias de segurança que venham a ser lançadas na plataforma Clinix.

#### PARÁGRAFO SEGUNDO — DA MODALIDADE B: PROJETO DEDICADO FECHADO (LICENÇA PERPÉTUA DEDICADA)
Caso eleita a **Modalidade B (Projeto Dedicado Fechado)**:
1. **Valor Total do Projeto:** A CONTRATANTE pagará ao CONTRATADO o valor global e fixo de **R$ 2.400,00 (dois mil e quatrocentos reais)**, liquidado de acordo com o cumprimento dos seguintes marcos de entrega (*milestones*):
   * **Marco 1 — Entrada (40%): R$ 960,00 (novecentos e sessenta reais)**, pagos na assinatura deste contrato, como condição indispensável para início dos trabalhos de configuração, estilização visual e modelagem do banco de dados da clínica;
   * **Marco 2 — Entrega Intermediária (30%): R$ 720,00 (setecentos e vinte reais)**, pagos após demonstração do sistema configurado, com a base de dados populada e fluxos de agendamento e prontuário validados em ambiente de testes;
   * **Marco 3 — Homologação Final e Treinamento (30%): R$ 720,00 (setecentos e vinte reais)**, pagos na entrega do sistema em produção, conclusão do treinamento da equipe e emissão do Termo de Aceite de Homologação.
2. **Infraestrutura e Custos de Nuvem:** A contratação, pagamento e manutenção dos serviços de nuvem (provedor de banco de dados e servidores web) serão faturados diretamente em nome e cartão da CONTRATANTE, sendo esta a titular das contas de infraestrutura;
3. **Garantia Técnica Total de 90 Dias:** O CONTRATADO assegura garantia técnica integral pelo prazo de **90 (noventa) dias corridos**, contados da homologação final, para a correção prioritária e sem custos adicionais de quaisquer falhas de programação, lentidões anormais ou inconsistências de dados;
4. **Serviços Pós-Garantia (Opcionais):** Expirado o prazo de 90 dias de garantia, a CONTRATANTE poderá optar por:
   * *Contrato de Sustentação Mensal:* Valor de **R$ 90,00 a R$ 120,00 / mês**, contemplando monitoramento de integridade, rotina de backup semanal e suporte técnico a dúvidas;
   * *Banco de Horas Sob Demanda:* Valor de **R$ 75,00 (setenta e cinco reais) por hora técnica**, para atendimento a novas demandas, criação de relatórios sob medida ou campos personalizados.

---

### CLÁUSULA TERCEIRA — DA PROPRIEDADE INTELECTUAL E DIREITOS AUTORAIS

**3.1.** O software CLINIX, seu código-fonte, arquitetura de software, esquemas conceituais de banco de dados, algoritmos, fluxos operacionais, interfaces de usuário, marcas e documentações técnicas são de **propriedade intelectual exclusiva, universal e inalienável do CONTRATADO (DAVI FELINTO DOS SANTOS)**, sendo expressamente protegidos pela legislação autoral e de propriedade industrial brasileira.

**3.2.** O presente contrato confere à CONTRATANTE única e exclusivamente o **direito de uso** da solução, sendo-lhe expressamente vedado, sob pena de cominação de perdas e danos civis e criminais:
* Copiar, reproduzir, modificar, descompilar, realizar engenharia reversa ou tentar derivar o código-fonte da aplicação;
* Vender, revender, sublicenciar, alugar, ceder, emprestar, doar ou transferir a qualquer título a licença do software para terceiros;
* Utilizar o sistema para operar filiais, clínicas terceiras ou estabelecimentos não identificados no presente instrumento sem a prévia e expressa autorização do CONTRATADO.

**3.3.** Na **Modalidade A (SaaS)**, a licença é concedida em caráter **temporário, precário, não-exclusivo e revogável**, condicionada estritamente à pontualidade dos pagamentos das mensalidades contratadas.

**3.4.** Na **Modalidade B (Projeto Dedicado)**, a licença é concedida em caráter **perpétuo, não-exclusivo e intransferível**, estritamente limitada à unidade de atendimento da CONTRATANTE qualificada no preâmbulo, não importando em qualquer transferência da titularidade dos direitos autorais patrimoniais do software.

---

### CLÁUSULA QUARTA — DA PROTEÇÃO DE DADOS PESSOAIS (LGPD) E SIGILO MÉDICO

**4.1.** As partes declaram formalmente o seu integral compromisso com a Lei Geral de Proteção de Dados Pessoais (Lei nº 13.709/2018 — LGPD), bem como com as resoluções deontológicas dos Conselhos Profissionais de Saúde (especialmente a Resolução CFM nº 1.821/2007 do Conselho Federal de Medicina), estabelecendo de forma incontroversa a seguinte divisão de papéis:

* **A CONTRATANTE figura juridicamente como CONTROLADORA DOS DADOS PESSOAIS E SENSÍVEIS:** Cabe à CONTRATANTE a tomada de todas as decisões relativas aos dados dos pacientes, o recolhimento das bases legais aplicáveis (consentimento ou tutela da saúde), a garantia do sigilo profissional de seus médicos/atendentes e o atendimento direto aos titulares de dados e à Autoridade Nacional de Proteção de Dados (ANPD);
* **O CONTRATADO figura juridicamente como OPERADOR TÉCNICO:** O CONTRATADO realiza o tratamento dos dados estritamente em nome, por conta e em conformidade com as instruções técnicas da CONTRATANTE, limitando-se a manter a segurança, disponibilidade e custódia tecnológica da base de dados.

**4.2.** O CONTRATADO compromete-se a adotar medidas técnicas e administrativas aptas a proteger os dados pessoais contra acessos não autorizados, acidentes ou destruição, incluindo:
* Armazenamento de credenciais de acesso mediante algoritmos criptográficos robustos (PBKDF2 com Salt criptográfico e SHA-256);
* Registro de trilha de auditoria (*access logs*) imutável registrando qual usuário acessou cada prontuário, a data e o horário (em atenção ao art. 13 da LGPD e RN13 do projeto);
* Versionamento imutável de prontuários (RN17), preservando o histórico de alterações sem permitir o descarte indevido de diagnósticos pretéritos.

**4.3.** O CONTRATADO obriga-se a manter absoluto sigilo profissional sobre quaisquer prontuários, registros médicos ou informações financeiras a que tenha acesso fortuito durante intervenções de suporte técnico, não podendo utilizá-las para nenhuma finalidade alheia ao cumprimento deste contrato.

---

### CLÁUSULA QUINTA — DO NÍVEL DE SERVIÇO (SLA) E SUPORTE TÉCNICO

**5.1.** O suporte técnico será prestado pelo CONTRATADO prioritariamente em ambiente remoto, por meio de canal direto via **WhatsApp** e **E-mail técnico**, de **segunda a sexta-feira, das 08h00 às 18h00 (horário oficial de Brasília)**, ressalvados os feriados civis nacionais e do Distrito Federal.

**5.2.** Ficam estipulados os seguintes prazos máximos contratuais de atendimento e resposta (*SLA — Service Level Agreement*):
* **Chamados de Severidade Alta (Críticos):** Entende-se por crítico o cenário de inacessibilidade total do sistema ou pane de banco de dados que impeça a realização de atendimentos clínicos ou agendamentos.  
  *Prazo de primeira resposta:* **Até 2 (duas) horas úteis** a partir da abertura do chamado, com início imediato das ações de mitigação;
* **Chamados de Severidade Média/Baixa (Operacionais):** Dúvidas sobre preenchimento de cadastros, parametrização de agendas, instruções de uso de relatórios ou solicitações de treinamento complementar.  
  *Prazo de primeira resposta:* **Até 24 (vinte e quatro) horas úteis**.

**5.3.** Não se encontram cobertos pelo suporte padrão gratuito: defeitos ou lentidões decorrentes da conexão de internet da clínica, panes em equipamentos de hardware da CONTRATANTE (computadores, roteadores ou impressoras), ou intervenções causadas por invasão de vírus decorrentes de mau uso dos computadores da clínica.

---

### CLÁUSULA SEXTA — DO PAGAMENTO, REAJUSTE E INADIMPLÊNCIA

**6.1.** Todos os valores pecuniários devidos em razão deste instrumento serão quitados pela CONTRATANTE por meio de **Pix** ou **Transferência Bancária**, mediante comprovante emitido aos dados bancários indicados no ANEXO I.

**6.2.** Na **Modalidade A (SaaS)**, o valor da mensalidade (R$ 179,00) será reajustado anualmente, a contar da data de assinatura deste instrumento, pela variação positiva acumulada do Índice Nacional de Preços ao Consumidor Amplo (IPCA/IBGE) ou, na falta deste, pelo índice legal substitutivo.

**6.3.** O atraso injustificado no pagamento de qualquer parcela ou mensalidade acarretará a incidência de:
* Multa moratória não compensatória de **2% (dois por cento)** sobre o montante em atraso;
* Juros de mora de **1% (um por cento) ao mês**, calculados *pro rata die* até a data da efetiva quitação;
* Correção monetária com base no IPCA/IBGE.

**6.4.** Na ocorrência de inadimplência na **Modalidade A (SaaS)**, será observada a seguinte régua formal de cobrança:
* **5º (quinto) dia de atraso:** Envio de notificação formal de advertência via e-mail e WhatsApp comunicando o débito pendente;
* **10º (décimo) dia de atraso:** **Suspensão temporária do acesso à plataforma**, com bloqueio de novos logins de profissionais e atendentes;
* **Garantia de Preservação dos Dados:** Durante o período de suspensão e pelo prazo de até **90 (noventa) dias corridos** contados do vencimento original, o CONTRATADO manterá a integridade e segurança de todos os dados clínicos da clínica, permitindo a imediata reativação do acesso no prazo de 24 (vinte e quatro) horas após a comprovação da quitação do débito pendente.

---

### CLÁUSULA SÉTIMA — DA VIGÊNCIA, RESCISÃO E PORTABILIDADE DE DADOS

**7.1.** Na **Modalidade A (SaaS)**, este contrato vigerá pelo prazo inicial de **12 (doze) meses**, sendo renovado automaticamente por iguais períodos sucessivos, caso não haja manifestação expressa em contrário por qualquer das partes.

**7.2.** A rescisão antecipada da Modalidade A (SaaS) poderá ser requerida imotivadamente por qualquer das partes a qualquer momento, mediante envio de **notificação formal por escrito com antecedência mínima de 30 (trinta) dias corridos**, sem incidência de multas rescisórias punitivas, exigindo-se apenas a liquidação das mensalidades vencidas e da respectiva taxa de adesão inicial.

**7.3.** Na **Modalidade B (Projeto Dedicado)**, o contrato vigerá desde a assinatura até a homologação final dos serviços e decurso integral dos 90 (noventa) dias de garantia técnica.

**7.4. CLÁUSULA INDISPENSÁVEL DE PORTABILIDADE E DEVOLUÇÃO DE DADOS (LGPD / CFM):**
Em qualquer modalidade de contratação e independentemente do motivo que der causa ao encerramento ou rescisão do contrato:
* O CONTRATADO obriga-se a disponibilizar à CONTRATANTE, no prazo máximo e improrrogável de **15 (quinze) dias úteis**, uma cópia integral, fidedigna e consolidada de toda a base de dados da clínica (incluindo prontuários, dados de pacientes, agendamentos e transações financeiras) em formatos interoperáveis, abertos e legíveis por máquina, especificamente **arquivos SQL e planilhas eletrônicas estruturadas (CSV/Excel)**;
* A entrega dos dados confere à CONTRATANTE plena autonomia para migração dos dados a outro sistema de sua escolha, vedada a imposição de quaisquer entraves técnicos ou taxas punitivas de liberação de dados;
* Cumprida a entrega e decorrido o prazo legal de salvaguarda de **60 (sessenta) dias corridos**, o CONTRATADO promoverá a exclusão segura, definitiva e irreversível de quaisquer cópias dos dados clínicos de seus servidores, ressalvados unicamente os comprovantes contábeis e fiscais de guarda obrigatória por lei.

---

### CLÁUSULA OITAVA — DAS DISPOSIÇÕES FINAIS E DO FORO

**8.1.** A eventual tolerância de qualquer das partes quanto ao descumprimento de quaisquer cláusulas ou prazos deste instrumento constituirá mera liberalidade, não implicando em novação, alteração contratual ou renúncia a direitos.

**8.2.** As comunicações formais entre as partes serão realizadas por escrito, reputando-se válidas e eficazes as notificações encaminhadas aos endereços eletrônicos (e-mails) e números de WhatsApp institucionalmente cadastrados no preâmbulo deste contrato.

**8.3.** As partes elegem, de comum acordo, o **Foro da Circunscrição Judiciária de Brasília, Distrito Federal**, com expressa renúncia a qualquer outro, por mais privilegiado que seja ou venha a ser, para dirimir quaisquer litígios, dúvidas ou controvérsias oriundas da interpretação ou execução deste contrato.

E, por estarem assim justas e contratadas, as partes firmam o presente instrumento em 2 (duas) vias de igual teor e forma, ou mediante assinatura eletrônica qualificada/avançada nos termos da Lei nº 14.063/2020, na presença de 2 (duas) testemunhas instrumentárias abaixo nomeadas.

---

Brasília - DF, `[DIA]` de `[MÊS]` de 2026.

<br><br>

_____________________________________________________________  
**CONTRATADO:** DAVI FELINTO DOS SANTOS  
CPF: `[000.000.000-00]`  

<br><br>

_____________________________________________________________  
**CONTRATANTE:** `[NOME DA CLÍNICA / PROFISSIONAL DE SAÚDE]`  
Representante Legal: `[NOME DO RESPONSÁVEL]`  
CPF: `[000.000.000-00]` | CRM/CRO/CRP: `[00000-DF]`  

<br><br>

**TESTEMUNHAS:**

1. _________________________________________________________  
   Nome:  
   CPF:  

2. _________________________________________________________  
   Nome:  
   CPF:  

---

<br><br>

# ANEXO I — FORMULÁRIO DE ESCOLHA DE MODALIDADE E PARAMETRIZAÇÃO

*(Este anexo é parte integrante e indissociável do Contrato de Licenciamento e Prestação de Serviços Técnicos do Software Clinix)*

---

### 1. SELEÇÃO DA MODALIDADE COMERCIAL VINCULANTE:

*(Assinale com um **X** a modalidade formalmente contratada pela clínica)*

- [ ] **OPÇÃO A — SOFTWARE COMO SERVIÇO (SaaS / ASSINATURA RECORRENTE):**
  * **Taxa Única de Adesão e Implantação:** R$ 290,00 (duzentos e noventa reais), à vista na assinatura.
  * **Mensalidade de Licenciamento:** R$ 179,00 (cento e setenta e nove reais) / mês.
  * **Carência:** A primeira mensalidade vencerá somente após 30 (trinta) dias de uso efetivo em produção.
  * **Infraestrutura em Nuvem & Backups:** 100% Inclusos na assinatura.
  * **Suporte Técnico & Atualizações:** Contínuos durante toda a vigência.

<br>

- [ ] **OPÇÃO B — PROJETO DEDICADO FECHADO (LICENÇA PERPÉTUA DEDICADA):**
  * **Valor Global Fechado:** R$ 2.400,00 (dois mil e quatrocentos reais).
  * **Forma de Liquidação:**
    - Marco 1 (Entrada de 40%): R$ 960,00 na assinatura deste contrato;
    - Marco 2 (Intermediária de 30%): R$ 720,00 na entrega do ambiente de homologação com dados reais;
    - Marco 3 (Final de 30%): R$ 720,00 na homologação final e treinamento operacional da equipe.
  * **Infraestrutura em Nuvem:** Servidores de nuvem cadastrados e custeados diretamente pela Contratante.
  * **Garantia Técnica:** 90 (noventa) dias corridos a partir da homologação final.
  * **Pós-Garantia Opcional:** Suporte mensal por R$ 90,00 a R$ 120,00/mês ou Banco de Horas por R$ 75,00/h.

---

### 2. DADOS BANCÁRIOS DO CONTRATADO PARA PAGAMENTOS (PIX / TED):

* **Favorecido:** Davi Felinto dos Santos
* **Chave Pix (E-mail):** `davifd0978@gmail.com`
* **Instituição Bancária:** `[Banco Inter / Nubank / Banco do Brasil]`
* **Agência:** `[0001]` | **Conta Corrente:** `[0000000-0]`

---

### 3. VISTO DAS PARTES NO ANEXO I:

<br>

_____________________________________________  
**Visto do Contratado (Davi Felinto)**  

<br>

_____________________________________________  
**Visto do(a) Contratante (Representante Legal)**  
