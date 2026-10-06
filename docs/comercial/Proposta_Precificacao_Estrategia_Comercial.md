# Proposta de Precificação e Estratégia Comercial
## Sistema de Agendamento, Prontuário e Gestão Clínica (Clinix)

> **Documento de Estratégia de Monetização, Benchmark de Mercado e Modelos de Proposta**  
> **Autor:** Davi Felinto — Engenharia de Software (CEUB)  
> **Objetivo:** Definir a política de precificação para o primeiro cliente real e estruturar a viabilidade comercial do projeto  
> **Data:** Outubro / 2026 — Versão: 1.0  

---

## 1. Visão Geral e Posicionamento Estratégico

A entrada de um primeiro cliente interessado no sistema representa a transição do projeto de um ambiente acadêmico para um **produto de mercado real (Micro-SaaS)**.

Como o projeto está no 2º semestre da graduação e continuará recebendo melhorias técnicas e arquiteturais (Banco de Dados relacional, infraestrutura em nuvem e novas funcionalidades), o modelo financeiro deve atender a três objetivos estratégicos:

1. **Garantir a adesão imediata da cliente:** Sem barreiras financeiras altas que causem desistência ou hesitação.
2. **Gerar receita recorrente previsível:** Para cobrir custos de infraestrutura e recompensar o tempo investido em suporte e evolução.
3. **Validar o produto no mundo real:** Transformar a cliente em um **case de sucesso de referência** para atrair novos consultórios.

---

## 2. Por que NÃO Vender por Valor Único Fechado?

Na área de software médico e de gestão clínica, a cobrança por valor único (ex.: *"vender o código ou licença vitalícia por R$ 2.000"*) é um erro grave:

| Risco do Modelo Fechado | Impacto Prático no Projeto |
| :--- | :--- |
| **Trabalho Não Remunerado** | Sistemas de saúde exigem manutenção, backups e suporte. Cobrar apenas na entrega obriga o desenvolvedor a prestar suporte contínuo de graça. |
| **Insegurança da Cliente** | Desembolsar R$ 2.000 ou R$ 3.000 à vista por uma solução em validação gera atrito e medo de risco financeiro. |
| **Bloqueio de Evolução** | O software fica congelado no tempo na versão vendida, inviabilizando o modelo de plataforma que atende várias clínicas. |

> **Diretriz:** O modelo correto e sustentável é o de **Assinatura Recorrente (Software as a Service — SaaS)** com um período inicial de **Programa de Cliente Piloto**.

---

## 3. Benchmark de Mercado (Preços no Brasil)

Pesquisa de mercado em soluções concorrentes para profissionais autônomos e consultórios de pequeno porte:

| Plataforma Concorrente | Funcionalidades Básicas | Mensalidade Praticada |
| :--- | :--- | :---: |
| **Sistemas de Entrada** *(iClinic, Doctoralia básico)* | Apenas agenda e prontuário simples | **R$ 99,00 a R$ 149,00 / mês** |
| **Sistemas com WhatsApp & Financeiro** *(Simples Dental, Zenklub Pro)* | Lembretes automáticos, histórico e fluxo de caixa | **R$ 160,00 a R$ 260,00 / mês** |
| **Sistemas Clínicos Completos** *(Feegow, Amplimed)* | Faturamento convênios, recepção integrada | **R$ 350,00 a R$ 600,00+ / mês** |

### Percepção de Valor e Retorno sobre o Investimento (ROI):
* Em consultórios particulares, o valor médio de uma única consulta varia de **R$ 150,00 a R$ 300,00**.
* O módulo de **confirmação e lembrete automático via WhatsApp** reduz as faltas de pacientes em até 40%.
* **Argumento Central:** Se o sistema evitar que **1 único paciente falte no mês inteiro**, ele já gerou mais economia do que o custo da sua mensalidade!

---

## 4. Modelos de Precificação Propostos

Para apresentar à cliente, são definidos dois modelos viáveis, com destaque para a Opção A.

### 🟢 Opção A (Recomendada): Plano Parceiro Fundador (SaaS Recorrente)

Proposta focada em eliminar o risco inicial da cliente e construir um relacionamento de longo prazo.

* **Etapa 1: Piloto de Validação (30 dias gratuitos):**
  * **Investimento:** R$ 0,00.
  * **Compromisso da Cliente:** Utilizar o sistema na rotina diária e realizar dois alinhamentos de feedback (quinzenais) com o desenvolvedor.
* **Etapa 2: Mensalidade de Parceiro Fundador:**
  * **Valor:** **R$ 97,00 a R$ 127,00 / mês** (valor travado por 12 meses em contrato).
  * **O que inclui:**
    * Acesso multi-dispositivo ilimitado (computador, tablet e celular);
    * Suporte técnico via WhatsApp em horário comercial;
    * Hospedagem segura na nuvem com rotina de backup diário;
    * Automação de notificações de agendamento e lembretes via WhatsApp;
    * Todas as melhorias e novos módulos lançados ao longo do ano.

---

### 🔵 Opção B: Taxa de Implantação (Setup) + Mensalidade Reduzida

Modelo indicado caso a cliente exija que você realize a migração manual de fichas antigas ou treinamento presencial da equipe de recepção.

* **Taxa de Implantação e Configuração (Setup Único):**
  * **Valor:** **R$ 350,00 a R$ 500,00** (pago na contratação).
  * **Serviços inclusos:** Parametrização da agenda, cadastro de serviços/valores, importação da lista inicial de pacientes e treinamento operacional da equipe.
