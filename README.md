# Sistema de Agendamento e Prontuário para Clínicas Pequenas

> **Projeto Integrador Multidisciplinar** — Engenharia de Software (CEUB)  
> Sistema simplificado de agendamento e prontuário eletrônico voltado para profissionais autônomos de saúde (psicólogos, nutricionistas, fisioterapeutas, etc.).

---

## 🎯 Divisão do Projeto (4 Fases)

O projeto é estruturado de forma integrada através de 4 disciplinas de Engenharia de Software:

```
┌──────────────────────────────────────────────────────────────────────────────┐
│                            PROJETO INTEGRADOR                                │
├──────────────────┬──────────────────┬───────────────────┬────────────────────┤
│ 1. Eng. Requisitos│ 2. POO (C#)      │ 3. Banco Dados II │ 4. Desenv. Interf. │
│   [✅ CONCLUÍDO]  │  [🚀 FOCO ATUAL]  │   [⏳ PLANEJADO]  │   [⏳ FUTURO]      │
└──────────────────┴──────────────────┴───────────────────┴────────────────────┘
```

| Fase | Disciplina | Foco & Entregáveis | Status |
| :--- | :--- | :--- | :---: |
| **Parte 1** | **Engenharia de Requisitos** | Mapeamento BPMN, Levantamento com cliente, 28 RFs, 7 RDs, 17 RNs e 16 RQs (ISO/IEC 25010). | ✅ Concluído |
| **Parte 2** | **Programação Orientada a Objetos** | Modelagem de classes de domínio, encapsulamento de regras de negócio em C# e aplicação Console CLI iterativa. | 🚀 Em andamento |
| **Parte 3** | **Banco de Dados II** | Modelo relacional (DER/MER), tabelas normalizadas, integridade referencial, transações ACID e consultas analíticas. | ⏳ Próxima fase |
| **Parte 4** | **Desenvolvimento de Interface** | Interface acessível e responsiva focada no fluxo rápido de atendimento (agendamento em até 3 cliques). | ⏳ Futuro |

---

## 📐 Modelagem Orientada a Objetos (Diagrama de Classes)

O diagrama abaixo representa as entidades, métodos, enumerações e relações do domínio, cobrindo integralmente as regras de negócio de **RF01 a RF28** e **RN01 a RN17**:

<p align="center">
  <img src="docs/diagramas/diagrama_classes.svg" alt="Diagrama de Classes do Domínio" width="100%">
</p>

* 📄 **Documentação detalhada das classes:** [`docs/diagramas/README.md`](docs/diagramas/README.md)
* 🎨 **Arquivo editável no Excalidraw:** [`docs/diagramas/diagrama_classes.excalidraw`](docs/diagramas/diagrama_classes.excalidraw) *(abra em [excalidraw.com](https://excalidraw.com) ou na extensão Excalidraw do VS Code)*

---

## 📋 Status Atual de Entregas

- [x] **Parte 1 — Engenharia de Requisitos**
  - [x] Levantamento e elicitação de necessidades do cliente
  - [x] Mapeamento de processo "TO-BE" em BPMN
  - [x] Especificação formal de Requisitos Funcionais (RF01–RF28) e Requisitos de Dados (RD01–RD07)
  - [x] Definição de Regras de Negócio (RN01–RN17) e Requisitos de Qualidade (RQ01–RQ16)
  - [x] Validação com cliente simulado e consolidação de decisões (LGPD, exclusão lógica, WhatsApp)
- [ ] **Parte 2 — Programação Orientada a Objetos (C#)**
  - [x] Modelagem do Diagrama de Classes UML (SVG e Excalidraw)
  - [ ] Implementação das entidades do domínio (`Paciente`, `Agendamento`, `SessaoProntuario`, etc.)
  - [ ] Implementação das regras de negócio em serviços (`AgendaService`, `ProntuarioService`, `FinanceiroService`)
  - [ ] Interface Console interativa para testes e demonstração do MVP
- [ ] **Parte 3 — Banco de Dados II**
  - [ ] Modelo Conceitual e Lógico/Físico (DER)
  - [ ] Scripts DDL/DML, integridade e transações de concorrência
- [ ] **Parte 4 — Desenvolvimento de Interface**
  - [ ] Protótipo e telas de usuário simplificadas

---

## 📂 Estrutura do Repositório

```
├── docs/
│   ├── diagramas/
│   │   ├── README.md                           # Detalhamento arquitetural do diagrama de classes
│   │   ├── diagrama_classes.svg                # Diagrama vetorial estilizado para visualização
│   │   └── diagrama_classes.excalidraw         # Arquivo nativo editável no Excalidraw
│   ├── requisitos/
│   │   └── Documento_Especificacao_Requisitos_Final.docx # Especificação formal completa
│   └── Projeto_Integrador_Contexto_Completo.md # Memória e histórico unificado do projeto
├── src/                                        # Código-fonte C# (.NET Console App em construção)
└── README.md
```

---

## 👨‍💻 Autor

**Davi Felinto**  
Estudante de Engenharia de Software — CEUB (Brasília/DF)  
GitHub: [@Davi-Felinto](https://github.com/Davi-Felinto)
