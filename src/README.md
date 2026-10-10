# 💻 Backend C# (.NET 8) — Arquitetura e Código-Fonte (`src/`)

> **Projeto Integrador Multidisciplinar — Engenharia de Software (CEUB)**  
> Módulos de Programação Orientada a Objetos (POO) e Persistência Relacional (Banco de Dados II - MySQL)

---

## 🏗️ Visão Geral da Solução (`ClinicaApp.slnx`)

A solução backend foi desenvolvida em **C# 12 / .NET 8**, organizada sob os princípios de **Clean Architecture** pragmática, **Domain-Driven Design (DDD)** e **Repository Pattern**. O código é dividido em três projetos principais com responsabilidades desacopladas:

```
src/
├── ClinicaApp.slnx                     # Arquivo de Solução moderno (.NET 8 / 9)
│
├── ClinicaApp/                         # Core: Domínio, Regras de Negócio e Repositórios
│   ├── Domain/
│   │   ├── Entities/                   # Entidades ricas com encapsulamento e validação de invariantes
│   │   ├── Enums/                      # Enums de status, perfis e formas de pagamento
│   │   └── Interfaces/                 # Contratos de repositórios e serviços externos
│   ├── Infrastructure/
│   │   ├── InMemory/                   # Persistência em memória thread-safe (Desenvolvimento & Testes)
│   │   ├── MySQL/                      # Repositórios relacionais com Dapper e MySqlConnector
│   │   └── External/                   # Adaptadores de serviços externos (ex: NotificadorWhatsApp)
│   └── Services/                       # Application Services com guardiões das Regras de Negócio
│       ├── AuthService.cs              # RF26, RQ08: Autenticação segura PBKDF2/SHA-256 + Salt
│       ├── AgendaService.cs            # RN01/RN02: Conflitos; RN10: Cancelamento tardio; RF12: Notificação
│       ├── ProntuarioService.cs        # RN05, RN17: Histórico imutável; RN13, RQ07: Auditoria LGPD
│       └── FinanceiroService.cs        # RN07: Cobrança sem duplicidade; RN08/RN09: Quitação e fechamento
│
├── ClinicaApp.Api/                     # Apresentação: ASP.NET Core Web API & Interface Web
│   ├── Controllers/                    # Endpoints REST (Pacientes, Agendamentos, Auth, Prontuários, Financeiro)
│   ├── Data/DadosIniciais.cs           # Seed de dados de demonstração em memória
│   ├── Program.cs                      # Injeção de dependência híbrida (MySQL ou InMemory) e middlewares
│   ├── appsettings.Development.json    # Configurações de logging e template de conexão
│   └── wwwroot/                        # Interface Web completa (HTML/Tailwind/React) distribuída com a API
│
└── ClinicaApp.Tests/                   # Garantia de Qualidade: TDD com xUnit
    ├── Domain/                         # 23 testes unitários focados nas entidades de domínio
    └── Services/                       # 29 testes unitários dos fluxos de regras de negócio
```

---

## 🏛️ Padrões Arquiteturais Adotados

### 1. Domain-Driven Design (DDD) Pragmático
- **Entidades Ricas:** As propriedades possuem `private set` para impedir mutações indevidas fora do ciclo de vida dos métodos da entidade (ex: `Paciente.InativarComAnonimizacao()`, `Agendamento.Confirmar()`).
- **Validação de Invariantes:** Construtores disparam `ArgumentException` caso dados obrigatórios sejam nulos ou vazios (ex: CPF/RG, conflito de datas de início/fim).
- **Sem Anemia de Domínio:** A lógica de negócio reside dentro dos métodos das entidades e nos serviços de aplicação.

### 2. Inversão de Dependência & Repository Pattern
A camada de aplicação (`Services`) comunica-se exclusivamente com **interfaces** (`IPacienteRepository`, `IAgendamentoRepository`, etc.), sem conhecer a tecnologia de banco de dados subjacente:
- **`InMemory` (`ClinicaApp.Infrastructure.InMemory`):** Implementação baseada em `ConcurrentDictionary` e coleções thread-safe. Permite executar o sistema sem infraestrutura externa.
- **`MySQL` (`ClinicaApp.Infrastructure.MySQL`):** Implementação relacional de alta performance utilizando **Dapper** e **MySqlConnector**, mapeando diretamente para o modelo lógico de 15 tabelas.

### 3. Injeção de Dependência Híbrida (`Program.cs`)
A API detecta automaticamente o ambiente de execução:
- Se a connection string `ClinixDb` estiver configurada no cofre de segredos (`dotnet user-secrets`) ou variável de ambiente, registra os repositórios **MySQL** (`Scoped`).
- Se a connection string não estiver configurada (ou contiver o placeholder `"sua_senha"`), faz fallback gracioso para os repositórios **InMemory** (`Singleton`).

---

## 🔐 Gerenciamento Seguro de Credenciais (`UserSecrets`)

Para evitar o vazamento acidental de senhas no Git, o projeto utiliza o **Secret Manager** nativo do .NET SDK:

### Configurar a senha local do MySQL:
```bash
cd src/ClinicaApp.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ClinixDb" "Server=localhost;Port=3306;Database=clinix_db;Uid=root;Pwd=SUA_SENHA_AQUI;"
```

Para inspecionar as chaves salvas localmente:
```bash
dotnet user-secrets list
```

---

## 🧪 Execução de Testes Automatizados (xUnit)

Toda a regra de negócio do sistema é coberta por testes automatizados sem necessidade de banco de dados ativo:

```bash
# Executar todos os 52 testes unitários
dotnet test src/
```

### Sumário da Suíte de Testes:
```text
Aprovado!  – Com falha: 0, Aprovado: 52, Ignorado: 0, Total: 52 (100% de sucesso)
```

---

## 🚀 Execução da API Localmente

```bash
# Executa a API e serve o frontend na porta 5055
dotnet run --project src/ClinicaApp.Api --urls "http://localhost:5055"
```

- **Interface Gráfica da Clínica:** `http://localhost:5055/`
- **Documentação Interativa (Swagger):** `http://localhost:5055/swagger`
