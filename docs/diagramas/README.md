# Diagrama de Classes — Fase POO (C#)

> Sistema de Agendamento e Prontuário para Clínicas Pequenas  
> Modelagem Orientada a Objetos referente aos requisitos especificados em [`Documento_Especificacao_Requisitos_Final.docx`](../requisitos/Documento_Especificacao_Requisitos_Final.docx).

---

## 1. Visualização do Diagrama

![Diagrama de Classes](diagrama_classes.svg)

> **Dica de Edição:** O arquivo [`diagrama_classes.excalidraw`](diagrama_classes.excalidraw) pode ser aberto e editado diretamente no [Excalidraw](https://excalidraw.com) ou através da extensão *Excalidraw* no VS Code.

---

## 2. Visão Arquitetural das Classes

| Pacote / Domínio | Classe / Interface | Responsabilidade Principal | Requisitos Associados |
| :--- | :--- | :--- | :--- |
| **Segurança & Acesso** | `Usuario` *(abstract)* | Modelo base com ID, nome, login e autenticação com hash. | RF26, RQ08 |
| | `ProfissionalSaude` | Usuário com registro profissional e escopo restrito à sua própria agenda. | RF27, RN12 |
| | `Administrador` | Usuário com privilégios para relatórios consolidados e auditoria geral. | RF27, RN12 |
| | `LogAcesso` | Registro imutável de acessos e operações sensíveis de prontuário (LGPD). | RF28, RN13, RQ07 |
| **Paciente** | `Paciente` | Dados cadastrais, condições preexistentes, alergias e inativação com anonimização. | RF01–RF05, RN16, RQ03 |
| **Agendamento** | `Agendamento` | Gerencia horários, verificação de conflitos e cancelamentos tardios (< 24h). | RF06–RF11, RN01, RN02, RN10 |
| **Prontuário** | `SessaoProntuario` | Anotações clínicas do atendimento realizadas pelo profissional. | RF15, RF17, RN05, RN06 |
| | `VersaoAnotacao` | Histórico de versões imutável para cada alteração na anotação de prontuário. | RF16, RN17 |
| **Financeiro** | `Pagamento` | Controle de valor, forma de quitação e status (pago/pendente). | RF19–RF22, RN07, RN08 |
| **Notificação** | `Notificacao` | Controle do ciclo de vida de envios de confirmação e lembretes (24h). | RF12–RF14, RN03, RN04 |
| | `INotificador` | Interface de contrato para disparo de mensagens desacoplada do canal. | RF12, RF13 |
| | `NotificadorWhatsApp` | Implementação do serviço de notificação via canal WhatsApp. | RF12, RF13, RN14, RN15 |

---

## 3. Decisões de Modelagem e Regras de Negócio Implementadas

1. **Exclusão Lógica e LGPD (`RN16`, `RQ03`):**
   * O método `InativarComAnonimizacao()` na classe `Paciente` seta `Ativo = false` e limpa/mascara campos pessoais identificadores não essenciais (`Telefone`, `Email`, `Endereco`), garantindo a preservação legal do histórico clínico do prontuário.
2. **Prevenção de Conflito de Horários (`RN01`, `RN02`):**
   * A classe `Agendamento` conta com `TemConflito(DateTime inicio, DateTime fim)` para impedir a sobreposição de horários antes de chamar `Confirmar()`.
3. **Cancelamento Tardio (`RN10`):**
   * No método `Cancelar(DateTime momentoCancelamento)`, o sistema calcula o intervalo até a consulta. Se for menor que 24 horas, ativa a flag `CancelamentoTardio = true`.
4. **Imutabilidade e Auditoria de Prontuário (`RN17`):**
   * Toda edição em `SessaoProntuario.AlterarAnotacao(novoTexto, motivo)` cria uma instância de `VersaoAnotacao` contendo o snapshot anterior, timestamp e justificativa antes de atualizar o texto corrente.
5. **Resiliência de Notificações (`RN14`, `RN15`):**
   * A classe `Notificacao` gerencia a propriedade `TentativasEnvio`. Caso ocorra falha, o método `PodeReenviar()` autoriza apenas 1 nova tentativa após 15 minutos; se persistir a falha, sinaliza para exibição de alerta visual na agenda.
