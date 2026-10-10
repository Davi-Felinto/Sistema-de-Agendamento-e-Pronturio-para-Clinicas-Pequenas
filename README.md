# 🏥 Clinix — Sistema de Agendamento e Prontuário para Clínicas Pequenas

> **Projeto Integrador Multidisciplinar** — Engenharia de Software (CEUB)  
> Sistema de gestão clínica, agendamento sem conflitos e prontuário eletrônico em conformidade com a LGPD, voltado para profissionais autônomos de saúde e clínicas médicas/terapêuticas.

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?logo=dotnet)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![xUnit](https://img.shields.io/badge/Testes-52%2F52_Passando-success?logo=xunit)
![LGPD](https://img.shields.io/badge/LGPD-Conforme-blue)
![Status](https://img.shields.io/badge/Status-Em_Evolução-brightgreen)

🌐 **Demonstração Web (GitHub Pages):**  
👉 [https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/](https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/)

---

## 🎯 Divisão Multidisciplinar do Projeto Integrador

O projeto conecta os conteúdos práticos de 4 disciplinas do curso de Engenharia de Software do CEUB:

```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│                                 PROJETO INTEGRADOR                                     │
├──────────────────┬──────────────────┬─────────────────────────┬────────────────────────┤
│ 1. Eng. Requisitos│ 2. POO (C# / .NET)│ 3. Banco de Dados II    │ 4. Interface Web (MVP) │
│   [✅ CONCLUÍDO]  │   [✅ CONCLUÍDO]  │   [🚀 EM ANDAMENTO]     │     [✅ CONCLUÍDO]     │
└──────────────────┴──────────────────┴─────────────────────────┴────────────────────────┘
```

| Fase | Disciplina | Foco & Entregáveis | Responsável | Status |
| :--- | :--- | :--- | :---: | :---: |
| **Parte 1** | **Engenharia de Requisitos** | Mapeamento BPMN, Levantamento com cliente, 28 RFs, 17 RNs e 16 RQs (ISO/IEC 25010 e LGPD). | Lucas | ✅ Concluído |
| **Parte 2** | **Programação Orientada a Objetos** | Clean Architecture, DDD pragmático, 52 testes unitários (TDD com xUnit), Serviços de Domínio e API REST. | Davi Felinto | ✅ Concluído |
| **Parte 3** | **Banco de Dados II (MySQL)** | Modelo Lógico (15 tabelas no Workbench), normalização (3FN), DDL, triggers e dados de seed. | Isaac / Davi | 🚀 Em andamento |
| **Parte 4** | **Desenvolvimento de Interface Web** | Interface responsiva, usabilidade em até 3 cliques (RQ10), modo híbrido (API C# + fallback offline). | Miguel / Davi | ✅ Concluído |

---

## 👥 Equipe do Projeto

- **Davi Felinto:** Dev Principal — POO C#/.NET, Arquitetura de Software, Web API e Integração Full-Stack.
- **Miguel:** Desenvolvimento de Interface Web — Telas, Componentes, Design System e Experiência do Usuário (UX).
- **Isaac:** Apoio em Banco de Dados II — Modelagem Relacional e Scripts SQL MySQL.
- **Lucas:** Engenharia de Requisitos — Levantamento de Requisitos, BPMN e Regras de Negócio.

---

## 📐 Arquitetura da Solução

A aplicação adota uma versão pragmática de **Clean Architecture** e **Repository Pattern**, permitindo que o núcleo de domínio permaneça 100% isolado de detalhes de infraestrutura ou do mecanismo de persistência:

```
src/
├── ClinicaApp.slnx                     # Solução .NET 8
├── ClinicaApp/                         # Camada de Domínio e Aplicação
│   ├── Domain/
│   │   ├── Entities/                   # Paciente, Agendamento, SessaoProntuario, Pagamento, etc.
│   │   ├── Enums/                      # StatusAgendamento, PerfilUsuario, FormaPagamento, etc.
│   │   └── Interfaces/                 # IPacienteRepository, IAgendamentoRepository, INotificador, etc.
│   ├── Infrastructure/
│   │   ├── InMemory/                   # Repositórios de persistência em memória thread-safe
│   │   └── External/                   # NotificadorWhatsApp (simulação de mensageria externa)
│   └── Services/                       # Application Services com guardiões das Regras de Negócio
│       ├── AuthService.cs              # RF26, RQ08: Autenticação PBKDF2/SHA-256 + Salt
│       ├── AgendaService.cs            # RN01/RN02: Conflitos; RN10: Cancelamento tardio; RF12: WhatsApp
│       ├── ProntuarioService.cs        # RN05, RN17: Histórico imutável; RN13, RQ07: Auditoria LGPD
│       └── FinanceiroService.cs        # RN07: Cobrança sem duplicidade; RN08/RN09: Quitação e fechamento
├── ClinicaApp.Api/                     # Web API ASP.NET Core (.NET 8)
│   ├── Controllers/                    # Endpoints REST (Pacientes, Agendamentos, Auth, Prontuários, Financeiro)
│   ├── Data/DadosIniciais.cs           # Carga de dados demonstrativos (Seed em memória)
│   └── wwwroot/                        # Interface Web distribuída diretamente com a API
└── ClinicaApp.Tests/                   # Test-Driven Development (TDD) com xUnit
    ├── Domain/                         # 23 testes unitários de regras nas entidades
    └── Services/                       # 29 testes unitários dos fluxos de aplicação e integração
```

---

## 🗄️ Módulo de Banco de Dados Relacional (`bancodedadosclinica/`)

A modelagem lógica desenvolvida no **MySQL Workbench 8.0** conta com **15 tabelas relacionais** estruturadas em 3ª Forma Normal (3FN), com especialização de usuários (Table-per-Type) e integridade referencial:

```
bancodedadosclinica/
├── clínica.mwb            # Modelo lógico oficial no MySQL Workbench (com as 4 melhorias técnicas)
├── clínica_original.mwb   # Backup de segurança da modelagem inicial
├── 01_schema_ddl.sql      # Script DDL com CREATE DATABASE, 15 tabelas, chaves e índices
├── 02_dados_iniciais.sql  # Script DML com carga de dados de demonstração (seed)
└── README.md              # Documentação completa, DER (Mermaid) e dicionário de dados
```

---

## 🧪 Testes Automatizados (TDD com xUnit)

O projeto possui **100% de cobertura de regras de negócio** com suíte automatizada de testes xUnit.

### Executar os Testes Unitários:
```bash
dotnet test src/
```

**Resultado:**
```text
Aprovado!  – Com falha: 0, Aprovado: 52, Ignorado: 0, Total: 52, Duração: 80 ms
```

### Matriz de Requisitos Validados nos Testes:
| Código | Requisito / Regra de Negócio Validada | Localização no Código |
| :--- | :--- | :--- |
| **RN01, RN02** | Bloqueio rigoroso de conflito/colisão de horário entre consultas | `Agendamento.cs`, `AgendaService.cs` |
| **RN03, RF12** | Notificação imediata via WhatsApp na confirmação da consulta | `AgendaService.cs`, `NotificadorWhatsApp.cs` |
| **RN10** | Identificação automática de cancelamento tardio (< 24 horas antes) | `Agendamento.cs`, `AgendaService.cs` |
| **RN16, RQ03** | Inativação lógica com anonimização de dados pessoais (LGPD) | `Paciente.cs`, `PacientesController.cs` |
| **RN17** | Histórico imutável de anotações médicas com justificativa clínica | `SessaoProntuario.cs`, `ProntuarioService.cs` |
| **RN07, RN08** | Cobrança vinculada a agendamento sem duplicidade e controle de quitação | `Pagamento.cs`, `FinanceiroService.cs` |
| **RN13, RQ07** | Trilha imutável de auditoria de acessos sensíveis ao prontuário | `LogAcesso.cs`, `ProntuarioService.cs` |
| **RQ08, RF26** | Autenticação segura com hash de senha e Salt criptográfico | `AuthService.cs`, `AuthController.cs` |

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/) (opcional, para a etapa de banco de dados)

### 1. Rodar a API Integrada com o Front-End:
```bash
# Executa a API e serve a interface gráfica em uma única porta
dotnet run --project src/ClinicaApp.Api --urls "http://localhost:5055"
```
Acesse no navegador:
👉 **Aplicação:** `http://localhost:5055/`  
👉 **Documentação Swagger:** `http://localhost:5055/swagger`

**Credenciais de Acesso de Demonstração:**
- **Profissional (Dr. Davi Felinto):** `davi.profissional@clinix.com` | Senha: `123456`
- **Administrador (Juliana Costa):** `admin@clinix.com` | Senha: `123456`

### 2. Rodar o Banco de Dados (MySQL):
```bash
mysql -u root -p < bancodedadosclinica/01_schema_ddl.sql
mysql -u root -p < bancodedadosclinica/02_dados_iniciais.sql
```

---

## 📂 Estrutura Geral do Repositório

```
├── .github/                               # Workflows de CI/CD (GitHub Pages)
├── bancodedadosclinica/                   # Módulo de Banco de Dados II (MySQL)
│   ├── clínica.mwb                        # Modelo Workbench atualizado
│   ├── 01_schema_ddl.sql                  # Script DDL de criação
│   ├── 02_dados_iniciais.sql              # Carga de dados (seed)
│   └── README.md                          # Documentação técnica do banco
├── docs/                                  # Central de Documentação do Projeto
│   ├── apresentacao/                      # Roteiros e apresentações executivas
│   ├── comercial/                         # Proposta de precificação e viabilidade
│   ├── design/                            # SDD (Software Design Document)
│   ├── diagramas/                         # Diagramas UML, BPMN e Arquitetura
│   ├── requisitos/                        # Documento formal de requisitos
│   └── README.md                          # Índice central da documentação
├── ia-system/                             # Memória Viva e Contexto da Equipe
│   ├── CONTEXTO_MEMORIA_PROJETO.md        # Memória compartilhada do projeto
│   └── membros/                           # Memórias pessoais por integrante (Davi, Miguel)
├── src/                                   # Código-fonte da aplicação
│   ├── ClinicaApp/                        # Domínio, Serviços e Infraestrutura
│   ├── ClinicaApp.Api/                    # ASP.NET Core Web API
│   └── ClinicaApp.Tests/                  # Suíte de Testes xUnit (TDD)
├── api.js                                 # Camada cliente de serviços front-end
├── index.html                             # Aplicação web estática / GitHub Pages
├── Dockerfile                             # Build e deploy conteinerizado
├── ClinicaApp.sln                         # Arquivo de Solução Visual Studio
└── README.md                              # Documentação mestre do projeto
```

---

## 📜 Licença

Projeto desenvolvido para fins estritamente acadêmicos no âmbito do curso de **Engenharia de Software** do **CEUB - Centro Universitário de Brasília**.
