# Levantamento de Custos de Infraestrutura e Manutenção (TCO)
## Sistema Clinix — Gestão, Agendamento e Prontuário para Clínicas Pequenas

> **Documento de Engenharia Econômica, Dimensionamento de Servidores e Planejamento Financeiro**  
> **Autor:** Davi Felinto — Engenharia de Software (CEUB)  
> **Data:** Outubro / 2026 — Versão: 1.0 (Produção e Mercado)  

---

## 1. Sumário Executivo: Quanto Custa Manter o Sistema no Ar?

O **Clinix** foi concebido com uma arquitetura moderna e de alta eficiência (.NET 8 no backend, banco relacional MySQL 8 otimizado com Dapper e Single Page Application estática no frontend). Graças a esse design arquitetural, o consumo de recursos computacionais é extremamente reduzido.

Para manter o sistema no ar em ambiente de produção real, 24 horas por dia, 7 dias por semana, com segurança (HTTPS/SSL), backups diários e conformidade com a LGPD:

| Cenário de Hospedagem | Custo Mensal Estimado | Recomendação de Uso | Margem na Mensalidade (R$ 179) |
| :--- | :---: | :--- | :---: |
| **Cenário A: "Custo Quase Zero" (Piloto / Free Tiers)** | **R$ 3,33 a R$ 15,00 / mês** | Testes, demonstrações e primeiros 30 dias de validação com a cliente. | **> 92% de margem** |
| **Cenário B: "VPS Econômica Profissional" (RECOMENDADO)** | **R$ 35,00 a R$ 50,00 / mês** | **Padrão comercial recomendado:** Suporta de 1 a 5 clínicas na mesma máquina com Docker. | **~78% a 85% de margem** |
| **Cenário C: "PaaS Gerenciado / No-Ops" (Render + Aiven)** | **R$ 90,00 a R$ 130,00 / mês** | Sem gerenciamento de Linux/Docker (banco gerenciado + container em nuvem). | **~40% a 50% de margem** |
| **Cenário D: "Enterprise / Big Cloud" (AWS / Azure)** | **R$ 180,00 a R$ 280,00 / mês** | Clínicas de grande porte, franquias hospitalares ou redes de policlínicas. | Inviável para 1 clínica pequena |

> 💡 **Conclusão Imediata:** No **Cenário B (Recomendado)**, com um custo fixo de apenas **~R$ 40,00/mês**, a mensalidade de **R$ 179,00** cobrada da primeira clínica cobre todas as despesas da infraestrutura e gera **R$ 139,00 de lucro líquido recorrente mensal** logo no primeiro cliente.

---

## 2. Dimensionamento e Carga de Trabalho (Workload)

Para dimensionar os custos reais de servidores, é necessário estimar a carga real de trabalho de uma clínica médica/odontológica pequena:

* **Equipe por clínica:** 1 a 3 profissionais de saúde (médicos/dentistas/psicólogos) + 1 a 2 recepcionistas;
* **Volume de consultas:** 15 a 40 atendimentos por dia (~400 a 1.000 consultas/mês);
* **Requisições à API:** 500 a 2.500 requisições HTTP por dia (menos de 1 requisição por segundo em média);
* **Crescimento do Banco de Dados:** O registro textual de 1.000 prontuários, consultas e pacientes consome aproximadamente **15 MB a 40 MB de disco por ano**;
* **Consumo de Memória RAM da API C# (.NET 8):** ~80 MB a 140 MB de memória em regime estável;
* **Consumo de Memória do MySQL 8:** ~150 MB a 350 MB de memória com buffer pool calibrado.

```
Consumo Total Estimado da Solução Completa:
┌────────────────────────────────────────────────────────┐
│ CPU: Menos de 15% de 1 vCPU moderna                    │
│ Memória RAM Total: ~450 MB a 700 MB                    │
│ Armazenamento em Disco: 5 GB a 10 GB (sobra por anos)  │
│ Tráfego de Rede: Menos de 20 GB / mês                  │
└────────────────────────────────────────────────────────┘
```

