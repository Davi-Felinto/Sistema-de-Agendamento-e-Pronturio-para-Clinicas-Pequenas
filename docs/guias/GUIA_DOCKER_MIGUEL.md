# 🐳 Guia de Instalação e Testes com Docker — Para o Miguel

> **Público-alvo:** Miguel (Desenvolvimento de Interface Web)  
> **Objetivo:** Subir o backend em C# (.NET 8) e o banco de dados relacional MySQL 8.0 totalmente povoado com **apenas 1 comando**, sem precisar instalar compiladores, SDKs ou servidores manuais no seu computador.  
> **Data:** Outubro de 2026 | **Projeto:** Clinix — Sistema de Agendamento e Prontuário para Clínicas Pequenas

---

## 🎯 1. Por que usar o Docker?

Como o seu foco é o **desenvolvimento visual e a experiência do usuário (UI/UX)** nas telas, você não precisa se preocupar em configurar ambiente C#/.NET, instalar servidor local de MySQL ou rodar scripts SQL manualmente.

Com o Docker:
1. O **MySQL 8.0** já sobe configurado e executa automaticamente os scripts que criam as 15 tabelas e populam todo o mês de outubro de 2026 (12 pacientes, 37 consultas, prontuários, financeiro).
2. A **API em C#** compila e roda automaticamente dentro do container.
3. As **telas da aplicação** e o visualizador do banco (**phpMyAdmin**) ficam acessíveis direto no seu navegador.
4. Se você alterar arquivos de tela (`index.html`), o Docker já reflete na hora!

---

## 📋 2. Pré-Requisitos

Antes de começar, você só precisa ter duas coisas instaladas na sua máquina Windows:
1. **Git** (para baixar e atualizar o código do repositório).
2. **Docker Desktop para Windows** (gratuito).

---

## 🚀 3. Passo a Passo de Instalação do Docker Desktop

Se você ainda não tem o Docker Desktop instalado no seu Windows, siga os passos abaixo:

