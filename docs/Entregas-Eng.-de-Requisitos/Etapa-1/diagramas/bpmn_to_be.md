# Mapeamento do Processo Proposto (TO-BE) — Sistema Clinix

> **Modelagem BPMN em Raias (Swimlanes)**  
> **Disciplina:** Engenharia de Requisitos — Professor Sérgio Haddad (CEUB)  
> **Equipe:** Davi Felinto, Isaac Soares, Miguel Dias, Lucas Evaldo, Vanessa Mendoça  
> **Cliente / Entrevistada:** Amanda Souza Peixoto  

---

## Diagrama BPMN (TO-BE)

```mermaid
flowchart TD
    subgraph PACIENTE ["Raia 1: Paciente"]
        P1([Início]) --> P2[Solicita agendamento de consulta]
        P3[Recebe confirmação imediata via WhatsApp RF12, RN03]
        P4[Recebe lembrete automático 24h antes RF13, RN04]
        P5[Comparece à consulta pontualmente]
        P6[Efetua pagamento flexível dividido entrada + saldo RF30, RN19]
        P7[Avalia atendimento na ouvidoria 1 a 5 estrelas RF32, RN21]
    end

    subgraph RECEPCAO ["Raia 2: Atendente / Recepção"]
        R1[Inicia agendamento selecionando Amanda e horário pretendido]
        R2[Cadastra paciente com autopreenchimento por CEP RF29, RN18]
        R3[Registra quitação de entrada e programa saldo restante RF30]
        R4[Acompanha mural de ouvidoria e cancelamentos categorizados RF31]
    end

    subgraph AMANDA ["Raia 3: Dra. Amanda Souza Peixoto"]
        M1[Realiza atendimento clínico especializado]
        M2[Registra anotação em prontuário eletrônico com versionamento RF15, RN17]
        M3[Cadastra pedido de exame laboratorial externo com código e data RF34]
    end

    subgraph SISTEMA ["Raia 4: Sistema Clinix (Automação em Nuvem)"]
        S1{Valida conflito na grade RN01, RN02}
        S2[Solicita novo horário / Alerta de colisão]
        S3[Confirma e grava agendamento no banco MySQL RF06]
        S4[Dispara confirmação instantânea no WhatsApp RF12, RN03]
        S5[Dispara lembrete 24h preventivo no WhatsApp RF13, RN04]
        S6[Registra log imutável de auditoria LGPD RF28, RN13]
        S7{Exame laboratorial possui laudo liberado? RN23}
        S8[Emite alerta visual de amostra pendente na agenda]
        S9[Calcula conciliação de pagamentos mistos e emite relatório RF20, RN09]
        S10[Monitora pacientes sem retorno há 180+ dias e gera cupom CHECKUP10 RF33, RN22]
    end

    subgraph LAB ["Raia 5: Laboratório Parceiro Terceirizado"]
        L1[Recebe material com código de rastreamento digital]
        L2[Executa processamento técnico da amostra]
        L3[Disponibiliza laudo digital no sistema liberando retorno clínico]
    end

    P2 --> R1
    R1 --> R2
    R2 --> S1
    S1 -- Sim (Conflito) --> S2 --> R1
    S1 -- Não (Livre) --> S3
    S3 --> S4 --> P3
    S3 --> S5 --> P4
    P4 -.-> P5
    P5 --> M1
    M1 --> M2
    M2 --> S6
    M1 --> M3
    M3 --> L1
    L1 --> L2 --> L3
    L3 --> S7
    S7 -- Não --> S8
    S7 -- Sim --> M1
    M1 --> P6
    P6 --> R3
    R3 --> S9
    P6 --> P7
    P7 --> R4
    S9 --> S10
    S10 --> FIM([Fim do Ciclo Clínico com Retenção Ativa])
```

---

## Diferenciais da Solução TO-BE

1. **Validação Matemática Anti-Conflito (RN01, RN02):** Zero duplicidade de consultas.
2. **Engajamento Digital via WhatsApp (RF12, RF13, RN03, RN04):** Redução de até 80% do no-show.
3. **Agilidade Cadastral (RF29, RN18):** Autopreenchimento de logradouro, bairro, cidade e UF a partir do CEP.
4. **Segurança Jurídica e LGPD (RF15, RF16, RN17, RN13):** Histórico imutável de prontuários com trilha de auditoria.
5. **Flexibilidade Financeira (RF30, RN19):** Pagamento misto/dividido (Entrada + Saldo) com conciliação automática.
6. **Controle de Evasão (RF31, RN20):** Obrigatoriedade de categorizar cancelamentos para cálculo de métricas.
7. **Ouvidoria Transparente (RF32, RN21):** Notas de 1 a 5 estrelas e depoimentos anônimos.
8. **CRM Preventivo Semestral (RF33, RN22):** Recall ativo de pacientes ausentes há mais de 180 dias com cupom `CHECKUP10`.
9. **Sincronismo com Laboratórios Externos (RF34, RN23):** Amostras biológicas rastreadas com trava visual na agenda.