---

## 3. Detalhamento por Componente do Sistema

### 3.1 Frontend Web (Interface do Usuário)
* **Arquitetura:** SPA estática (HTML5, Vanilla CSS3, JavaScript ES6+).
* **Onde hospedar:** Cloudflare Pages, Vercel, Netlify ou GitHub Pages.
* **Custo:** **R$ 0,00 (Gratuito)**.
* **Benefícios:** CDN global com servidores no Brasil (São Paulo/Rio), cache em borda, proteção contra ataques DDoS e certificado SSL gratuito.

### 3.2 Backend Web API (ASP.NET Core .NET 8)
* **Arquitetura:** Container Docker Linux (Kestrel).
* **Opções de Hospedagem:**
  * *VPS Linux (Docker Compose)*: Custo compartilhado com o banco (~R$ 25 a R$ 40/mês).
  * *Render.com (Web Service)*: $7,00/mês (~R$ 39,00/mês).
  * *Railway.app*: ~$5,00/mês (~R$ 28,00/mês).

### 3.3 Banco de Dados Relacional (MySQL 8)
* **Requisitos:** ACID, Procedures, Triggers de auditoria e Views analíticas.
* **Opções de Hospedagem:**
  * *Na mesma VPS via Docker*: **R$ 0,00 extra** (incluído na VPS).
  * *Aiven Cloud / DigitalOcean Managed DB*: $15,00/mês (~R$ 85,00/mês) — caso opte por não gerenciar o banco.

### 3.4 Domínio Próprio e Certificados SSL/TLS
* **Registro de Domínio (`.com.br`):** Registro.br por **R$ 40,00 / ano** = **R$ 3,33 / mês**.
* **Certificado SSL/TLS (HTTPS):** Let's Encrypt ou Cloudflare = **R$ 0,00 (Gratuito)** com renovação automática.

### 3.5 Rotinas de Backup na Nuvem (Disaster Recovery & LGPD)
* **Estratégia:** Script agendado (cron diário às 03:00) que gera `mysqldump`, compacta com `gzip`, criptografa com AES-256 e envia para armazenamento externo off-site.
* **Onde armazenar:** Cloudflare R2 ou Backblaze B2.
* **Custo:** **R$ 0,00** (Cloudflare R2 oferece 10 GB gratuitos; o backup do Clinix consumirá menos de 1 GB no ano).

### 3.6 Mensageria: Notificações WhatsApp e E-mail Transacional
* **WhatsApp (Lembretes e Confirmações):**
  * *Nativo do Clinix via link `wa.me`:* **R$ 0,00 (Gratuito)**. A recepcionista clica no botão do sistema e a mensagem personalizada abre pronta no WhatsApp Web/Desktop.
  * *API Automatizada sem clique (Opcional):* Caso a clínica queira envio 100% automático por robô, integrações como Z-API ou Evolution API custam entre **R$ 39,00 e R$ 79,00 / mês**. *(Pode ser cobrado à parte como opcional da clínica).*
* **E-mail Transacional (Avisos e Redefinição de Senha):**
  * *Provedores:* Resend (3.000 envios/mês grátis) ou Brevo (300 envios/dia grátis).
  * *Custo:* **R$ 0,00 / mês**.

---

## 4. Comparativo Detalhado dos Cenários de Implantação

### Cenário 1: "VPS Dedicada com Docker" (⭐ ESTRATÉGIA RECOMENDADA)
Uma máquina virtual privada (VPS) em provedor confiável rodando Ubuntu Server com Docker Compose:
* **Provedores recomendados:**
  * **Hetzner Cloud (CX22):** 2 vCPUs, 4 GB RAM, 40 GB SSD NVMe = **~€ 3,79 / mês (~R$ 23,00)**.
  * **Hostinger VPS 1 / Contabo Cloud VPS:** 4 GB RAM, 50 GB SSD = **~R$ 35,00 a R$ 45,00 / mês**.
  * **DigitalOcean Droplet Basic:** 1 vCPU, 2 GB RAM, 50 GB SSD = **~$ 6,00 / mês (~R$ 34,00)**.

