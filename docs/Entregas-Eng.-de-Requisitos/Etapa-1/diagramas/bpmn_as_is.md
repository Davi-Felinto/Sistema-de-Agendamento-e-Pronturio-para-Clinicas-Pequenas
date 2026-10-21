# Mapeamento do Processo Atual (AS-IS) — Consultório Amanda Souza Peixoto

> **Modelagem BPMN em Raias (Swimlanes)**  
> **Disciplina:** Engenharia de Requisitos — Professor Sérgio Haddad (CEUB)  
> **Equipe:** Davi Felinto, Isaac Soares, Miguel Dias, Lucas Evaldo, Vanessa Mendoça  
> **Cliente / Entrevistada:** Amanda Souza Peixoto  

---

## Diagrama BPMN (AS-IS)

```mermaid
flowchart TD
    subgraph PACIENTE ["Raia 1: Paciente"]
        P1([Início]) --> P2[Solicita agendamento via WhatsApp ou telefone]
        P3[Recebe mensagem de confirmação se lembrada pela secretária]
        P4[Falta à consulta por esquecimento ou comparece]
        P5[Aguarda na fila da recepção e preenche ficha em papel]
        P6[Comparece à consulta com Dra. Amanda]
        P7[Solicita pagamento dividido na saída dinheiro e cartão]
    end

    subgraph RECEPCAO ["Raia 2: Recepção / Secretária"]
        R1[Consulta caderno físico ou planilha Excel local]
        R2{Horário livre?}
        R3[Registra agendamento à mão risco de rasura e overbooking]
        R4[Envia lembrete manual no dia anterior se houver tempo]
        R5[Entrega ficha em papel sem validação de CEP]
        R6[Encaminha amostra biológica ao laboratório parceiro]
        R7[Anota pagamento misto e pendência em caderno]
        R8[Ausência de pesquisa de satisfação e de recall de 6 meses]
    end

    subgraph AMANDA ["Raia 3: Dra. Amanda Souza Peixoto"]
        M1[Realiza atendimento clínico e anamnese]
        M2[Anota evolução clínica em papel ou arquivo Word sem versões]
        M3[Coleta amostra biológica e entrega à recepção]
    end

    subgraph LAB ["Raia 4: Laboratório Parceiro Terceirizado"]
        L1[Recebe material biológico sem código de rastreio integrado]
        L2[Emite laudo com atraso; retorno clínico ocorre sem o laudo]
    end

    P2 --> R1
    R1 --> R2
    R2 -- Não (Conflito) --> P2
    R2 -- Sim --> R3
    R3 -.-> R4
    R4 -.-> P3
    P3 --> P4
    P4 -- Comparece --> P5
    P5 --> R5
    R5 --> M1
    M1 --> M2
    M1 --> M3
    M3 --> R6
    R6 --> L1
    L1 --> L2
    M1 --> P7
    P7 --> R7
    R7 --> R8
    R8 --> FIM([Fim do Fluxo sem Retenção])
```

---

## Gargalos Críticos do Processo AS-IS

1. **Overbooking e Choque de Horários:** Uso de caderno/planilha sem trava algorítmica.
2. **Absenteísmo Elevado (No-Show de 25% a 30%):** Falta de automação de lembretes preventivos 24h.
3. **Insegurança Jurídica e Risco LGPD:** Prontuário em papel/Word sem imutabilidade, versionamento ou trilha de auditoria.
4. **Descontrole de Pagamento Dividido:** Pagamento de entrada em PIX e saldo em cartão anotado em bloco de notas.
5. **Retorno Clínico Frustrado:** Paciente retorna sem que o laudo do laboratório parceiro tenha sido liberado.
6. **Perda de Recorrência (180+ dias):** Sem mecanismo de pós-consulta e recall de check-up semestral.
