# Relatório de Segurança — Davi-Felinto/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas

**Scan:** `cmvldq1bi04cnjxxflcxadvhf` · MANUAL · branch `main` · commit `97f4ba2a8438`
**Status:** COMPLETED · **Executado em:** 2026-10-23T19:45:25.571Z · **Concluído em:** 2026-10-23T19:47:53.401Z
**Relatório gerado em:** 2026-10-23T19:48:38.138Z por GitGuard

## Instruções para a IA que for corrigir isto

- Repositório alvo: Davi-Felinto/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas, branch "main", commit 97f4ba2a84389daedb11176249ea9366be1d8369. Aplique as correções diretamente nesse checkout.
- Em "dependencyUpgrades", cada entrada agrupa TODOS os CVEs de um mesmo pacote — faça UM upgrade por pacote (para "recommendedVersion" ou mais recente), não uma correção por CVE.
- Em "secrets", nunca tente adivinhar ou reconstruir o valor original do segredo (ele foi propositalmente redigido) — apenas remova/rotacione conforme "remediation".
- Depois de aplicar as correções, rode os testes existentes do projeto e, se disponível, o linter/build antes de considerar concluído.

## Resumo

- **Total de findings:** 13
- **Por severidade:** MEDIUM: 13
- **Por scanner:** SEMGREP: 13

## Outros findings

| Severidade | Scanner | Categoria | Título | Local |
|---|---|---|---|---|
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.java.spring.security.audit.spring-actuator-non-health-enabled.spring-actuator-dangerous-endpoints-enabled | /scan/.agents/skills/security-review/references/misconfiguration.md:221 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.html.security.audit.missing-integrity.missing-integrity | /scan/index.html:9 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/index.html:3024 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/index.html:3208 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/index.html:5568 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.csharp.dotnet.security.mvc-missing-antiforgery.mvc-missing-antiforgery | /scan/src/ClinicaApp.Api/Controllers/AgendamentosController.cs:53 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.csharp.dotnet.security.mvc-missing-antiforgery.mvc-missing-antiforgery | /scan/src/ClinicaApp.Api/Controllers/AvaliacoesController.cs:65 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.csharp.dotnet.security.mvc-missing-antiforgery.mvc-missing-antiforgery | /scan/src/ClinicaApp.Api/Controllers/ExamesLaboratorioController.cs:66 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.csharp.dotnet.security.mvc-missing-antiforgery.mvc-missing-antiforgery | /scan/src/ClinicaApp.Api/Controllers/ProntuariosController.cs:33 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.html.security.audit.missing-integrity.missing-integrity | /scan/src/ClinicaApp.Api/wwwroot/index.html:9 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/src/ClinicaApp.Api/wwwroot/index.html:3024 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/src/ClinicaApp.Api/wwwroot/index.html:3208 |
| MEDIUM | SEMGREP | SAST | Semgrep Finding: rules.generic.html-templates.security.unquoted-attribute-var.unquoted-attribute-var | /scan/src/ClinicaApp.Api/wwwroot/index.html:5568 |