| Item de Custo | Provedor / Solução | Custo em Moeda Original | Custo em Reais (R$) / Mês |
| :--- | :--- | :---: | :---: |
| Servidor VPS (API + MySQL + Caddy) | Hetzner / Hostinger / DigitalOcean | ~$6.00 / €3.79 | **R$ 32,00** |
| Domínio Institucional (`.com.br`) | Registro.br | R$ 40,00 / ano | **R$ 3,33** |
| Certificado SSL/TLS HTTPS | Let's Encrypt / Caddy automático | Gratuito | **R$ 0,00** |
| Frontend CDN Global | Cloudflare Pages / Vercel | Gratuito | **R$ 0,00** |
| Backup Externo Off-site | Cloudflare R2 / Backblaze B2 | Tier Free (< 10 GB) | **R$ 0,00** |
| E-mails Transacionais | Resend API (3.000 msgs/mês) | Tier Free | **R$ 0,00** |
| WhatsApp de Confirmação | Links Web nativos Clinix | Gratuito | **R$ 0,00** |
| **CUSTO TOTAL MENSAL** | — | — | **~R$ 35,33 / mês** |

---

### Cenário 2: "PaaS Gerenciado / Zero Administração de Servidor"
Para quem prefere não administrar linha de comando Linux, atualizações de sistema operacional ou firewall:
* Backend em container no **Render.com** (Starter Plan de $7/mês) — evita que a API entre em hibernação (*cold start*).
* Banco de Dados MySQL gerenciado na nuvem (**Aiven** ou **DigitalOcean Managed Database**).

| Item de Custo | Provedor / Solução | Custo Mensal Estimado |
| :--- | :--- | :---: |
| API ASP.NET Core (.NET 8) | Render.com Starter (Always On) | $7,00 (~R$ 39,00) |
| Banco de Dados MySQL 8 | Aiven MySQL / DigitalOcean DB | $15,00 (~R$ 84,00) |
| Domínio (`.com.br`) | Registro.br | R$ 3,33 |
| Frontend Web & SSL | Vercel / Cloudflare | R$ 0,00 |
| Backup em Nuvem | Integrado no banco Aiven | R$ 0,00 |
| **CUSTO TOTAL MENSAL** | — | **~R$ 126,33 / mês** |

---

### Cenário 3: "Piloto Gratuito para Testes de Validação" (Custo Mínimo Inicial)
Configuração utilizada durante os testes de homologação com a cliente antes do go-live oficial:
* Frontend no GitHub Pages ou Cloudflare Pages (R$ 0,00).
* API no Render.com Free Tier (R$ 0,00 — dorme após 15 min de inatividade, demora 40s para acordar).
* Banco de Dados MySQL na Aiven Free Tier ou Localhost da clínica (R$ 0,00).
* **Custo Total: R$ 0,00 a R$ 3,33 / mês** (apenas o domínio se registrado).

---

## 5. Viabilidade Financeira e Unit Economics (Lucratividade)

Analisando a relação entre os custos de infraestrutura levantados e o modelo comercial definido na [Proposta Comercial](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Proposta_Precificacao_Estrategia_Comercial.md):

### 5.1 No Modelo de Assinatura Recorrente (SaaS — R$ 179,00 / mês)

```
Demonstrativo de Resultado Mensal (Por Cliente):
┌────────────────────────────────────────────────────────┐
│ (+) Mensalidade do Clinix:                 R$ 179,00   │
│ (-) Custo de Infraestrutura (Cenário B):  - R$  35,33  │
├────────────────────────────────────────────────────────┤
│ (=) LUCRO LÍQUIDO RECORRENTE:              R$ 143,67   │
│     Margem de Lucro Operacional:                80,2%  │
└────────────────────────────────────────────────────────┘
```

