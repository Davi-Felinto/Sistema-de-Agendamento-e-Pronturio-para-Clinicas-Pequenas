# Diagrama de Arquitetura do Sistema (Camadas)

> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> Estrutura arquitetural em camadas com isolamento de responsabilidades e desacoplamento de persistência.

---

## 1. Visão Geral da Arquitetura

O sistema adota uma arquitetura em camadas concêntricas (inspirada em princípios da *Clean Architecture* e *Onion Architecture*), onde o **Domínio (POO pura)** não conhece detalhes de infraestrutura ou de interface.

```mermaid
flowchart TD
    subgraph UI ["1. Camada de Apresentação (Interface)"]
        WEB["Front-end Web (HTML5 / CSS3 / JavaScript)"]
    end

    subgraph API ["2. Camada de Aplicação / Exposição (C# / .NET)"]
        CTRL["Endpoints REST / Controllers (ASP.NET Core)"]
        DTO["Data Transfer Objects (DTOs)"]
    end

    subgraph SERV ["3. Camada de Serviços / Negócio (Services)"]
        AUTH_S["AuthService (Login, Hash & Sessão)"]
        AG_S["AgendaService (RN01, RN02, RN10)"]
        PR_S["ProntuarioService (RN05, RN06, RN17)"]
        FIN_S["FinanceiroService (RN07, RN08, RN09)"]
        NOT_S["NotificacaoService (RN14, RN15)"]
    end

    subgraph DOMAIN ["4. Camada de Domínio (POO Pura)"]
        ENT["Entidades (Paciente, Agendamento, Sessao, etc.)"]
        ENUMS["Enums (StatusAgendamento, PerfilUsuario, etc.)"]
        IR["Interfaces de Repositório (IPacienteRepository, etc.)"]
        INOT["Interface de Notificação (INotificador)"]
    end

    subgraph INFRA ["5. Camada de Infraestrutura / Persistência"]
        MEM["Fase POO: Repositórios em Memória (List<T> com LINQ)"]
        MYSQL["Fase BD II: Repositórios MySQL (Dapper / SQL)"]
        WHATS["NotificadorWhatsApp (Simulação de Canal)"]
    end

    WEB <-->|"JSON / HTTP REST"| CTRL
    CTRL --> DTO
    CTRL --> SERV
    SERV --> DOMAIN
    SERV --> IR
    SERV --> INOT
    INFRA -.->|Implementa| IR
    INFRA -.->|Implementa| INOT
```

---

## 2. Princípios de Desacoplamento

1. **Agnóstico a Banco de Dados:**  
   Os serviços de aplicação (`Services`) dependem apenas dos contratos (`IRepository<T>`). No momento de POO, persistem em memória via `List<T>`; ao chegar na disciplina de BD II, substitui-se a injeção de dependência para a implementação concreta em **MySQL** sem tocar no Domínio.
2. **Agnóstico a Interface:**  
   O Domínio e as Regras de Negócio não possuem acoplamento com `Console.WriteLine` nem com frameworks de tela, expondo operações puras através de DTOs para o front-end Web.
