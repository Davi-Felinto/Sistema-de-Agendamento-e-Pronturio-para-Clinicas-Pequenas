# 🏥 Clinix — Sistema de Agendamento e Prontuário para Clínicas Pequenas

> **Projeto Integrador Multidisciplinar** — Engenharia de Software (CEUB)  
> Sistema de gestão clínica, agendamento sem conflitos e prontuário eletrônico em conformidade com a LGPD, voltado para profissionais autônomos de saúde e clínicas médicas/terapêuticas.

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?logo=dotnet)
![MySQL](https://img.shields.io/badge/MySQL-8.0%20%2F%208.4-4479A1?logo=mysql&logoColor=white)
![xUnit](https://img.shields.io/badge/Testes-66%2F66_Passando-success?logo=xunit)
![LGPD](https://img.shields.io/badge/LGPD-Conforme-blue)
![Segurança OWASP](https://img.shields.io/badge/Segurança-Auditado_A%2B-success)

🌐 **Demonstração Web (GitHub Pages):**  
👉 [https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/](https://davi-felinto.github.io/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/)

⚡ **API REST & Swagger na Nuvem (Render.com):**  
👉 [https://clinix-api-c58x.onrender.com/swagger](https://clinix-api-c58x.onrender.com/swagger)

🗄️ **Banco de Dados Relacional na Nuvem (Aiven Cloud):**  
👉 Cluster MySQL 8.4 gerenciado ativo com 15 tabelas, views analíticas, triggers de auditoria LGPD, procedures e dados populados.

---

## 🎯 Divisão Multidisciplinar do Projeto Integrador

O projeto conecta os conteúdos práticos de 4 disciplinas do curso de Engenharia de Software:
```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│                                 PROJETO INTEGRADOR                                     │
├──────────────────┬──────────────────┬─────────────────────────┬────────────────────────┤
│ 1. Eng. Requisitos│ 2. POO (C# / .NET)│ 3. Banco de Dados II    │ 4. Interface Web (MVP) │
│   [✅ CONCLUÍDO]  │   [✅ CONCLUÍDO]  │     [✅ CONCLUÍDO]      │     [✅ CONCLUÍDO]     │
└──────────────────┴──────────────────┴─────────────────────────┴────────────────────────┘
```

| Fase | Disciplina | Foco & Entregáveis | Responsável | Status |
| :--- | :--- | :--- | :--- | :---: |
| **Parte 1** | **Engenharia de Requisitos** | Etapa 1 (Análise de Negócio) oficial entregue, modelagem BPMN AS-IS/TO-BE, 34 RFs, 23 RNs e 16 RQs ([Ver Especificação](docs/requisitos/README.md)). | Lucas / Davi | ✅ Concluído |
| **Parte 2** | **Programação Orientada a Objetos** | Clean Architecture, DDD, 66 testes unitários xUnit com 100% de sucesso ([Ver Detalhes](src/README.md)). | Davi Felinto | ✅ Concluído |
| **Parte 3** | **Banco de Dados II (MySQL)** | 15 tabelas, 3FN, DDL/DML, Dapper, Views/Triggers/Procedures na nuvem Aiven e Docker ([Ver Banco](bancodedadosclinica/README.md)). | Isaac / Davi | ✅ Concluído |
| **Parte 4** | **Desenvolvimento de Interface Web** | Interface responsiva, usabilidade em até 3 cliques (RQ10), modo híbrido (Offline / API Nuvem / Docker). | Miguel / Davi | ✅ Concluído |

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
│   │   ├── Entities/                   # Paciente, Agendamento, SessaoProntuario, Pagamento, AvaliacaoAtendimento, PedidoExameLaboratorio, etc.
│   │   ├── Enums/                      # StatusAgendamento, PerfilUsuario, FormaPagamento, MotivoCancelamento, StatusExameLaboratorio, etc.
│   │   └── Interfaces/                 # IPacienteRepository, IAgendamentoRepository, INotificador, IExameLaboratorioRepository, etc.
│   ├── Infrastructure/
│   │   ├── InMemory/                   # Repositórios de persistência em memória thread-safe
│   │   ├── MySQL/                      # Repositórios relacionais com Dapper e MySqlConnector
│   │   └── External/                   # NotificadorWhatsApp (simulação de mensageria externa)
│   └── Services/                       # Application Services com guardiões das Regras de Negócio
│       ├── AuthService.cs              # RF26, RQ08: Autenticação PBKDF2/SHA-256 + Salt (atalho isolado em #if DEBUG)
│       ├── AgendaService.cs            # RN01/RN02: Conflitos; RN10: Cancelamento tardio; RF12: WhatsApp; RF31/RN20: Motivos
│       ├── ProntuarioService.cs        # RN05, RN17: Histórico imutável; RN13, RQ07: Auditoria LGPD
│       └── FinanceiroService.cs        # RN07: Cobrança; RN08/RN09: Quitação; RF30/RN19: Pagamento dividido
├── ClinicaApp.Api/                     # Web API ASP.NET Core (.NET 8)
│   ├── Controllers/                    # Endpoints REST (Pacientes, Agendamentos, Auth, Prontuários, Financeiro, Cep, Avaliações, Exames, Status)
│   ├── Data/DadosIniciais.cs           # Carga de dados demonstrativos (Seed em memória)
│   ├── Program.cs                      # Injeção de dependência híbrida (MySQL ou InMemory), Rate Limiting, CSP e Middlewares
│   └── wwwroot/                        # Interface Web distribuída diretamente com a API
└── ClinicaApp.Tests/                   # Test-Driven Development (TDD) com xUnit (66 Testes / 100% Sucesso)
    ├── Domain/                         # 32 testes unitários de regras nas entidades
    └── Services/                       # 34 testes unitários dos fluxos de aplicação e integração
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
Aprovado!  – Com falha: 0, Aprovado: 66, Ignorado: 0, Total: 66, Duração: 430 ms
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
| **RQ08, RF26** | Autenticação segura com hash de senha e Salt criptográfico (PBKDF2) | `AuthService.cs`, `AuthController.cs` |
| **RF29, RN18** | Validação estruturada de CEP e endereço completo via integração | `Paciente.cs`, `CepController.cs` |
| **RF30, RN19** | Pagamento dividido com verificação de igualdade da soma ao valor total | `Pagamento.cs`, `FinanceiroService.cs` |
| **RF31, RN20** | Registro obrigatório de categoria e justificativa ao cancelar consulta | `Agendamento.cs`, `AgendaService.cs` |
| **RF32, RN21** | Ouvidoria clínica com notas 1 a 5 e suporte a depoimentos anônimos | `AvaliacaoAtendimento.cs` |
| **RF34, RN23** | Ciclo de vida e liberação de laudo de exames laboratoriais externos | `PedidoExameLaboratorio.cs` |

---

## 🚀 Como Executar o Projeto Localmente

### Opção A: Execução em 1 Clique com Docker Compose (Recomendado)
Sobe o **MySQL 8.0** já com as 15 tabelas criadas (`01_schema_ddl.sql`), dados de demonstração populados (`02_dados_iniciais.sql`) e a **API .NET** conectada:

```bash
docker compose up -d
```
Acesse no navegador:
- 👉 **Aplicação Web:** `http://localhost:5055/`
- 👉 **Swagger API:** `http://localhost:5055/swagger`

---

### Opção B: Execução com .NET SDK Local

#### 1. Pré-requisitos:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/) (opcional — caso não tenha, a API faz fallback automático em memória)

#### 2. Configurar Segredo do Banco (Opcional - Se usar MySQL local):
Para proteger sua senha de banco e não commitar dados sensíveis:
```bash
cd src/ClinicaApp.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ClinixDb" "Server=localhost;Port=3306;Database=clinix_db;Uid=root;Pwd=SUA_SENHA_AQUI;"
```

#### 3. Rodar a API:

**Com Banco de Dados MySQL 8.0 (Persistência Relacional):**
```bash
dotnet run --project src/ClinicaApp.Api --urls "http://localhost:5055"
```

**Sem Banco de Dados / Modo Em Memória (Mock / Fallback):**
> Ideal para quem não tem MySQL instalado ou deseja testar de forma rápida sem configurar banco.
```bash
dotnet run --project src/ClinicaApp.Api --urls "http://localhost:5055" --in-memory
```

Acesse no navegador: **`http://localhost:5055/`**

#### 4. Indicadores de Status em Tempo Real no Header:
A interface web identifica dinamicamente o estado da arquitetura através do endpoint `GET /api/status`:
- 🟢 **`● API .NET C# Conectada`**: Backend C# ativo respondendo via HTTP/REST.
- 🔵 **`● MySQL 8.0 Ativo`**: Repositórios relacionais com Dapper ativos gravando no banco.
- 🟣 **`● Memória (Mock)`**: Repositórios InMemory ativos com dados pré-populados pelo `DadosIniciais.cs`.
- 🟡 **`● Modo Standalone (Offline)`**: Execução estática/offline (GitHub Pages) com persistência em `localStorage`.

**Credenciais de Acesso de Demonstração:**
- **Profissional (Dr. Davi Felinto):** `davi.profissional@clinix.com` | Senha: `123456` *(ou `123` em ambiente Debug)*
- **Administrador (Juliana Costa):** `admin@clinix.com` | Senha: `123456` *(ou `123` em ambiente Debug)*

---

## 📂 Estrutura Geral do Repositório

```
├── .github/                               # Workflows de CI/CD (GitHub Pages)
├── bancodedadosclinica/                   # Módulo de Banco de Dados II (MySQL 8.0/8.4)
│   ├── clínica.mwb                        # Modelo Workbench atualizado (15 tabelas)
│   ├── 01_schema_ddl.sql                  # Script DDL de criação
│   ├── 02_dados_iniciais.sql              # Carga de dados (seed de outubro/2026)
│   ├── 03_procedures_views_triggers.sql  # Views, Triggers LGPD e Stored Procedures
│   └── README.md                          # Documentação técnica, DER e dicionário de dados
├── docs/                                  # Central de Documentação do Projeto
│   ├── apresentacao/                      # Roteiros e apresentações acadêmicas e comerciais
│   ├── comercial/                         # Contrato oficial de prestação de serviços (.docx e .md) e precificação
│   ├── design/                            # SDD (Software Design Document - Versão 2.5)
│   ├── diagramas/                         # Diagramas UML, BPMN e Arquitetura
│   ├── Entregas-Eng.-de-Requisitos/       # Entregas oficiais da disciplina de ER (Etapa 1: Análise de Negócio)
│   ├── guias/                             # Guias passo a passo (ex: GUIA_DOCKER_MIGUEL.md)
│   ├── requisitos/                        # Especificação completa de requisitos (RF01–RF34, RN01–RN23, RQ01–RQ16)
│   ├── seguranca/                         # Relatório Oficial de Auditoria e Hardening (Score A+ / OWASP)
│   └── README.md                          # Índice central da documentação
├── ia-system/                             # Memória Viva e Contexto da Equipe
│   ├── CONTEXTO_MEMORIA_PROJETO.md        # Memória compartilhada do projeto
│   └── membros/                           # Memórias pessoais por integrante (Davi, Miguel)
├── src/                                   # Código-fonte da aplicação
│   ├── ClinicaApp/                        # Domínio, Serviços e Repositórios Dapper
│   ├── ClinicaApp.Api/                    # ASP.NET Core Web API com DI Híbrida
│   ├── ClinicaApp.Tests/                  # Suíte de Testes xUnit (TDD)
│   └── README.md                          # Guia detalhado da arquitetura backend
├── docker-compose.yml                     # Orquestração MySQL 8.0 + API .NET
├── Dockerfile                             # Build e deploy conteinerizado
├── api.js                                 # Camada cliente de serviços front-end
├── index.html                             # Aplicação web estática / GitHub Pages
├── ClinicaApp.sln                         # Arquivo de Solução Visual Studio
└── README.md                              # Documentação mestre do projeto
```

---

## 📜 Licença

Projeto desenvolvido para fins estritamente acadêmicos no âmbito do curso de **Engenharia de Software** do **CEUB - Centro Universitário de Brasília**.