#### Efeito de Escala (Alavancagem de Margem):
Uma única VPS de 4 GB RAM (Cenário B) suporta com folga os bancos de dados e instâncias de **até 5 clínicas pequenas diferentes** (utilizando bancos isolados `clinix_clinica_a`, `clinix_clinica_b`, etc.):

| Número de Clínicas Ativas | Faturamento Mensal | Custo Total da VPS | Lucro Líquido / Mês | Margem Líquida |
| :---: | :---: | :---: | :---: | :---: |
| **1 Clínica** | R$ 179,00 | R$ 35,33 | **R$ 143,67** | 80,2% |
| **2 Clínicas** | R$ 358,00 | R$ 35,33 | **R$ 322,67** | 90,1% |
| **3 Clínicas** | R$ 537,00 | R$ 35,33 | **R$ 501,67** | 93,4% |
| **5 Clínicas** | R$ 895,00 | R$ 45,00 *(upgrade de disco)* | **R$ 850,00** | 95,0% |

> Além da mensalidade, a **Taxa de Adesão única de R$ 290,00** paga pela clínica na contratação já cobre antecipadamente mais de **8 meses inteiros** de toda a infraestrutura em nuvem!

---

### 5.2 No Modelo de Projeto Dedicado (Não-SaaS — R$ 2.400,00 Fechado)

* A conta da VPS ou da nuvem é criada e faturada **diretamente no cartão de crédito da clínica**;
* O custo de manter o sistema no ar para você é **R$ 0,00 (Zero)**;
* A clínica desembolsa diretamente os ~R$ 35 a R$ 45/mês de hospedagem;
* O valor de **R$ 2.400,00** é receita 100% líquida para você;
* Você ainda pode oferecer o **Contrato Opcional de Sustentação** por **R$ 90,00 a R$ 120,00 / mês** (conforme previsto no [Contrato](file:///c:/Users/dav09/Dev/Projeto-Integrador/Sistema-de-Agendamento-e-Pronturio-para-Clinicas-Pequenas/docs/comercial/Contrato_Prestacao_Servicos_Clinix.md)), gerando receita recorrente sem arcar com o servidor.

---

## 6. Riscos Ocultos e Como Blindar os Gastos

Para garantir que a fatura não venha com surpresas no final do mês:

1. **Evitar Cobrança por Tráfego Excessivo na AWS/Azure:**
   Provedores como AWS cobram separadamente por GB de dados transferidos para fora (*egress traffic*). Na Hetzner ou DigitalOcean, os planos incluem de 1.000 GB a 20.000 GB de tráfego gratuito por mês, eliminando qualquer risco de surpresa.
2. **Armazenamento de Anexos no Banco de Dados:**
   Nunca grave PDFs de exames ou fotos diretamente em colunas `BLOB` do MySQL. O Clinix armazena apenas metadados e links, mantendo a base de dados leve e veloz.
3. **Backup Automatizado Fora do Servidor:**
   Nunca mantenha os backups apenas dentro do mesmo disco do servidor. O envio para o Cloudflare R2 / Backblaze B2 garante recuperação imediata caso o servidor queime ou sofra problemas.

---

## 7. Próximos Passos Recomendados para o Davi

1. **Para a Reunião com a Cliente:**
   Apresente que o sistema opera em servidores profissionais de nuvem de alta disponibilidade, com rotinas de backup diário e conformidade com a LGPD.
2. **Para a Fase de Homologação:**
   Manter o ambiente atual (Render + Aiven) ou provisionar uma VPS econômica de ~R$ 35/mês na DigitalOcean/Hetzner.
3. **Registro do Domínio Oficial:**
   Registrar o domínio oficial da plataforma no Registro.br (ex: `clinixgestao.com.br` ou `clinixapp.com.br`) por R$ 40/ano.
