-- ============================================================================
-- PROJETO INTEGRADOR - ENGENHARIA DE SOFTWARE (CEUB)
-- Disciplina: Banco de Dados II (MySQL) & POO (C# / .NET 8)
-- Script 03: Recursos Avançados de Banco de Dados (Views, Triggers e Stored Procedures)
--
-- Autoria: Isaac (Modelagem Relacional) & Davi Felinto (Arquitetura e POO)
-- Rastreabilidade: RF06, RF08, RF09, RF15, RF16, RF22, RN01, RN07, RN08, RN09, RN10, RN13, RN16, RN17, RQ03, RQ07
-- ============================================================================

USE clinix_db;

-- ============================================================================
-- 1. VIEWS ANALÍTICAS E OPERACIONAIS
-- ============================================================================

-- ----------------------------------------------------------------------------
-- View 1: vw_agenda_detalhada (RF06, RF09, RN01, RN10)
-- Visão consolidada da agenda unindo paciente, médico, status amigável e financeiro
-- ----------------------------------------------------------------------------
DROP VIEW IF EXISTS vw_agenda_detalhada;
CREATE VIEW vw_agenda_detalhada AS
SELECT 
    a.id_agendamento,
    a.data_hora_inicio,
    a.data_hora_fim,
    a.status AS status_agendamento,
    a.cancelamento_tardio,
    a.observacoes,
    p.id_paciente,
    p.nome AS paciente_nome,
    p.documento_identificacao AS paciente_cpf,
    p.telefone AS paciente_telefone,
    p.email AS paciente_email,
    p.ativo AS paciente_ativo,
    u.id_usuario AS id_profissional,
    u.nome AS profissional_nome,
    ps.registro_profissional,
    ps.especialidade,
    pag.id_pagamento,
    pag.valor AS valor_consulta,
    pag.status AS status_pagamento,
    pag.forma_pagamento,
    sp.id_sessao AS id_sessao_prontuario,
    a.criado_em
FROM agendamentos a
INNER JOIN pacientes p ON a.id_paciente = p.id_paciente
INNER JOIN profissionais_saude ps ON a.id_profissional = ps.id_usuario
INNER JOIN usuarios u ON ps.id_usuario = u.id_usuario
LEFT JOIN pagamentos pag ON a.id_agendamento = pag.id_agendamento
LEFT JOIN sessoes_prontuario sp ON a.id_agendamento = sp.id_agendamento;

-- ----------------------------------------------------------------------------
-- View 2: vw_resumo_financeiro_mensal (RF22, RN09)
-- Consolidação analítica em tempo real de faturamento, pendências e volumes
-- ----------------------------------------------------------------------------
DROP VIEW IF EXISTS vw_resumo_financeiro_mensal;
CREATE VIEW vw_resumo_financeiro_mensal AS
SELECT 
    u.id_usuario AS id_profissional,
    u.nome AS profissional_nome,
    YEAR(a.data_hora_inicio) AS ano,
    MONTH(a.data_hora_inicio) AS mes,
    COUNT(a.id_agendamento) AS total_consultas,
    COUNT(pag.id_pagamento) AS total_cobrancas,
    COUNT(CASE WHEN pag.status = 'Pago' THEN 1 END) AS total_pagos,
    COUNT(CASE WHEN pag.status = 'Pendente' THEN 1 END) AS total_pendentes,
    COALESCE(SUM(CASE WHEN pag.status = 'Pago' THEN pag.valor ELSE 0 END), 0.00) AS total_faturado,
    COALESCE(SUM(CASE WHEN pag.status = 'Pendente' THEN pag.valor ELSE 0 END), 0.00) AS total_pendente,
    COALESCE(SUM(pag.valor), 0.00) AS total_geral
FROM agendamentos a
INNER JOIN profissionais_saude ps ON a.id_profissional = ps.id_usuario
INNER JOIN usuarios u ON ps.id_usuario = u.id_usuario
LEFT JOIN pagamentos pag ON a.id_agendamento = pag.id_agendamento
GROUP BY u.id_usuario, u.nome, YEAR(a.data_hora_inicio), MONTH(a.data_hora_inicio);

-- ----------------------------------------------------------------------------
-- View 3: vw_prontuario_historico_completo (RF15, RF16, RN17, RN13)
-- Histórico clínico integrado do paciente com rastreabilidade de quem evoluiu
-- ----------------------------------------------------------------------------
DROP VIEW IF EXISTS vw_prontuario_historico_completo;
CREATE VIEW vw_prontuario_historico_completo AS
SELECT 
    sp.id_sessao,
    sp.id_paciente,
    p.nome AS paciente_nome,
    p.documento_identificacao AS paciente_cpf,
    sp.id_agendamento,
    a.data_hora_inicio AS data_consulta,
    u_medico.nome AS medico_responsavel,
    sp.versao_atual,
    sp.anotacoes_clinicas AS evolucao_atual,
    sp.informacoes_clinicas_relevantes,
    sp.data_registro,
    sp.data_ultima_alteracao,
    sp.motivo_alteracao,
    u_alt.nome AS usuario_ultima_alteracao
FROM sessoes_prontuario sp
INNER JOIN pacientes p ON sp.id_paciente = p.id_paciente
INNER JOIN agendamentos a ON sp.id_agendamento = a.id_agendamento
INNER JOIN usuarios u_medico ON a.id_profissional = u_medico.id_usuario
LEFT JOIN usuarios u_alt ON sp.id_usuario_alteracao = u_alt.id_usuario;

-- ============================================================================
-- 2. TRIGGERS AUTOMÁTICAS (Auditoria LGPD e Integridade em Nível de SGBD)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Trigger 1: trg_auditoria_prontuario_insert (RN13, RQ07)
-- Grava automaticamente em logs_acesso sempre que uma sessão clínica é criada
-- ----------------------------------------------------------------------------
DROP TRIGGER IF EXISTS trg_auditoria_prontuario_insert;

DELIMITER //
CREATE TRIGGER trg_auditoria_prontuario_insert
AFTER INSERT ON sessoes_prontuario
FOR EACH ROW
BEGIN
    DECLARE v_id_medico INT;
    
    -- Descobre o médico vinculado à consulta
    SELECT id_profissional INTO v_id_medico 
    FROM agendamentos 
    WHERE id_agendamento = NEW.id_agendamento;

    INSERT INTO logs_acesso (
        id_usuario,
        id_paciente,
        operacao,
        entidade,
        id_registro,
        detalhes,
        ip_origem,
        data_hora
    ) VALUES (
        COALESCE(NEW.id_usuario_alteracao, v_id_medico, 1),
        NEW.id_paciente,
        'CRIAR_PRONTUARIO',
        'SessaoProntuario',
        NEW.id_sessao,
        CONCAT('Nova sessao clinica vinculada a consulta #', NEW.id_agendamento),
        '127.0.0.1',
        CURRENT_TIMESTAMP(3)
    );
END //
DELIMITER ;

-- ----------------------------------------------------------------------------
-- Trigger 2: trg_auditoria_prontuario_update (RN13, RN17, RQ07)
-- Trilha imutável em logs_acesso a cada edição/versionamento de prontuário
-- ----------------------------------------------------------------------------
DROP TRIGGER IF EXISTS trg_auditoria_prontuario_update;

DELIMITER //
CREATE TRIGGER trg_auditoria_prontuario_update
AFTER UPDATE ON sessoes_prontuario
FOR EACH ROW
BEGIN
    IF OLD.versao_atual <> NEW.versao_atual OR OLD.anotacoes_clinicas <> NEW.anotacoes_clinicas THEN
        INSERT INTO logs_acesso (
            id_usuario,
            id_paciente,
            operacao,
            entidade,
            id_registro,
            detalhes,
            ip_origem,
            data_hora
        ) VALUES (
            COALESCE(NEW.id_usuario_alteracao, 1),
            NEW.id_paciente,
            'ATUALIZAR_PRONTUARIO',
            'SessaoProntuario',
            NEW.id_sessao,
            CONCAT('Evolucao clinica versionada para v', NEW.versao_atual, '. Motivo: ', COALESCE(NEW.motivo_alteracao, 'Nao informado')),
            '127.0.0.1',
            CURRENT_TIMESTAMP(3)
        );
    END IF;
END //
DELIMITER ;

-- ----------------------------------------------------------------------------
-- Trigger 3: trg_auditoria_paciente_inativacao (RN16, RQ03)
-- Grava auditoria legal de inativação lógica e anonimização de dados pessoais
-- ----------------------------------------------------------------------------
DROP TRIGGER IF EXISTS trg_auditoria_paciente_inativacao;

DELIMITER //
CREATE TRIGGER trg_auditoria_paciente_inativacao
AFTER UPDATE ON pacientes
FOR EACH ROW
BEGIN
    IF OLD.ativo = 1 AND NEW.ativo = 0 THEN
        INSERT INTO logs_acesso (
            id_usuario,
            id_paciente,
            operacao,
            entidade,
            id_registro,
            detalhes,
            ip_origem,
            data_hora
        ) VALUES (
            COALESCE(NEW.id_profissional_responsavel, 1),
            NEW.id_paciente,
            'INATIVACAO_LGPD',
            'Paciente',
            NEW.id_paciente,
            CONCAT('Paciente inativado logicamente conforme LGPD (RN16). Documento anonimizado: ', IF(NEW.documento_identificacao IS NULL, 'SIM', 'NAO')),
            '127.0.0.1',
            CURRENT_TIMESTAMP(3)
        );
    END IF;
END //
DELIMITER ;

-- ============================================================================
-- 3. STORED PROCEDURES (Regras de Negócio no SGBD)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Procedure 1: sp_cancelar_consulta (RF08, RN10)
-- Valida integridade e calcula automaticamente cancelamento tardio (< 24h)
-- ----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS sp_cancelar_consulta;

DELIMITER //
CREATE PROCEDURE sp_cancelar_consulta(
    IN p_id_agendamento INT,
    IN p_motivo VARCHAR(255)
)
BEGIN
    DECLARE v_status VARCHAR(20);
    DECLARE v_inicio DATETIME;
    DECLARE v_tardio TINYINT(1) DEFAULT 0;

    -- Localiza o agendamento
    SELECT status, data_hora_inicio 
    INTO v_status, v_inicio
    FROM agendamentos 
    WHERE id_agendamento = p_id_agendamento;

    IF v_status IS NULL THEN
        SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Erro de Negocio: Agendamento nao encontrado.';
    END IF;

    IF v_status = 'Cancelado' THEN
        SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Erro de Negocio: Consulta ja se encontra cancelada.';
    END IF;

    IF v_status = 'Concluido' THEN
        SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Erro de Negocio: Nao e possivel cancelar uma consulta ja concluida.';
    END IF;

    -- RN10: Verifica se o cancelamento ocorre com menos de 24 horas de antecedência
    IF TIMESTAMPDIFF(HOUR, NOW(), v_inicio) < 24 THEN
        SET v_tardio = 1;
    END IF;

    -- Atualiza status e marcação de cancelamento tardio
    UPDATE agendamentos
    SET status = 'Cancelado',
        cancelamento_tardio = v_tardio,
        data_cancelamento = NOW(),
        observacoes = CONCAT(COALESCE(observacoes, ''), ' | Motivo Cancelamento: ', p_motivo)
    WHERE id_agendamento = p_id_agendamento;
END //
DELIMITER ;

-- ----------------------------------------------------------------------------
-- Procedure 2: sp_fechamento_mensal (RF22, RN09)
-- Calcula indicadores agregados do mês e consolida na tabela oficial
-- ----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS sp_fechamento_mensal;

DELIMITER //
CREATE PROCEDURE sp_fechamento_mensal(
    IN p_id_profissional INT,
    IN p_ano SMALLINT,
    IN p_mes TINYINT
)
BEGIN
    DECLARE v_total_atendimentos INT DEFAULT 0;
    DECLARE v_total_faturado DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_pendente DECIMAL(12,2) DEFAULT 0.00;

    -- Consolida números a partir dos agendamentos e pagamentos reais
    SELECT 
        COUNT(a.id_agendamento),
        COALESCE(SUM(CASE WHEN pag.status = 'Pago' THEN pag.valor ELSE 0 END), 0.00),
        COALESCE(SUM(CASE WHEN pag.status = 'Pendente' THEN pag.valor ELSE 0 END), 0.00)
    INTO v_total_atendimentos, v_total_faturado, v_total_pendente
    FROM agendamentos a
    LEFT JOIN pagamentos pag ON a.id_agendamento = pag.id_agendamento
    WHERE a.id_profissional = p_id_profissional
      AND YEAR(a.data_hora_inicio) = p_ano
      AND MONTH(a.data_hora_inicio) = p_mes;

    -- Salva ou atualiza o fechamento consolidado
    INSERT INTO resumos_financeiros_mensais (
        id_profissional,
        ano,
        mes,
        total_atendimentos,
        total_faturado,
        total_pendente,
        gerado_em
    ) VALUES (
        p_id_profissional,
        p_ano,
        p_mes,
        v_total_atendimentos,
        v_total_faturado,
        v_total_pendente,
        NOW()
    )
    ON DUPLICATE KEY UPDATE
        total_atendimentos = VALUES(total_atendimentos),
        total_faturado = VALUES(total_faturado),
        total_pendente = VALUES(total_pendente),
        gerado_em = NOW();

    -- Retorna o registro consolidado
    SELECT * 
    FROM resumos_financeiros_mensais
    WHERE id_profissional = p_id_profissional 
      AND ano = p_ano 
      AND mes = p_mes;
END //
DELIMITER ;