### Passo 3.1: Download do Instalador
1. Acesse o site oficial do Docker: [https://www.docker.com/products/docker-desktop/](https://www.docker.com/products/docker-desktop/)
2. Clique no botão **"Download for Windows"**.

### Passo 3.2: Instalação
1. Execute o arquivo baixado (`Docker Desktop Installer.exe`).
2. Na tela de opções de configuração, **certifique-se de marcar a opção**:
   - ✅ **"Use WSL 2 instead of Hyper-V (recommended)"**
3. Clique em **OK** e aguarde o término da instalação.
4. Se o instalador solicitar que você reinicie o computador (**Restart Windows**), reinicie para concluir a ativação dos recursos do sistema.

> [!NOTE]
> Se ao abrir o Docker Desktop aparecer uma mensagem pedindo para atualizar o WSL 2, abra o **Prompt de Comando (CMD)** ou **PowerShell** como Administrador e execute:
> ```powershell
> wsl --update
> ```

### Passo 3.3: Abrir o Docker Desktop
1. Abra o menu Iniciar e clique em **Docker Desktop**.
2. Aceite os termos de serviço se solicitado.
3. Aguarde alguns instantes até que o ícone do Docker na barra de tarefas (ou no canto inferior esquerdo da janela) fique **verde** indicando: **"Engine running"**.

Pronto! Seu computador agora está pronto para rodar qualquer serviço com 1 comando.

---

## 🔄 4. Atualizar o Repositório do Projeto

Abra o terminal na pasta do projeto e garanta que você está com a versão mais recente da branch `main`:

```bash
git checkout main
git pull origin main
```

---

## ⚡ 5. Subindo o Ambiente (O Grande Passo!)

Com o Docker Desktop aberto, abra o terminal (PowerShell, Git Bash ou o terminal integrado do VS Code) **na pasta raiz do projeto** e execute:

```bash
docker compose up -d
```

### O que vai acontecer nos bastidores?
- O Docker vai baixar as imagens oficiais do MySQL 8.0 e do ASP.NET Core.
- O MySQL iniciará e executará automaticamente os scripts:
  - `01_schema_ddl.sql` (criação das 15 tabelas com chaves estrangeiras e índices).
  - `02_dados_iniciais.sql` (carga inicial completa do mês de outubro de 2026).
- A API C# compilará e se conectará automaticamente ao MySQL.
- O container visualizador do banco (**phpMyAdmin**) será iniciado.

Para verificar se tudo subiu com sucesso, você pode rodar:
```bash
docker compose ps
```
Você verá 3 containers com status `Up` ou `healthy`:
- `clinix-mysql` (Banco de Dados relacional)
- `clinix-api` (Backend ASP.NET Core + Frontend)
- `clinix-pma` (Interface Web do Banco phpMyAdmin)

---

## 🌐 6. Como Acessar e Testar

Tudo já está rodando localmente na sua máquina! Acesse os seguintes links no seu navegador:

### 🏥 A. Aplicação Web Clinix (Telas do Sistema)
👉 **URL:** [http://localhost:5055](http://localhost:5055)

Ao abrir, você verá a aplicação completa conectada diretamente ao MySQL do container:
- No cabeçalho, note os selos:
  - 🟢 **API: Online (.NET 8)**
  - 🟢 **Banco: MySQL 8.0 (Persistência Relacional)**

#### 🔑 Credenciais para Login de Teste:
| Perfil | Usuário | Senha | O que você pode testar |
|---|---|---|---|
| **Dr. Davi Felinto** (Médico / Administrador) | `Davi` | `Davi123!` | Ver agenda, acessar prontuários com histórico, editar diagnósticos, ver relatórios financeiros. |
| **Juliana Lima** (Recepcionista) | `Juliana` | `Juliana123!` | Agendar novas consultas, cadastrar pacientes, registrar pagamentos, emitir recibos. |

#### 📊 O que já vem povoado para você ver em tela:
- **Pacientes:** 12 pacientes cadastrados com CPFs, telefones, endereços e histórico clínico.
- **Agenda / Calendário:** 37 consultas distribuídas ao longo de todo o mês de outubro de 2026 (consultas concluídas, agendadas, retornos, bloqueios de agenda).
- **Prontuário Eletrônico:** 12 sessões com anamnese completa, diagnósticos, condutas e prescrições.
- **Financeiro:** 37 lançamentos (pagamentos via PIX, Cartão e Dinheiro, valores pagos e pendentes).

---

### 🗄️ B. Visualizador do Banco de Dados (phpMyAdmin)
👉 **URL:** [http://localhost:8085](http://localhost:8085)

Não precisa instalar MySQL Workbench, DBeaver nem linha de comando!
- O phpMyAdmin abre direto no seu navegador com login automático.
- No menu esquerdo, clique no banco **`clinix_db`**.
- Você verá as 15 tabelas (`pacientes`, `agendamentos`, `prontuarios`, `pagamentos`, `usuarios`, `logs_acesso`, etc.).
- Clique em qualquer tabela e depois em **"Visualizar" (Browse)** para ver as linhas e colunas reais!

---

### 📑 C. Documentação Interativa da API (Swagger)
👉 **URL:** [http://localhost:5055/swagger](http://localhost:5055/swagger)

- Mostra todos os endpoints disponíveis na API (`/api/pacientes`, `/api/agendamentos`, `/api/financeiro`, etc.).
- Você pode clicar em **"Try it out"** e testar requisições diretamente pela interface.

---

## 🎨 7. Como Você Pode Desenvolver as Telas

Você tem duas formas confortáveis de trabalhar no front-end:

### Opção 1: Usando o Live Server do VS Code (Recomendado para agilidade)
1. Deixe o Docker rodando em segundo plano (`docker compose up -d`).
2. Abra o arquivo `index.html` no VS Code.
3. Clique com o botão direito e selecione **"Open with Live Server"** (geralmente abre em `http://127.0.0.1:5500`).
4. O `api.js` detecta automaticamente a porta do Live Server e faz as requisições para a API do Docker em `http://localhost:5055` sem nenhum erro de CORS!
5. Cada alteração de CSS ou HTML que você salvar no editor recarrega a página automaticamente.

### Opção 2: Testando direto na porta do Docker
1. O volume de arquivos estáticos está mapeado no container.
2. Ao alterar o arquivo `src/ClinicaApp.Api/wwwroot/index.html`, basta atualizar a página (`F5`) em [http://localhost:5055](http://localhost:5055) para ver o resultado imediato.

> [!TIP]
> **Sincronização dos arquivos:**  
> O repositório tem o arquivo de tela na raiz (`index.html` - usado no GitHub Pages) e em `src/ClinicaApp.Api/wwwroot/index.html` (usado pela API C#).  
> Ao fazer melhorias no `index.html`, lembre-se de manter os dois sincronizados ou pedir para o Davi integrá-los no final da sessão.

---

## 🛠️ 8. Comandos Úteis do Dia a Dia

Aqui está uma colinha dos comandos mais usados no terminal:

| O que você quer fazer | Comando |
|---|---|
| **Subir tudo em segundo plano** | `docker compose up -d` |
| **Ver se os containers estão rodando** | `docker compose ps` |
| **Acompanhar os logs da API em tempo real** | `docker compose logs -f api` |
| **Parar os containers (sem apagar os dados)** | `docker compose stop` |
| **Reiniciar os containers parados** | `docker compose start` |
| **Encerrar e desligar os containers** | `docker compose down` |
| **Resetar o banco do zero (limpar e repovoar)** | `docker compose down -v` seguido de `docker compose up -d --build` |

---

## ❓ 9. Dúvidas Frequentes & Resolução de Problemas

### 🔴 Erro: "Port 3306 is already in use" ou "Port 5055 is already in use"
- **Causa:** Você já tem um MySQL ou um processo do C#/.NET rodando localmente na sua máquina fora do Docker.
- **Solução:**
  - Se tiver um serviço MySQL do Windows rodando, abra o Gerenciador de Serviços do Windows (`services.msc`), procure por **MySQL** ou **MySQL80** e clique em **Parar**.
  - Ou pare qualquer terminal que esteja rodando `dotnet run`.

### 🔴 O banco subiu, mas está vazio (sem as consultas ou pacientes)
- **Causa:** O Docker reutilizou um volume antigo criado anteriormente antes dos dados novos serem adicionados.
- **Solução:** Execute o comando de reset para forçar a carga dos dados iniciais:
  ```bash
  docker compose down -v
  docker compose up -d --build
  ```

### 🔴 Erro de WSL 2 ao abrir o Docker Desktop
- **Solução:** Abra o PowerShell como Administrador e execute:
  ```powershell
  wsl --update
  wsl --set-default-version 2
  ```
  Depois reinicie o computador.

---

## 🤝 Dúvidas ou Suporte?
Se surgir qualquer dúvida de conexão ou se você precisar de novos campos nos endpoints para apoiar suas telas, avise o **Davi** ou anote no arquivo de memória compartilhada em [`ia-system/membros/MIGUEL.md`](../../ia-system/membros/MIGUEL.md).
