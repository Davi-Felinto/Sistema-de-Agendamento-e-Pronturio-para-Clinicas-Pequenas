-- ============================================================================
-- PROJETO INTEGRADOR - ENGENHARIA DE SOFTWARE (CEUB)
-- Disciplina: Banco de Dados II (MySQL) & POO (C# / .NET 8)
-- Script DML: Carga de Dados Iniciais de Demonstração (Seed)
--
-- Compatível com os dados de demonstração do sistema Clinix
-- ============================================================================

USE clinix_db;

-- 1. USUÁRIOS (Hash PBKDF2/SHA-256 + Salt demonstrativo para senhas de teste)
INSERT INTO usuarios (id_usuario, nome, login, senha_hash, perfil, ativo, criado_em) VALUES
(1, 'Dr. Davi Felinto', 'davi.profissional@clinix.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'Profissional', 1, NOW()),
(2, 'Juliana Costa', 'admin@clinix.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'Administrador', 1, NOW()),
(3, 'Dr. Roberto Silva', 'roberto.med', '3b612c75a7b5c85d6176d4c45a0fc11a5ff02f26708f473a30f320219ac838b3', 'Profissional', 1, NOW()),
(4, 'Ana Costa', 'ana.admin', '3b612c75a7b5c85d6176d4c45a0fc11a5ff02f26708f473a30f320219ac838b3', 'Administrador', 1, NOW());

-- 2. ESPECIALIZAÇÃO DE USUÁRIOS
INSERT INTO profissionais_saude (id_usuario, perfil, registro_profissional, especialidade) VALUES
(1, 'Profissional', 'CRP-DF 12345', 'Psicologia Clínica & Neuropsicologia'),
(3, 'Profissional', 'CRM-DF 98765', 'Clínica Geral');

INSERT INTO administradores (id_usuario, perfil, cargo) VALUES
(2, 'Administrador', 'Gestão & Recepção Clínica'),
(4, 'Administrador', 'Gerente de Operações');

-- 3. PACIENTES
INSERT INTO pacientes (id_paciente, id_profissional_responsavel, nome, documento_identificacao, data_nascimento, telefone, email, endereco, alergias, condicoes_preexistentes, ativo, data_cadastro) VALUES
(1, 1, 'Mariana Duarte Souza', '123.456.789-00', '1992-05-14', '(61) 98765-4321', 'mariana.duarte@email.com', 'Asa Norte, Bloco C, Apto 302 - Brasília/DF', 'Penicilina, Dipirona', 'Rinite alérgica crônica, Ansiedade generalizada', 1, NOW()),
(2, 1, 'Carlos Eduardo Neves', '987.654.321-11', '1985-11-20', '(61) 99123-9988', 'carlos.neves@email.com', 'Águas Claras, Rua 24 Sul - Brasília/DF', 'Nenhuma conhecida', 'Hipertensão leve', 1, NOW()),
(3, 1, 'Beatriz Vasconcelos Ramos', '456.789.012-33', '1998-03-08', '(61) 98222-1144', 'beatriz.ramos@email.com', 'Sudoeste, QMSW 5, Bloco B - Brasília/DF', 'Frutos do mar', 'Insônia pontual', 1, NOW());

-- 4. BASE LEGAL LGPD
INSERT INTO registros_base_legal (id_registro, id_paciente, base_legal, finalidade, observacoes, data_registro, id_usuario_registro) VALUES
(1, 1, 'TutelaDeSaude', 'Tratamento psicoterapêutico contínuo', 'Consentimento colhido na admissão clínica', NOW(), 1),
(2, 2, 'TutelaDeSaude', 'Acompanhamento clínico geral', 'Ficha clínica arquivada', NOW(), 1),
(3, 3, 'TutelaDeSaude', 'Avaliação inicial', 'Consentimento assinado', NOW(), 1);

-- 5. AGENDAMENTOS (Consulta de teste para amanhã às 14h)
INSERT INTO agendamentos (id_agendamento, id_paciente, id_profissional, data_hora_inicio, data_hora_fim, status, observacoes, cancelamento_tardio, criado_em) VALUES
(1, 1, 1, DATE_ADD(CURRENT_DATE(), INTERVAL '1 14' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '1 15' DAY_HOUR), 'Confirmado', 'Acompanhamento quinzenal com foco em manejo de estresse', 0, NOW());

-- 6. COBRANÇA FINANCEIRA (RN07)
INSERT INTO pagamentos (id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento, criado_em) VALUES
(1, 1, 200.00, 'Pendente', NULL, NULL, NOW());

-- 7. NOTIFICAÇÃO WHATSAPP (RF12)
INSERT INTO notificacoes (id_notificacao, id_agendamento, tipo, canal, data_hora_agendada, data_hora_envio, status, tentativas_envio, alerta_agenda, mensagem) VALUES
(1, 1, 'Confirmacao', 'WhatsApp', NOW(), NOW(), 'Enviado', 1, 0, 'Olá, Mariana Duarte Souza! Sua consulta foi agendada para amanhã às 14:00.');

-- 8. AUDITORIA LGPD (RF28, RN13, RQ07)
INSERT INTO logs_acesso (id_log, id_usuario, id_paciente, operacao, entidade, id_registro, detalhes, ip_origem, data_hora) VALUES
(1, 1, 1, 'Autenticacao (RF26)', 'Usuario', 1, 'Login realizado com sucesso', '127.0.0.1', NOW(3)),
(2, 1, 1, 'Acesso ao Prontuario (RN13)', 'SessaoProntuario', 1, 'Ficha médica consultada: Mariana Duarte Souza', '127.0.0.1', NOW(3));
