# Sistema de Agendamento e Prontuário para Clínicas Pequenas

Projeto Integrador — Engenharia de Requisitos (com extensão para Programação Orientada a Objetos, Banco de Dados II e Desenvolvimento de Interface).

## Sobre o projeto

Sistema voltado para profissionais autônomos de saúde (psicólogos, nutricionistas, fisioterapeutas, entre outros) que hoje controlam agenda e prontuário de forma manual, por planilha ou WhatsApp. O sistema centraliza:

- Cadastro de pacientes
- Agendamento de consultas, com verificação automática de conflito de horário
- Notificação automática de confirmação e lembrete (WhatsApp)
- Prontuário eletrônico com histórico de versões
- Controle financeiro básico (pagamentos e pendências)
- Autenticação e controle de acesso por perfil (Profissional / Administrador)

## Status atual

- [x] Levantamento e elicitação de requisitos
- [x] Mapeamento do processo de negócio (BPMN)
- [x] Especificação de Requisitos Funcionais, Requisitos de Dados, Regras de Negócio e Requisitos de Qualidade (ISO/IEC 25010)
- [x] Validação dos requisitos com o cliente
- [ ] Modelagem de classes (POO)
- [ ] Modelagem do banco de dados (Banco de Dados II)
- [ ] Protótipo de interface (Desenvolvimento de Interface)

## Estrutura do repositório

```
├── docs/
│   └── requisitos/
│       └── Documento_Especificacao_Requisitos_Final.docx   # Documento de Especificação de Requisitos
├── src/                                                     # Código-fonte (POO, em construção)
└── README.md
```

## Documentação

A especificação completa de requisitos está em [`docs/requisitos/Documento_Especificacao_Requisitos_Final.docx`](docs/requisitos/Documento_Especificacao_Requisitos_Final.docx), contendo:

- Descrição das necessidades do cliente
- Mapeamento do processo "TO-BE" (BPMN)
- Funcionalidades (F1–F7)
- Requisitos Funcionais (RF01–RF28)
- Requisitos de Dados (RD01–RD07)
- Regras de Negócio (RN01–RN17)
- Requisitos de Qualidade (RQ01–RQ16), conforme ISO/IEC 25010
- Escopo futuro (pós-MVP)

## Disciplinas envolvidas

| Disciplina | Entrega |
|---|---|
| Engenharia de Requisitos | Especificação de requisitos (concluída) |
| Programação Orientada a Objetos | Modelagem e implementação das classes do domínio |
| Banco de Dados II | Modelagem e implementação do banco de dados |
| Desenvolvimento de Interface | Protótipo de interface do sistema |

## Autor

Davi Felinto — Engenharia de Software, CEUB
