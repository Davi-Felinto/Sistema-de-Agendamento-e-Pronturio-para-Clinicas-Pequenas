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

export const api = {
  isConnected: false,

  async checkHealth() {
    try {
      const res = await fetch('/api/pacientes', { method: 'GET', headers: { 'Accept': 'application/json' } });
      this.isConnected = res.ok;
      return res.ok;
    } catch {
      this.isConnected = false;
      return false;
    }
  },

  // --- PACIENTES (RF01-RF05, RN16) ---
  async listarPacientes() {
    try {
      const res = await fetch('/api/pacientes');
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
      const res = await fetch('/api/pacientes', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          nome: dados.nome,
          documentoIdentificacao: dados.cpf,
          dataNascimento: dados.nascimento ? new Date(dados.nascimento).toISOString() : new Date().toISOString(),
          telefone: dados.telefone,
          email: dados.email,
          endereco: dados.endereco,
          alergias: dados.alergias,
          condicoesPreexistentes: dados.condicoesPreexistentes
        })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao cadastrar paciente' };
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
        const res = await fetch(`/api/pacientes/${numId}/inativar`, { method: 'PUT' });
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

  // --- AGENDAMENTOS (RF06-RF11, RN01, RN02, RN10) ---
  async listarAgendamentos() {
    try {
      const res = await fetch('/api/agendamentos');
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
          if (a.status === 2 || a.status === 'Cancelado') statusStr = 'cancelado';
          else if (a.status === 0 || a.status === 'Pendente') statusStr = 'pendente';

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
            cancelamentoTardio: Boolean(a.cancelamentoTardio)
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

      const res = await fetch('/api/agendamentos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
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
        // Mensagem de bloqueio de RN01/RN02 devolvida pelo AgendaService C#
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

  async cancelarAgendamento(id) {
    try {
      const numId = typeof id === 'number' ? id : parseInt(String(id).replace(/\D/g, ''), 10);
      if (!isNaN(numId)) {
        const res = await fetch(`/api/agendamentos/${numId}/cancelar`, { method: 'PUT' });
        if (!res.ok) {
          const err = await res.json().catch(() => ({}));
          return { sucesso: false, erro: err.erro || err.mensagem || 'Erro ao cancelar consulta' };
        }
        const data = await res.json();
        return { sucesso: true, cancelamentoTardio: data.cancelamentoTardio };
      }
      return { sucesso: true };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  },

  // --- AUTENTICAÇÃO (RF26, RQ08) ---
  async login(login, senha) {
    try {
      const res = await fetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, senha })
      });
      if (!res.ok) {
        const err = await res.json().catch(() => ({}));
        return { sucesso: false, erro: err.erro || 'Login ou senha inválidos.' };
      }
      const user = await res.json();
      return { sucesso: true, dados: user };
    } catch (e) {
      return { sucesso: false, erro: e.message };
    }
  }
};
