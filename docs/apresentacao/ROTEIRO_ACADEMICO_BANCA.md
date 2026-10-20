# 🎓 Roteiro Estratégico de Apresentação Acadêmica — Banca CEUB
## Sistema de Agendamento e Prontuário Eletrônico para Clínicas Pequenas (Clinix)

> **Documento de Defesa Acadêmica e Apresentação para Professores**  
> **Autor:** Davi Felinto (Dev Principal / POO & Arquitetura)  
> **Equipe:** Davi Felinto, Lucas Pereira, Dr. Miguel Silva, Isaac Santos, Dra. Vanessa Oliveira, Juliana Costa  
> **Disciplinas Integradas:** Engenharia de Requisitos, POO C#/.NET 8, Banco de Dados II (MySQL) e Interface Web  
> **Tempo estimado de banca:** 10 a 15 minutos  

---

## 1. Visão Geral dos Dois Ambientes de Demonstração (Plano A e B)

Para segurança contra oscilações de rede:

* **Plano A (Local ou Nuvem):**
  * **Nuvem (Produção):** API ASP.NET Core no Render (`https://clinix-api-c58x.onrender.com`) conectada ao MySQL 8.4 no Aiven Cloud.
  * **Local:** `docker compose up -d` (ou `dotnet run --project src/ClinicaApp.Api --urls "http://localhost:5055"`).
  * No navegador em `http://localhost:5055`:
    * Badge 1: `● API .NET C# Conectada` (Verde)
    * Badge 2: `● MySQL 8.0 Ativo` (Azul com pulso dinâmico)
* **Plano B (Rede Offline / Standalone):**
  * O front-end possui fallback gracioso para `localStorage`, permitindo demonstrar todas as telas e regras visuais mesmo se a internet da faculdade oscilar.

---

## 2. Roteiro Passo a Passo na Tela (5 a 7 minutos)

### Passo 1: Login e Segurança (Engenharia de Software + POO)
1. Abrir a tela de login e destacar que **cada membro da equipe possui login próprio** com credenciais de demonstração (`davi@clinix.com`, `lucas@clinix.com`, etc. com senha `123456`).
2. Mostrar o controle de acesso por papel (**RBAC**): Profissional de Saúde vs Administrador.
3. *Argumento técnico:* Senhas com hash criptográfico seguro PBKDF2 com Salt e SHA-256 retrocompatível (**RQ08**), além de proteção contra força bruta por Rate Limiting no endpoint de autenticação.

### Passo 2: Agenda e Bloqueio de Conflitos (RN01, RN02)
1. Mostrar a grade de horários de outubro de 2026.
2. Tentar agendar uma consulta em um horário já ocupado.
3. Mostrar o bloqueio imediato com aviso visual amigável:
   > *"Regra RN01/RN02 atendida: o médico não pode ter duas consultas simultâneas no mesmo horário."*

### Passo 3: Prontuário com Histórico Imutável e LGPD (RN13, RN17, RN16)
1. Abrir a aba de **Prontuário** de um paciente (ex: *Mariana Duarte Souza*).
2. Fazer uma alteração em uma anotação clínica com motivo da alteração.
3. Mostrar a aba de **Versões**: a anotação antiga não foi sobrescrita; ela virou a Versão 1 arquivada com data/hora e autor (**RN17**).
4. Ir na aba de **Auditoria (Logs)**: mostrar que o acesso ao prontuário gerou automaticamente registro imutável para conformidade com a LGPD (**RN13 / RQ07**).

### Passo 4: Banco de Dados Relacional II (MySQL 8.0 / 8.4)
1. Destacar o badge duplo `● MySQL 8.0 Ativo` no cabeçalho.
2. Explicar que o sistema possui **15 tabelas relacionais em 3FN** (`01_schema_ddl.sql`), triggers automáticas de auditoria na tabela `logs_acesso`, views analíticas (`vw_agenda_detalhada`, `vw_resumo_financeiro_mensal`) e stored procedures de fechamento (`03_procedures_views_triggers.sql`).
3. Destacar o uso do micro-ORM **Dapper** para execução de queries de alta performance mapeadas diretamente para as entidades de Domínio.

### Passo 5: Gran Finale — TDD com xUnit no Terminal
1. Abrir o terminal e rodar:
   ```bash
   dotnet test src/
   ```
2. Mostrar o resultado: **56 testes unitários passando com 100% de sucesso**.
3. Isso prova a cobertura de testes em entidades, serviços de aplicação e segurança.

---

## 3. Respostas Técnicas para Perguntas da Banca

| Pergunta Provável | Resposta Firme e Técnica |
|---|---|
| **Como garantiram que dois agendamentos não entrem no mesmo horário?** | *"Validação em duas camadas: no C# (`AgendaService.cs` com verificação de interseção de intervalos) e no MySQL com transações relacionais via Dapper."* |
| **Por que Dapper e não Entity Framework Core?** | *"Por performance e alinhamento com a disciplina de Banco de Dados II: o Dapper nos dá controle direto sobre os comandos SQL, permitindo exercitar views, joins complexos e triggers sem abstração excessiva."* |
| **Como o sistema atende à LGPD?** | *"Não deletamos prontuários físicos por determinação legal do CFM. Em caso de desligamento, os dados pessoais são anonimizados (`RN16`) e qualquer leitura/edição de prontuário gera log obrigatório de auditoria (`RN13 / RQ07`)."* |
| **Qual padrão arquitetural foi adotado?** | *"Clean Architecture simplificada com Repository Pattern: o domínio é isolado em `ClinicaApp` e não depende de banco. Alternamos entre MySQL e Memória apenas na injeção de dependência do `Program.cs`."* |
