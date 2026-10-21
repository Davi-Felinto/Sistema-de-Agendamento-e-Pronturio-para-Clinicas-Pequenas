# 📂 Entregas Formais — Engenharia de Requisitos

> **CEUB — Centro Universitário de Brasília**  
> **Curso:** Bacharelado em Engenharia de Software  
> **Disciplina:** Engenharia de Requisitos  
> **Docente:** Prof. Sérgio Haddad  
> **Equipe de Desenvolvimento:**  
> • Davi Felinto  
> • Isaac Soares  
> • Miguel Dias  
> • Lucas Evaldo  
> • Vanessa Mendoça  
> **Cliente / Estudo de Caso:** Amanda Souza Peixoto (Consultório Clínico e Gestão em Saúde — Brasília-DF)  

---

## 🗂️ Estrutura das Entregas

Este diretório concentra todos os artefatos acadêmicos solicitados pelo docente para a construção e avaliação do Projeto Final da disciplina de Engenharia de Requisitos:

```
docs/Entregas-Eng.-de-Requisitos/
├── README.md                                                     # Visão geral e guia de navegação das entregas
└── Etapa-1/                                                      # ETAPA 1: ANÁLISE DE NEGÓCIO
    ├── Trabalho Final ER - 1 - Análise de Negócio - Clinix.docx  # Documento oficial no formato Word (.docx) pronto para envio
    ├── Trabalho_Final_ER_Etapa_1_Analise_de_Negocio.md           # Versão completa em Markdown para leitura no GitHub / IDE
    ├── Trabalho Final ER - 1 - Análise de Negócio - MODELO.docx  # Modelo de referência fornecido pelo docente
    ├── ER-ProjetoFinal_EngenhariaRequisitos_Orientações.pdf     # Orientações e diretrizes formais do professor
    └── diagramas/                                                # Modelagens BPMN dos Processos de Negócio
        ├── bpmn_as_is.png                                        # Imagem em alta resolução do Processo Atual (AS-IS)
        ├── bpmn_as_is.md                                         # Código Mermaid do Processo Atual
        ├── bpmn_to_be.png                                        # Imagem em alta resolução do Processo Proposto (TO-BE)
        └── bpmn_to_be.md                                         # Código Mermaid do Processo Proposto
```

---

## 📋 Resumo da Etapa 1 — Análise de Negócio

- **Arquivo para Entrega ao Professor:** [`Etapa-1/Trabalho Final ER - 1 - Análise de Negócio - Clinix.docx`](Etapa-1/Trabalho%20Final%20ER%20-%201%20-%20Análise%20de%20Negócio%20-%20Clinix.docx)
- **Visualização Rápida no Repositório:** [`Etapa-1/Trabalho_Final_ER_Etapa_1_Analise_de_Negocio.md`](Etapa-1/Trabalho_Final_ER_Etapa_1_Analise_de_Negocio.md)
- **Seções Contempladas:**
  1. *Análise Institucional e Funcional:* Contexto da organização, áreas de negócio e mapeamento de stakeholders (Amanda Souza Peixoto, secretárias, pacientes, laboratórios e órgãos reguladores).
  2. *Mapeamento do Processo Atual (AS-IS):* Diagnóstico do fluxo manual em caderno/Excel, dependência de WhatsApp manual, no-show de 30% e fragilidade de prontuários.
  3. *Identificação dos Problemas e Necessidades:* Oito problemas prioritários estruturados com causa raiz e impacto no negócio.
  4. *Proposta de Solução:* Visão geral da plataforma Clinix (C# .NET 8, MySQL 8.0, React Web), processo proposto TO-BE, objetivos gerais/específicos, benefícios tangíveis/intangíveis, características da solução, 11 macrofuncionalidades (`RF01–RF34`, `RN01–RN23`) e requisitos de qualidade ISO/IEC 25010 e LGPD (`RQ01–RQ16`).
  5. *Anexos:*
     - Mapas BPMN AS-IS e TO-BE embutidos como imagens e código Mermaid;
     - Protótipos das 9 telas do MVP funcional homologado;
     - Referências normativas (LGPD, CFM, ISO/IEC 25010);
     - Síntese da entrevista com a cliente Amanda Souza Peixoto.
