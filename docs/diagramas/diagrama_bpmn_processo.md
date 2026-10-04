# Diagrama de Processo de Negócio "TO-BE" (BPMN / Raias)

> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> Mapeamento de processo de ponta a ponta estruturado em três raias: Paciente, Profissional e Sistema.

---

## 1. Visão do Fluxo de Processo com Raias (Swimlanes)

O fluxo TO-BE cobre desde o primeiro contato do paciente até a consolidação financeira mensal, integrando os requisitos funcionais e regras de negócio:

```mermaid
flowchart TD
    subgraph PACIENTE ["Raia 1: Paciente"]
        P1([Início]) --> P2[Solicita agendamento de consulta]
        P3[Recebe mensagem de confirmação via WhatsApp]
        P4[Recebe lembrete automático 24h antes]
        P5[Comparece à consulta]
        P6[Efetua o pagamento do atendimento]
    end

    subgraph PROFISSIONAL ["Raia 2: Profissional / Atendente"]
        PR1[Informa dados do paciente e horário pretendido]
        PR2[Realiza a consulta clínica]
        PR3[Registra a sessão no prontuário eletrônico]
        PR4[Registra status de pagamento pago/pendente]
        PR5[Gera resumo financeiro mensal]
    end

    subgraph SISTEMA ["Raia 3: Sistema Automatizado"]
        S1{Paciente já cadastrado?}
        S2[Cadastra novo paciente RF01]
        S3{Existe conflito de horário? RN01, RN02}
        S4[Solicita novo horário / Alerta de conflito]
        S5[Confirma e grava agendamento RF06]
        S6[Dispara confirmação automática via WhatsApp RF12, RN03]
        S7[Dispara lembrete 24h antes via WhatsApp RF13, RN04]
        S8[Atualiza prontuário e histórico do paciente RF15, RN06]
        S9[Salva pagamento e atualiza pendências RF19, RN07, RN08]
        S10[Consolida faturamento e consultas do mês RF22, RN09]
    end

    P2 --> PR1
    PR1 --> S1
    S1 -- Não --> S2 --> S3
    S1 -- Sim --> S3
    S3 -- Sim (Conflito) --> S4 --> PR1
    S3 -- Não (Livre) --> S5
    S5 --> S6 --> P3
    S5 --> S7 --> P4
    P4 -.-> P5
    P5 --> PR2
    PR2 --> PR3
    PR3 --> S8
    PR3 --> P6
    P6 --> PR4
    PR4 --> S9
    PR5 --> S10
```

---

## 2. Destaques das Regras de Negócio Integradas ao Processo

* **RN01 / RN02:** Validação automática de conflito de agenda antes de qualquer confirmação.
* **RN03 / RN04:** Automação de confirmação imediata e lembrete preventivo 24h antes via WhatsApp.
* **RN05 / RN06:** Vinculação estrita entre a realização da consulta e a atualização do histórico clínico do paciente.
* **RN07 / RN08 / RN09:** Fechamento financeiro com suporte a pagamentos pendentes e consolidação mensal.
