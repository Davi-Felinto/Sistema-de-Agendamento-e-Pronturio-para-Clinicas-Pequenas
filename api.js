/**
 * Clinix - Camada de Serviços Front-End (api.js)
 * Conexão com o backend ASP.NET Core em C# (POO)
 * Contrato de Métodos acordado entre Davi (Backend/Integração) e Miguel (Interface)
 */

const getTodayDateStr = () => {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const day = String(now.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

const CLOUD_API_URL = 'https://clinix-api-c58x.onrender.com';

const getApiBase = () => {
  if (typeof window !== 'undefined' && window.location) {
    // Servido diretamente pela API C# (.NET 8 local na porta 5055)
    if (window.location.port === '5055' || (window.location.href && window.location.href.includes(':5055/'))) {
      return '';
    }
    // Servido diretamente pelo container no Render
    if (window.location.hostname.includes('onrender.com')) {
      return '';
    }
    // Live Server ou arquivo local: conecta ao backend local na porta 5055
    if (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1' || window.location.protocol === 'file:') {
      return 'http://localhost:5055';
    }
    // Produção no GitHub Pages: conecta à API de produção no Render
    if (window.location.hostname.includes('github.io')) {
      return CLOUD_API_URL;
    }
  }
  return CLOUD_API_URL;
};
const API_BASE = getApiBase();

export const api = {
  isConnected: false,
  storage: 'offline', // 'mysql' | 'memoria' | 'offline'
  storageLabel: 'Modo Standalone (Offline)',

  // RF26 - Token JWT devolvido pelo login. Fica em sessionStorage (some ao fechar a aba)
  // e é enviado no cabeçalho Authorization de todas as chamadas protegidas.
  token: (() => { try { return sessionStorage.getItem('clinix_token'); } catch { return null; } })(),

  setToken(token) {
    this.token = token || null;
    try {
      if (this.token) sessionStorage.setItem('clinix_token', this.token);
      else sessionStorage.removeItem('clinix_token');
    } catch { /* sessionStorage indisponível */ }
  },

  authHeaders(extra = {}) {
    return this.token ? { ...extra, 'Authorization': `Bearer ${this.token}` } : extra;
  },

  async checkHealth() {
    try {
      const res = await fetch(`${API_BASE}/api/status`, { method: 'GET', headers: { 'Accept': 'application/json' } });
      if (res.ok) {
        const data = await res.json();
        this.isConnected = true;
        this.storage = data.storage || 'memoria';
        this.storageLabel = data.storageLabel || 'Banco: Memória (Mock)';
        return { ok: true, storage: this.storage, storageLabel: this.storageLabel };
      }
      const resHealth = await fetch(`${API_BASE}/api/health`, { method: 'GET', headers: { 'Accept': 'application/json' } });
      this.isConnected = resHealth.ok;
      this.storage = resHealth.ok ? 'memoria' : 'offline';
      this.storageLabel = resHealth.ok ? 'API Conectada' : 'Modo Standalone (Offline)';
      return { ok: resHealth.ok, storage: this.storage, storageLabel: this.storageLabel };
    } catch {
      this.isConnected = false;
      this.storage = 'offline';
      this.storageLabel = 'Modo Standalone (Offline)';
      return { ok: false, storage: 'offline', storageLabel: 'Modo Standalone (Offline)' };
    }
  },

  // --- AUTENTICAÇÃO E REGISTRO (RF26, RQ08) ---
  async login(login, senha) {
    try {
      const res = await fetch(`${API_BASE}/api/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Accept': 'application/json' },
        body: JSON.stringify({ login: String(login).trim(), senha: String(senha) })
      });

      if (!res.ok) {
        if (res.status === 429) {
          return { sucesso: false, erro: 'Muitas tentativas em pouco tempo. Aguarde alguns segundos e tente novamente.' };
        }
        let erroMsg = 'Login ou senha inválidos.';
        try {
          const erroData = await res.json();
          if (erroData && (erroData.erro || erroData.mensagem)) erroMsg = erroData.erro || erroData.mensagem;
        } catch {}
        return { sucesso: false, erro: erroMsg };
      }

      const data = await res.json();
      this.setToken(data.token);
      this.isConnected = true;
      return {
        sucesso: true,
        dados: data,
        usuario: {
          id: data.id,
          name: data.nome,
          role: data.perfil,
          email: data.email || login,
          token: data.token
        }
      };
    } catch (e) {
      return { sucesso: false, erro: 'Falha de conexão com a API: ' + e.message };
    }
  },

  async registrar(dados) {
    try {
      const res = await fetch(`${API_BASE}/api/auth/registrar`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Accept': 'application/json' },
        body: JSON.stringify({
          nome: String(dados.nome || '').trim(),
          email: String(dados.email || '').trim(),
          senha: String(dados.senha || ''),
          perfil: dados.perfil || 'Profissional',
          nomeClinica: dados.nomeClinica || null,
          registroProfissional: dados.registroProfissional || null,
          especialidade: dados.especialidade || null,
          codigoConvite: dados.codigoConvite ? String(dados.codigoConvite).trim() : null
        })
      });

      if (!res.ok) {
        let erroMsg = 'Não foi possível concluir o cadastro.';
        try {
          const erroData = await res.json();
          if (erroData && erroData.erro) erroMsg = erroData.erro;
        } catch {}
        return { sucesso: false, erro: erroMsg };
      }

      const data = await res.json();
      this.setToken(data.token);
      this.isConnected = true;
      return {
        sucesso: true,
        usuario: {
          id: data.id,
          name: data.nome,
          role: data.perfil,
          email: data.email || dados.email,
          token: data.token
        }
      };
    } catch (e) {
      return { sucesso: false, erro: 'Falha de conexão com a API: ' + e.message };
    }
  },

  async listarUsuarios() {
    try {
      const res = await fetch(`${API_BASE}/api/auth/usuarios`, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao listar usuários');
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async listarLogs() {
    try {
      const res = await fetch(`${API_BASE}/api/logs`, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao listar logs de auditoria');
      const data = await res.json();
      return {
        sucesso: true,
        dados: data.map(l => ({
          id: String(l.id),
          timestamp: l.dataHora || new Date().toISOString(),
          usuarioId: String(l.usuarioId),
          usuarioNome: l.usuarioNome || `Usuário #${l.usuarioId}`,
          usuarioEmail: l.usuarioEmail || '',
          operacao: l.operacao,
          detalhes: l.detalhes || '',
          ipOrigem: l.ipOrigem || null
        }))
      };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- CEP & ENDEREÇO ESTRUTURADO (RF29, RN18) ---
  async consultarCep(cep) {
    try {
      const limpo = String(cep).replace(/\D/g, '');
      if (limpo.length !== 8) {
        return { sucesso: false, erro: 'CEP deve conter 8 dígitos' };
      }
      // 1. Tenta consulta direta no ViaCEP
      try {
        const res = await fetch(`https://viacep.com.br/ws/${limpo}/json/`);
        if (res.ok) {
          const data = await res.json();
          if (!data.erro) {
            return {
              sucesso: true,
              cep: data.cep || limpo,
              logradouro: data.logradouro || '',
              bairro: data.bairro || '',
              cidade: data.localidade || '',
              estadoUf: data.uf || '',
              complemento: data.complemento || ''
            };
          }
        }
      } catch {
        // Bloqueio de CSP ou offline do navegador: recorre ao proxy backend
      }

      // 2. Fallback: Proxy backend na API local (/api/cep/{limpo})
      try {
        const resBackend = await fetch(`${API_BASE}/api/cep/${limpo}`, { headers: this.authHeaders() });
        if (resBackend.ok) {
          const data = await resBackend.json();
          if (data.sucesso) return data;
        }
      } catch {}

      return { sucesso: false, erro: 'CEP não encontrado na base oficial. Preencha manualmente.' };
    } catch (e) {
      return { sucesso: false, erro: 'Não foi possível consultar o CEP: ' + e.message };
    }
  },

  // --- PACIENTES (RF01-RF05, RN16, RF29, RN18) ---
  async listarPacientes() {
    try {
      const res = await fetch(`${API_BASE}/api/pacientes`, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao listar pacientes');
      const data = await res.json();
      this.isConnected = true;
      return {
        sucesso: true,
        dados: data.map(p => ({
          id: p.id,
          nome: p.nome,
          cpf: p.documentoIdentificacao || p.cpf || '',
          nascimento: p.dataNascimento ? p.dataNascimento.split('T')[0] : (p.nascimento || ''),
          telefone: p.telefone || '',
          email: p.email || '',
          endereco: p.endereco || '',
          cep: p.cep || '',
          logradouro: p.logradouro || '',
          numero: p.numero || '',
          complemento: p.complemento || '',
          bairro: p.bairro || '',
          cidade: p.cidade || '',
          estadoUf: p.estadoUf || '',
          alergias: p.alergias || '',
          condicoesPreexistentes: p.condicoesPreexistentes || '',
          status: p.ativo !== undefined ? (p.ativo ? 'ativo' : 'inativo') : (p.status || 'ativo'),
          profissionalId: 'u1'
        }))
      };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async criarPaciente(dados) {
    try {
      const res = await fetch(`${API_BASE}/api/pacientes`, {
        method: 'POST',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          nome: dados.nome,
          documentoIdentificacao: dados.cpf,
          dataNascimento: dados.nascimento ? new Date(dados.nascimento).toISOString() : new Date().toISOString(),
          telefone: dados.telefone,
          email: dados.email,
          endereco: dados.endereco,
          cep: dados.cep,
          logradouro: dados.logradouro,
          numero: dados.numero,
          complemento: dados.complemento,
          bairro: dados.bairro,
          cidade: dados.cidade,
          estadoUf: dados.estadoUf,
          alergias: dados.alergias,
          condicoesPreexistentes: dados.condicoesPreexistentes
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        const validacao = err.errors ? Object.values(err.errors).flat()[0] : null;
        return { sucesso: false, erro: err.erro || err.mensagem || validacao || 'Erro ao cadastrar paciente' };
      }
      const p = await res.json();
      return {
        sucesso: true,
        dados: {
          id: p.id,
          nome: p.nome,
          cpf: p.documentoIdentificacao || dados.cpf,
          nascimento: p.dataNascimento ? p.dataNascimento.split('T')[0] : dados.nascimento,
          telefone: p.telefone,
          email: p.email,
          endereco: p.endereco,
          cep: p.cep || dados.cep,
          logradouro: p.logradouro || dados.logradouro,
          numero: p.numero || dados.numero,
          complemento: p.complemento || dados.complemento,
          bairro: p.bairro || dados.bairro,
          cidade: p.cidade || dados.cidade,
          estadoUf: p.estadoUf || dados.estadoUf,
          alergias: p.alergias,
          condicoesPreexistentes: p.condicoesPreexistentes,
          status: p.ativo ? 'ativo' : 'inativo',
          profissionalId: dados.profissionalId || 'u1'
        }
      };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async inativarPaciente(id) {
    try {
      const numId = Number(id);
      if (!isNaN(numId)) {
        const res = await fetch(`${API_BASE}/api/pacientes/${numId}/inativar`, {
          method: 'PUT',
          headers: this.authHeaders()
        });
        if (!res.ok) {
          const err = await res.json().catch(() => ({}));
          return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao inativar paciente' };
        }
      }
      return { sucesso: true };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- AGENDAMENTOS (RF06-RF11, RN01, RN02, RN10, RF31, RN20) ---
  async listarAgendamentos() {
    try {
      const res = await fetch(`${API_BASE}/api/agendamentos`, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao listar agendamentos');
      const data = await res.json();
      this.isConnected = true;
      return {
        sucesso: true,
        dados: data.map(a => {
          const inicioStr = a.dataHoraInicio || '';
          const fimStr = a.dataHoraFim || '';
          const dataStr = inicioStr ? inicioStr.split('T')[0] : getTodayDateStr();
          const horaStr = inicioStr ? inicioStr.split('T')[1].slice(0, 5) : '09:00';
          const duracaoMin = (inicioStr && fimStr)
            ? Math.round((new Date(fimStr) - new Date(inicioStr)) / 60000)
            : 60;
          let statusStr = 'confirmado';
          if (a.status === 2 || a.status === 4 || a.status === 'Cancelado') statusStr = 'cancelado';
          else if (a.status === 3 || a.status === 'Remarcado') statusStr = 'remarcado';
          else if (a.status === 0 || a.status === 1 || a.status === 'Pendente') statusStr = 'pendente';

          return {
            id: a.id,
            pacienteId: a.pacienteId,
            profissionalId: a.profissionalId === 1 ? 'u1' : `u${a.profissionalId}`,
            data: dataStr,
            horario: horaStr,
            duracaoMin,
            status: statusStr,
            tipo: 'consulta',
            observacoes: a.observacoes || 'Consulta de rotina',
            valor: 200,
            statusPagamento: 'pendente',
            formaPagamento: 'Em aberto',
            notificacaoStatus: 'enviado',
            notificacaoTentativas: 1,
            cancelamentoTardio: Boolean(a.cancelamentoTardio),
            motivoCancelamento: a.motivoCancelamento || null,
            justificativaCancelamento: a.justificativaCancelamento || null
          };
        })
      };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async agendarConsulta(dados) {
    try {
      const inicioDate = `${dados.data}T${dados.horario}:00`;
      const inicio = new Date(inicioDate);
      const duracaoMin = dados.duracaoMin || 60;
      const fim = new Date(inicio.getTime() + duracaoMin * 60000);
      const fimIso = `${dados.data}T${String(fim.getHours()).padStart(2, '0')}:${String(fim.getMinutes()).padStart(2, '0')}:00`;

      const pacienteIdNum = typeof dados.pacienteId === 'number'
        ? dados.pacienteId
        : (parseInt(String(dados.pacienteId).replace(/\D/g, ''), 10) || 1);

      const profissionalIdNum = typeof dados.profissionalId === 'number'
        ? dados.profissionalId
        : (parseInt(String(dados.profissionalId).replace(/\D/g, ''), 10) || 1);

      const res = await fetch(`${API_BASE}/api/agendamentos`, {
        method: 'POST',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          pacienteId: pacienteIdNum,
          profissionalId: profissionalIdNum,
          dataHoraInicio: inicioDate,
          dataHoraFim: fimIso,
          observacoes: dados.observacoes || 'Consulta de rotina'
        })
      });

      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return {
          sucesso: false,
          erro: err.erro || err.mensagem || 'Horário indisponível: conflito de horário identificado (RN01, RN02).'
        };
      }

      const criado = await res.json();
      return {
        sucesso: true,
        dados: {
          id: criado.id,
          pacienteId: dados.pacienteId,
          profissionalId: dados.profissionalId,
          data: dados.data,
          horario: dados.horario,
          duracaoMin,
          status: 'confirmado',
          tipo: 'consulta',
          observacoes: criado.observacoes || dados.observacoes,
          valor: dados.valor || 200,
          statusPagamento: 'pendente',
          formaPagamento: 'Em aberto',
          notificacaoStatus: 'enviado',
          notificacaoTentativas: 1,
          cancelamentoTardio: false
        }
      };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async cancelarAgendamento(id, motivo = 'Outro', justificativa = '') {
    try {
      const numId = typeof id === 'number' ? id : parseInt(String(id).replace(/\D/g, ''), 10);
      if (!isNaN(numId)) {
        const res = await fetch(`${API_BASE}/api/agendamentos/${numId}/cancelar`, {
          method: 'PUT',
          headers: this.authHeaders({ 'Content-Type': 'application/json' }),
          body: JSON.stringify({
            motivo: motivo,
            justificativa: justificativa
          })
        });
        if (!res.ok) {
          const err = await res.json().catch(() => ({}));
          return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao cancelar consulta' };
        }
        const data = await res.json();
        return {
          sucesso: true,
          cancelamentoTardio: data.cancelamentoTardio,
          motivoCancelamento: data.motivoCancelamento
        };
      }
      return { sucesso: true };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- FINANCEIRO & PAGAMENTO DIVIDIDO (RF21-RF25, RF30, RN19) ---
  async registrarPagamentoDividido(pagamentoId, dados) {
    try {
      const numId = typeof pagamentoId === 'number' ? pagamentoId : parseInt(String(pagamentoId).replace(/\D/g, ''), 10);
      const res = await fetch(`${API_BASE}/api/financeiro/${numId || pagamentoId}/pagamento-dividido`, {
        method: 'POST',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          valorEntrada: Number(dados.valorEntrada),
          formaEntrada: dados.formaEntrada,
          valorSaldo: Number(dados.valorSaldo),
          formaSaldo: dados.formaSaldo,
          saldoPago: Boolean(dados.saldoPago)
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao registrar pagamento dividido' };
      }
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- OUVIDORIA & AVALIAÇÕES (RF32, RN21) ---
  async listarAvaliacoes() {
    try {
      const res = await fetch(`${API_BASE}/api/avaliacoes`, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao obter avaliações');
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async registrarAvaliacao(dados) {
    try {
      const res = await fetch(`${API_BASE}/api/avaliacoes`, {
        method: 'POST',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          agendamentoId: Number(dados.agendamentoId),
          notaRecepcao: Number(dados.notaRecepcao),
          notaAtendimentoClinico: Number(dados.notaAtendimentoClinico),
          notaEspacoFisico: Number(dados.notaEspacoFisico),
          comentarioOuvidoria: dados.comentarioOuvidoria || null,
          anonimo: Boolean(dados.anonimo)
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao registrar avaliação' };
      }
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- RASTREAMENTO DE EXAMES LABORATORIAIS (RF34, RN23) ---
  async listarExames(pacienteId = null) {
    try {
      const url = pacienteId ? `${API_BASE}/api/exameslaboratorio?pacienteId=${pacienteId}` : `${API_BASE}/api/exameslaboratorio`;
      const res = await fetch(url, { headers: this.authHeaders() });
      if (!res.ok) throw new Error('Falha ao listar exames laboratoriais');
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async criarExame(dados) {
    try {
      const res = await fetch(`${API_BASE}/api/exameslaboratorio`, {
        method: 'POST',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          pacienteId: Number(dados.pacienteId),
          nomeExameAmostra: dados.nomeExameAmostra,
          laboratorioParceiro: dados.laboratorioParceiro,
          dataColeta: dados.dataColeta,
          dataPrevisaoLaudo: dados.dataPrevisaoLaudo,
          codigoRastreio: dados.codigoRastreio || null,
          observacoes: dados.observacoes || null
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao cadastrar pedido de exame' };
      }
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  async atualizarStatusExame(id, novoStatus, observacao = null) {
    try {
      const res = await fetch(`${API_BASE}/api/exameslaboratorio/${id}/status`, {
        method: 'PUT',
        headers: this.authHeaders({ 'Content-Type': 'application/json' }),
        body: JSON.stringify({
          novoStatus: novoStatus,
          observacao: observacao
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao atualizar status do exame' };
      }
      const data = await res.json();
      return { sucesso: true, dados: data };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  }
};
