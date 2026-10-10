-- ============================================================================
-- PROJETO INTEGRADOR - ENGENHARIA DE SOFTWARE (CEUB)
-- Disciplina: Banco de Dados II (MySQL) & POO (C# / .NET 8)
-- Sistema de Agendamento e Prontuário Eletrônico para Clínicas Pequenas (Clinix)
--
-- Autoria: Isaac (Modelagem Relacional) & Davi Felinto (Arquitetura e POO)
-- Versão: 2.0 (Com ajustes de unicidade, LGPD e compatibilidade C#)
-- ============================================================================

CREATE DATABASE IF NOT EXISTS clinix_db
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE clinix_db;

-- Desativa checagem temporária para recriação limpa (se necessário)
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS relatorios_gerados;
DROP TABLE IF EXISTS logs_acesso;
DROP TABLE IF EXISTS notificacoes;
DROP TABLE IF EXISTS resumos_financeiros_mensais;
DROP TABLE IF EXISTS pagamentos;
DROP TABLE IF EXISTS historico_versoes_prontuario;
DROP TABLE IF EXISTS sessoes_prontuario;
DROP TABLE IF EXISTS historico_remarcacoes;
DROP TABLE IF EXISTS bloqueios_agenda;
DROP TABLE IF EXISTS agendamentos;
DROP TABLE IF EXISTS registros_base_legal;
DROP TABLE IF EXISTS pacientes;
DROP TABLE IF EXISTS administradores;
DROP TABLE IF EXISTS profissionais_saude;
DROP TABLE IF EXISTS usuarios;

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================================
-- 1. AUTENTICAÇÃO E CONTROLE DE ACESSO (RF26, RF27, RQ06, RQ08)
-- ============================================================================

-- Tabela Base de Usuários (Especialização Table-per-Type)
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(120) NOT NULL,
    login VARCHAR(60) NOT NULL,
    senha_hash VARCHAR(255) NOT NULL COMMENT 'Armazenamento seguro com PBKDF2/SHA-256 + Salt (RQ08)',
    perfil ENUM('Profissional', 'Administrador') NOT NULL COMMENT 'Perfil de permissão RBAC (RF27, RQ06)',
    ativo TINYINT(1) NOT NULL DEFAULT 1 COMMENT '1 = Ativo, 0 = Inativo',
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ultimo_acesso DATETIME NULL,
    CONSTRAINT uq_usuarios_login UNIQUE (login),
    CONSTRAINT uq_usuarios_id_perfil UNIQUE (id_usuario, perfil)
) ENGINE=InnoDB COMMENT='Usuários do sistema (RF26, RQ08)';

-- Especialização: Profissionais de Saúde (Médicos, Psicólogos, Nutricionistas)
CREATE TABLE profissionais_saude (
    id_usuario INT PRIMARY KEY,
    perfil ENUM('Profissional', 'Administrador') NOT NULL DEFAULT 'Profissional',
    registro_profissional VARCHAR(30) NOT NULL COMMENT 'Ex: CRM-DF 12345, CRP-DF 67890',
    especialidade VARCHAR(80) NOT NULL,
    CONSTRAINT uq_prof_registro UNIQUE (registro_profissional),
    CONSTRAINT fk_prof_usuario FOREIGN KEY (id_usuario, perfil)
        REFERENCES usuarios (id_usuario, perfil)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Especialização de profissionais de saúde';

-- Especialização: Administradores e Recepcionistas
CREATE TABLE administradores (
    id_usuario INT PRIMARY KEY,
    perfil ENUM('Profissional', 'Administrador') NOT NULL DEFAULT 'Administrador',
    cargo VARCHAR(80) NOT NULL COMMENT 'Ex: Gestão & Recepção Clínica',
    CONSTRAINT fk_adm_usuario FOREIGN KEY (id_usuario, perfil)
        REFERENCES usuarios (id_usuario, perfil)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Especialização de administradores';

-- ============================================================================
-- 2. CADASTRO DE PACIENTES E PRIVACIDADE LGPD (RF01-RF05, RN16, RQ03)
-- ============================================================================

CREATE TABLE pacientes (
    id_paciente INT AUTO_INCREMENT PRIMARY KEY,
    id_profissional_responsavel INT NULL COMMENT 'Profissional de referência opcional; permite cadastro avulso na recepção',
    nome VARCHAR(150) NOT NULL,
    documento_identificacao VARCHAR(20) NULL COMMENT 'CPF ou RG. Anulável para permitir anonimização múltipla LGPD (RN16, RQ03)',
    data_nascimento DATE NOT NULL,
    telefone VARCHAR(20) NULL,
    email VARCHAR(150) NULL,
    endereco VARCHAR(255) NULL,
    alergias TEXT NULL COMMENT 'Dados clínicos vitais preservados permanentemente',
    condicoes_preexistentes TEXT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1 COMMENT '1 = Ativo, 0 = Inativo logicamente (RN16)',
    data_cadastro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_inativacao DATETIME NULL,
    anonimizado_em DATETIME NULL COMMENT 'Timestamp de anonimização LGPD (RQ03)',
    CONSTRAINT uq_pacientes_documento UNIQUE (documento_identificacao),
    CONSTRAINT fk_pacientes_profissional FOREIGN KEY (id_profissional_responsavel)
        REFERENCES profissionais_saude (id_usuario)
        ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Pacientes e dados pessoais protegidos por LGPD';

CREATE INDEX ix_pacientes_nome ON pacientes (nome);
CREATE INDEX ix_pacientes_profissional ON pacientes (id_profissional_responsavel);

-- Registro formal de Base Legal para tratamento de dados sensíveis (LGPD Art. 7 e 11)
CREATE TABLE registros_base_legal (
    id_registro INT AUTO_INCREMENT PRIMARY KEY,
    id_paciente INT NOT NULL,
    base_legal ENUM('TutelaDeSaude', 'Consentimento', 'ObrigacaoLegal', 'ExecucaoContrato') NOT NULL,
    finalidade VARCHAR(255) NOT NULL,
    observacoes TEXT NULL,
    data_registro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_usuario_registro INT NOT NULL,
    CONSTRAINT fk_baselegal_paciente FOREIGN KEY (id_paciente)
        REFERENCES pacientes (id_paciente)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_baselegal_usuario FOREIGN KEY (id_usuario_registro)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Rastreabilidade e consentimento LGPD';

CREATE INDEX ix_baselegal_paciente ON registros_base_legal (id_paciente);

-- ============================================================================
-- 3. GESTÃO DE AGENDAMENTOS E AGENDA (RF06-RF11, RN01, RN02, RN10)
-- ============================================================================

CREATE TABLE agendamentos (
    id_agendamento INT AUTO_INCREMENT PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_profissional INT NOT NULL,
    data_hora_inicio DATETIME NOT NULL,
    data_hora_fim DATETIME NOT NULL,
    status ENUM('Pendente', 'Confirmado', 'Remarcado', 'Cancelado', 'Concluido') NOT NULL DEFAULT 'Confirmado',
    observacoes TEXT NULL,
    cancelamento_tardio TINYINT(1) NOT NULL DEFAULT 0 COMMENT '1 se cancelado com menos de 24h de antecedência (RN10)',
    data_cancelamento DATETIME NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    -- Impede que o mesmo paciente possua duas consultas no mesmo horário inicial
    CONSTRAINT uq_agend_paciente_horario UNIQUE (id_paciente, data_hora_inicio),
    CONSTRAINT fk_agend_paciente FOREIGN KEY (id_paciente)
        REFERENCES pacientes (id_paciente)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_agend_profissional FOREIGN KEY (id_profissional)
        REFERENCES profissionais_saude (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Agendamentos de consultas (RF06, RN01, RN02, RN10)';

CREATE INDEX ix_agend_prof_periodo ON agendamentos (id_profissional, data_hora_inicio, data_hora_fim);
CREATE INDEX ix_agend_paciente_data ON agendamentos (id_paciente, data_hora_inicio);
CREATE INDEX ix_agend_status ON agendamentos (status);

-- Bloqueios de Horário / Férias / Intervalos do Profissional
CREATE TABLE bloqueios_agenda (
    id_bloqueio INT AUTO_INCREMENT PRIMARY KEY,
    id_profissional INT NOT NULL,
    tipo ENUM('Almoco', 'Ferias', 'CompromissoPessoal', 'Feriado', 'Outro') NOT NULL,
    data_hora_inicio DATETIME NOT NULL,
    data_hora_fim DATETIME NOT NULL,
    descricao VARCHAR(255) NULL,
    id_usuario_criacao INT NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_bloq_profissional FOREIGN KEY (id_profissional)
        REFERENCES profissionais_saude (id_usuario)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_bloq_usuario FOREIGN KEY (id_usuario_criacao)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Bloqueios de grade médica';

CREATE INDEX ix_bloq_prof_periodo ON bloqueios_agenda (id_profissional, data_hora_inicio, data_hora_fim);

-- Histórico de Remarcações (Auditoria de reagendamentos de consulta)
CREATE TABLE historico_remarcacoes (
    id_remarcacao INT AUTO_INCREMENT PRIMARY KEY,
    id_agendamento INT NOT NULL,
    inicio_anterior DATETIME NOT NULL,
    fim_anterior DATETIME NOT NULL,
    inicio_novo DATETIME NOT NULL,
    fim_novo DATETIME NOT NULL,
    remarcado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_usuario INT NULL,
    CONSTRAINT fk_remarc_agendamento FOREIGN KEY (id_agendamento)
        REFERENCES agendamentos (id_agendamento)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_remarc_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario)
        ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Rastreabilidade de remarcações de consultas';

CREATE INDEX ix_remarc_agendamento ON historico_remarcacoes (id_agendamento);

-- ============================================================================
-- 4. PRONTUÁRIO ELETRÔNICO E HISTÓRICO IMUTÁVEL (RF15-RF18, RN05, RN17)
-- ============================================================================

CREATE TABLE sessoes_prontuario (
    id_sessao INT AUTO_INCREMENT PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_agendamento INT NOT NULL COMMENT '1 sessão clínica por consulta realizada',
    data_registro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    anotacoes_clinicas TEXT NOT NULL,
    informacoes_clinicas_relevantes TEXT NULL,
    versao_atual INT NOT NULL DEFAULT 1,
    data_ultima_alteracao DATETIME NULL,
    motivo_alteracao VARCHAR(255) NULL,
    id_usuario_alteracao INT NULL,
    CONSTRAINT uq_sessao_agendamento UNIQUE (id_agendamento),
    CONSTRAINT fk_sessao_paciente FOREIGN KEY (id_paciente)
        REFERENCES pacientes (id_paciente)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_sessao_agendamento FOREIGN KEY (id_agendamento)
        REFERENCES agendamentos (id_agendamento)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_sessao_usuario_alt FOREIGN KEY (id_usuario_alteracao)
        REFERENCES usuarios (id_usuario)
        ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Sessões de prontuário eletrônico (RF15, RN05)';

CREATE INDEX ix_sessao_paciente_data ON sessoes_prontuario (id_paciente, data_registro);

-- Histórico de Versões de Anotações (Imutabilidade exigida por CFM/CFP e RN17)
CREATE TABLE historico_versoes_prontuario (
    id_versao INT AUTO_INCREMENT PRIMARY KEY,
    id_sessao INT NOT NULL,
    numero_versao INT NOT NULL,
    texto_anterior TEXT NOT NULL,
    informacoes_clinicas_anteriores TEXT NULL,
    data_modificacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    motivo_alteracao VARCHAR(255) NOT NULL COMMENT 'Justificativa clínica obrigatória (RN17)',
    id_usuario INT NOT NULL,
    CONSTRAINT uq_versao_sessao_numero UNIQUE (id_sessao, numero_versao),
    CONSTRAINT fk_versao_sessao FOREIGN KEY (id_sessao)
        REFERENCES sessoes_prontuario (id_sessao)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_versao_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Histórico imutável de versões de anotações (RN17)';

-- ============================================================================
-- 5. MÓDULO FINANCEIRO (RF19-RF22, RN07-RN09)
-- ============================================================================

CREATE TABLE pagamentos (
    id_pagamento INT AUTO_INCREMENT PRIMARY KEY,
    id_agendamento INT NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    status ENUM('Pendente', 'Pago') NOT NULL DEFAULT 'Pendente',
    forma_pagamento ENUM('Pix', 'Dinheiro', 'CartaoCredito', 'CartaoDebito') NULL,
    data_pagamento DATETIME NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    -- Garante exatamente 1 cobrança por consulta (RN07)
    CONSTRAINT uq_pagamento_agendamento UNIQUE (id_agendamento),
    CONSTRAINT fk_pagamento_agendamento FOREIGN KEY (id_agendamento)
        REFERENCES agendamentos (id_agendamento)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Controle financeiro de pagamentos (RF19, RN07, RN08)';

CREATE INDEX ix_pagamento_status ON pagamentos (status);

-- Resumo Consolidado Mensal (RF22, RN09)
CREATE TABLE resumos_financeiros_mensais (
    id_resumo INT AUTO_INCREMENT PRIMARY KEY,
    id_profissional INT NOT NULL,
    ano SMALLINT NOT NULL,
    mes TINYINT NOT NULL,
    total_atendimentos INT NOT NULL DEFAULT 0,
    total_faturado DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    total_pendente DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    gerado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT uq_resumo_periodo UNIQUE (id_profissional, ano, mes),
    CONSTRAINT fk_resumo_profissional FOREIGN KEY (id_profissional)
        REFERENCES profissionais_saude (id_usuario)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Consolidação financeira mensal (RF22, RN09)';

-- ============================================================================
-- 6. NOTIFICAÇÕES E AUDITORIA (RF12-RF14, RF28, RN13, RQ07)
-- ============================================================================

CREATE TABLE notificacoes (
    id_notificacao INT AUTO_INCREMENT PRIMARY KEY,
    id_agendamento INT NOT NULL,
    tipo ENUM('Confirmacao', 'Lembrete') NOT NULL,
    canal ENUM('WhatsApp') NOT NULL DEFAULT 'WhatsApp',
    data_hora_agendada DATETIME NOT NULL,
    data_hora_envio DATETIME NULL,
    status ENUM('Pendente', 'Enviado', 'Falha', 'Reenviado') NOT NULL DEFAULT 'Pendente',
    tentativas_envio TINYINT NOT NULL DEFAULT 0,
    proxima_tentativa DATETIME NULL,
    alerta_agenda TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'Sinaliza na agenda se falhou (RF14)',
    mensagem VARCHAR(500) NULL,
    detalhe_erro VARCHAR(255) NULL,
    CONSTRAINT fk_notif_agendamento FOREIGN KEY (id_agendamento)
        REFERENCES agendamentos (id_agendamento)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Fila e retentativas de notificações WhatsApp (RF12-RF14)';

CREATE INDEX ix_notif_agendamento ON notificacoes (id_agendamento);
CREATE INDEX ix_notif_fila ON notificacoes (status, data_hora_agendada);
CREATE INDEX ix_notif_retentativa ON notificacoes (status, proxima_tentativa);

-- Trilha de Auditoria e Acessos LGPD (RF28, RN13, RQ07)
CREATE TABLE logs_acesso (
    id_log BIGINT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_paciente INT NULL COMMENT 'Preenchido quando a operação envolve paciente/prontuário',
    operacao VARCHAR(50) NOT NULL COMMENT 'Ex: Acesso Prontuario, Autenticacao, Inativacao LGPD',
    entidade VARCHAR(50) NULL COMMENT 'Ex: Paciente, SessaoProntuario',
    id_registro BIGINT NULL,
    detalhes TEXT NULL,
    ip_origem VARCHAR(45) NULL,
    data_hora DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT 'Alta precisão em milissegundos',
    CONSTRAINT fk_log_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_log_paciente FOREIGN KEY (id_paciente)
        REFERENCES pacientes (id_paciente)
        ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Auditoria imutável LGPD (RF28, RN13, RQ07)';

CREATE INDEX ix_log_usuario_data ON logs_acesso (id_usuario, data_hora);
CREATE INDEX ix_log_paciente_data ON logs_acesso (id_paciente, data_hora);

-- Relatórios e Exportações Geradas
CREATE TABLE relatorios_gerados (
    id_relatorio INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_paciente INT NULL,
    periodo_inicio DATE NOT NULL,
    periodo_fim DATE NOT NULL,
    status_pagamento ENUM('Pendente', 'Pago', 'Todos') NULL,
    formato_exportacao ENUM('PDF', 'CSV', 'JSON') NOT NULL,
    gerado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_relatorio_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_relatorio_paciente FOREIGN KEY (id_paciente)
        REFERENCES pacientes (id_paciente)
        ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB COMMENT='Histórico de relatórios gerados';

CREATE INDEX ix_relatorio_usuario ON relatorios_gerados (id_usuario);