* **Mensalidade de Manutenção e Hospedagem:**
  * **Valor:** **R$ 79,00 a R$ 97,00 / mês**.
  * **Serviços inclusos:** Hospedagem na nuvem, backups diários, suporte técnico e manutenção preventiva.

---

## 5. Estimativa de Custos Operacionais e Lucratividade

Para manter o sistema em produção para 1 a 3 clínicas, a infraestrutura requer custos mínimos:

| Componente | Provedor / Solução Técnica | Custo Mensal Estimado |
| :--- | :--- | :---: |
| **Hospedagem Front/Backend** | Railway, Render ou VPS básica (Hetzner / Hostinger) | R$ 0,00 a R$ 30,00 |
| **Banco de Dados (MySQL)** | Supabase, Railway ou servidor MySQL na VPS | R$ 0,00 a R$ 25,00 |
| **Disparo de WhatsApp** | Evolution API / Z-API / WhatsApp Webhook | R$ 0,00 a R$ 40,00 |
| **Custo Total Estimado:** | | **~ R$ 30,00 a R$ 50,00 / mês** |

### 📈 Projeção Financeira e Margem de Lucro:
* **Com 1 Cliente (R$ 119,00/mês):** Cobre 100% dos custos operacionais e gera lucro líquido de ~R$ 70,00 a R$ 85,00/mês.
* **Com 5 Clientes (R$ 119,00/mês = R$ 595,00):** O custo de infraestrutura quase não se altera, gerando lucro líquido superior a **R$ 500,00/mês recorrentes**.

---

## 6. Roteiro de Negociação e Apresentação da Proposta

Quando a cliente perguntar sobre os valores do sistema durante a reunião:

### 🗣️ Script de Negociação Recomendado:
> *"Hoje, softwares consolidados desse segmento cobram entre R$ 180 e R$ 260 por mês, além de taxas altas de implantação, e muitas vezes oferecem interfaces engessadas e suporte impessoal.*
> 
> *Como o Clinix está iniciando a fase de implantação com profissionais parceiros selecionados, nós criamos o **Programa de Parceiro Fundador**:*
> 
> 1. *Disponibilizamos **30 dias de uso piloto sem qualquer custo** no seu consultório, para você validar na prática o ganho de tempo e a redução de faltas.*
> 2. *Após esse período de 30 dias, se o sistema atender plenamente às suas expectativas, você passa a ter uma mensalidade especial fixada de apenas **R$ 119,00 por mês** [ou R$ 97,00].*
> 3. *Esse valor cobre a hospedagem na nuvem, backups diários de segurança, suporte direto comigo e todas as novas atualizações que lançarmos, sem qualquer taxa de fidelidade ou multa de cancelamento.*
> 
> *O que acha dessa proposta para estruturarmos o início do piloto?"*

---

## 7. Como Lidar com Objeções Comuns de Preço

| Objeção da Cliente | Abordagem Recomendada |
| :--- | :--- |
| **"Por que não é de graça se é projeto de faculdade?"** | *"O projeto acadêmico serviu para validar a engenharia e os 23 testes de qualidade. Para operar no seu consultório, o sistema consome servidores profissionais em nuvem, rotinas de backup diário e suporte técnico dedicado, garantindo que os dados dos seus pacientes estejam seguros e dentro da LGPD."* |
| **"Achei um sistema grátis na internet."** | *"Sistemas gratuitos geralmente não têm suporte, não realizam backup garantido e costumam monetizar vendendo anúncios ou limitando o número de pacientes quando você mais precisa. O Clinix oferece canal direto de atendimento e segurança jurídica no prontuário."* |
| **"Não quero pagar mensalidade, prefiro pagar uma vez só."** | *"O modelo de assinatura é a garantia de que o seu sistema nunca ficará desatualizado ou abandonado. Ele garante que a hospedagem esteja sempre ativa, com backups diários e novos recursos sendo adicionados continuamente sem custos surpresa."* |

---

## 8. Alinhamento de Expectativas e Limites do Acordo (SLA)

Para preservar o seu tempo e foco nos estudos:

1. **Horário e Canal de Suporte:**
   - Atendimento via WhatsApp em horário comercial (segunda a sexta, das 08h às 18h).
   - Prazo de resposta para dúvidas: até 4 horas úteis.
   - Chamados críticos (ex.: indisponibilidade): prioridade imediata.
2. **Escopo do Sistema:**
   - O plano cobre os módulos de Agenda, Cadastro de Pacientes, Prontuário Eletrônico, Controle Financeiro e Trilha de Auditoria.
   - Demandas de novas funcionalidades complexas (ex.: faturamento de convênios TISS, nota fiscal eletrônica) entrarão na lista de evolução e serão orçadas separadamente se exigirem customização exclusiva.
3. **Propriedade dos Dados:**
   - Todos os dados cadastrais, históricos de consultas e prontuários pertencem 100% à cliente e à clínica, podendo ser exportados a qualquer momento em conformidade com a LGPD.
4. **Contrapartida de Case de Sucesso:**
   - Ao final dos 30 dias de piloto, a cliente concorda em fornecer um breve depoimento sobre os resultados práticos obtidos (ganho de tempo e redução de faltas), autorizando a citação do consultório como case de referência.
