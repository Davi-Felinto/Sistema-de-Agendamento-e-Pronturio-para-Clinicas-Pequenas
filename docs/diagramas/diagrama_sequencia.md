# Diagramas de Sequência UML (Fluxos Críticos)

> **Sistema de Agendamento e Prontuário para Clínicas Pequenas**  
> Documento visual complementar referente aos fluxos temporais de troca de mensagens entre objetos.

---

## 1. Fluxo Crítico 1: Agendamento de Consulta com Verificação de Conflito e Notificação

* **Requisitos e Regras Envolvidos:** RF06, RF09, RF12, RF13, RN01, RN02, RN03, RN04, RQ12.
* **Objetivo:** Garantir que nenhuma consulta seja agendada em sobreposição de horários e disparar a confirmação automática via WhatsApp.

```mermaid
sequenceDiagram
    autonumber
    actor Atend as "Profissional / Atendente"
    participant UI as "Camada UI / Controller"
    participant Serv as "AgendaService"
    participant Repo as "IAgendamentoRepository"
    participant Model as "Agendamento (Entidade)"
    participant Notif as "INotificador (WhatsApp)"

    Atend->>UI: Solicitar Agendamento (pacienteId, profId, dataHora)
    UI->>Serv: AgendarConsulta(dto)
    Serv->>Repo: ObterPorProfissionalEPeriodo(profId, data)
    Repo-->>Serv: agendamentosExistentes

    loop Para cada agendamento ativo no dia
        Serv->>Model: TemConflito(inicio, fim)
        Model-->>Serv: bool temConflito
    end

    alt Conflito Detectado (RN01, RN02)
        Serv-->>UI: Lança RegraNegocioException ("Horário indisponível")
        UI-->>Atend: Alerta de conflito e sugestão de novo horário
    else Horário Disponível
        Serv->>Model: new Agendamento(pacienteId, profId, inicio, fim)
        Serv->>Repo: Adicionar(novoAgendamento)
        Repo-->>Serv: Agendamento persistido com sucesso

        Serv->>Notif: EnviarNotificacao(notificacao, telefone, mensagem)
        Notif-->>Serv: Status do envio (Sucesso / Falha)

        Serv-->>UI: Retorna AgendamentoConfirmadoDTO
        UI-->>Atend: Exibe confirmação com sucesso
    end
```

---

## 2. Fluxo Crítico 2: Atendimento Clínico e Alteração de Prontuário com Histórico Imutável

* **Requisitos e Regras Envolvidos:** RF15, RF16, RF28, RN05, RN06, RN13, RN17, RQ07 (LGPD).
* **Objetivo:** Registrar o atendimento do paciente, permitir correções de anotações preservando versões anteriores e registrar log de auditoria.

```mermaid
sequenceDiagram
    autonumber
    actor Med as "Profissional de Saúde"
    participant UI as "Camada UI / Controller"
    participant Serv as "ProntuarioService"
    participant Repo as "IProntuarioRepository"
    participant Sessao as "SessaoProntuario (Entidade)"
    participant Versao as "VersaoAnotacao (Snapshot)"
    participant Log as "ILogRepository (Auditoria LGPD)"

    Med->>UI: Editar Anotação Clínica (sessaoId, novoTexto, motivo)
    UI->>Serv: AtualizarAnotacao(sessaoId, novoTexto, motivo, usuarioId)
    Serv->>Repo: ObterPorId(sessaoId)
    Repo-->>Serv: sessaoExistente

    Serv->>Sessao: AlterarAnotacao(novoTexto, motivo)
    create participant Versao
    Sessao->>Versao: new VersaoAnotacao(textoAnterior, DateTime.Now, motivo)
    Sessao->>Sessao: HistoricoVersoes.Add(versao)
    Sessao->>Sessao: AnotacoesClinicas = novoTexto

    Serv->>Repo: Atualizar(sessaoExistente)
    Repo-->>Serv: Prontuário persistido com versão arquivada

    Serv->>Log: RegistrarLog(usuarioId, "EDICAO_PRONTUARIO", sessaoId)
    Log-->>Serv: Log gravado com sucesso

    Serv-->>UI: Retorna SessaoAtualizadaDTO
    UI-->>Med: Confirmação de edição com integridade preservada
```
