-- ============================================================================
-- PROJETO INTEGRADOR - ENGENHARIA DE SOFTWARE (CEUB)
-- Disciplina: Banco de Dados II (MySQL) & POO (C# / .NET 8)
-- Script DML: Carga de Dados Iniciais de Demonstração (Seed Completo)
--
-- Compatível com os dados de demonstração do sistema Clinix
-- Cobre: Usuários, Especialização, Pacientes, Base Legal LGPD,
--        Agendamentos, Sessões de Prontuário, Histórico de Versões,
--        Pagamentos, Notificações, Bloqueios e Resumos Financeiros.
-- ============================================================================

USE clinix_db;

-- Desativa temporariamente checagens para carga limpa e idempotente
SET FOREIGN_KEY_CHECKS = 0;

-- 1. USUÁRIOS (Hash PBKDF2/SHA-256 + Salt demonstrativo para senhas de teste)
INSERT INTO usuarios (id_usuario, nome, login, senha_hash, perfil, ativo, criado_em) VALUES
(1, 'Dr. Davi Felinto', 'davi.profissional@clinix.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'Profissional', 1, NOW()),
(2, 'Juliana Costa', 'admin@clinix.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'Administrador', 1, NOW()),
(3, 'Dr. Roberto Silva', 'roberto.med', '3b612c75a7b5c85d6176d4c45a0fc11a5ff02f26708f473a30f320219ac838b3', 'Profissional', 1, NOW()),
(4, 'Ana Costa', 'ana.admin', '3b612c75a7b5c85d6176d4c45a0fc11a5ff02f26708f473a30f320219ac838b3', 'Administrador', 1, NOW())
ON DUPLICATE KEY UPDATE nome = VALUES(nome), login = VALUES(login), senha_hash = VALUES(senha_hash), perfil = VALUES(perfil), ativo = VALUES(ativo);

-- 2. ESPECIALIZAÇÃO DE USUÁRIOS
INSERT INTO profissionais_saude (id_usuario, perfil, registro_profissional, especialidade) VALUES
(1, 'Profissional', 'CRP-DF 12345', 'Psicologia Clínica & Neuropsicologia'),
(3, 'Profissional', 'CRM-DF 98765', 'Clínica Geral')
ON DUPLICATE KEY UPDATE registro_profissional = VALUES(registro_profissional), especialidade = VALUES(especialidade);

INSERT INTO administradores (id_usuario, perfil, cargo) VALUES
(2, 'Administrador', 'Gestão & Recepção Clínica'),
(4, 'Administrador', 'Gerente de Operações')
ON DUPLICATE KEY UPDATE cargo = VALUES(cargo);

-- 3. PACIENTES (RF01-RF05, RN16)
INSERT INTO pacientes (id_paciente, id_profissional_responsavel, nome, documento_identificacao, data_nascimento, telefone, email, endereco, alergias, condicoes_preexistentes, ativo, data_cadastro) VALUES
(1, 1, 'Mariana Duarte Souza', '123.456.789-00', '1992-05-14', '(61) 98765-4321', 'mariana.duarte@email.com', 'Asa Norte, Bloco C, Apto 302 - Brasília/DF', 'Penicilina, Dipirona', 'Rinite alérgica crônica, Ansiedade generalizada', 1, NOW()),
(2, 1, 'Carlos Eduardo Neves', '987.654.321-11', '1985-11-20', '(61) 99123-9988', 'carlos.neves@email.com', 'Águas Claras, Rua 24 Sul - Brasília/DF', 'Nenhuma conhecida', 'Hipertensão leve', 1, NOW()),
(3, 1, 'Beatriz Vasconcelos Ramos', '456.789.012-33', '1998-03-08', '(61) 98222-1144', 'beatriz.ramos@email.com', 'Sudoeste, QMSW 5, Bloco B - Brasília/DF', 'Frutos do mar', 'Insônia pontual', 1, NOW()),
(4, 1, 'Davi Felinto', '097.327.679-90', '2004-09-07', '(61) 99912-9760', 'davifd0978@gmail.com', 'Brasília/DF', 'Nenhuma conhecida', 'Check-up anual de rotina', 1, NOW())
ON DUPLICATE KEY UPDATE nome = VALUES(nome), documento_identificacao = VALUES(documento_identificacao), telefone = VALUES(telefone), email = VALUES(email), endereco = VALUES(endereco), alergias = VALUES(alergias), condicoes_preexistentes = VALUES(condicoes_preexistentes), ativo = VALUES(ativo);

-- 4. BASE LEGAL LGPD (RQ01, RQ02)
INSERT INTO registros_base_legal (id_registro, id_paciente, base_legal, finalidade, observacoes, data_registro, id_usuario_registro) VALUES
(1, 1, 'TutelaDeSaude', 'Tratamento psicoterapêutico contínuo', 'Consentimento colhido na admissão clínica', NOW(), 1),
(2, 2, 'TutelaDeSaude', 'Acompanhamento clínico geral', 'Ficha clínica arquivada', NOW(), 1),
(3, 3, 'TutelaDeSaude', 'Avaliação inicial', 'Consentimento assinado', NOW(), 1),
(4, 4, 'TutelaDeSaude', 'Acompanhamento preventivo', 'Consentimento colhido via portal', NOW(), 1)
ON DUPLICATE KEY UPDATE finalidade = VALUES(finalidade), observacoes = VALUES(observacoes);

-- 5. AGENDAMENTOS (RF06-RF11, RN01, RN02, RN10)
-- Consultas distribuídas entre passadas (para prontuário), hoje e futuras
INSERT INTO agendamentos (id_agendamento, id_paciente, id_profissional, data_hora_inicio, data_hora_fim, status, observacoes, cancelamento_tardio, data_cancelamento, criado_em) VALUES
(1, 1, 1, DATE_ADD(CURRENT_DATE(), INTERVAL '1 14' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '1 15' DAY_HOUR), 'Confirmado', 'Acompanhamento quinzenal com foco em manejo de estresse', 0, NULL, NOW()),
(2, 2, 1, DATE_ADD(CURRENT_DATE(), INTERVAL '0 10:30' DAY_MINUTE), DATE_ADD(CURRENT_DATE(), INTERVAL '0 11:30' DAY_MINUTE), 'Confirmado', 'Reavaliação dos exames laboratoriais e controle de pressão arterial', 0, NULL, NOW()),
(3, 3, 1, DATE_ADD(CURRENT_DATE(), INTERVAL '2 15' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '2 16' DAY_HOUR), 'Pendente', 'Primeira consulta de avaliação e triagem clínica', 0, NULL, NOW()),
(4, 4, 1, DATE_ADD(CURRENT_DATE(), INTERVAL '3 09' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '3 10' DAY_HOUR), 'Confirmado', 'Consulta de rotina e emissão de atestado ocupacional', 0, NULL, NOW()),
(5, 2, 1, DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '14' HOUR), INTERVAL 7 DAY), DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '15' HOUR), INTERVAL 7 DAY), 'Concluido', 'Consulta realizada - Investigação de cefaleia tensional e aferição de PA', 0, NULL, DATE_SUB(NOW(), INTERVAL 7 DAY)),
(6, 1, 1, DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '10' HOUR), INTERVAL 14 DAY), DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '11' HOUR), INTERVAL 14 DAY), 'Concluido', 'Sessão clínica de acompanhamento - Evolução de quadro ansioso', 0, NULL, DATE_SUB(NOW(), INTERVAL 14 DAY)),
(7, 3, 1, DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '16' HOUR), INTERVAL 1 DAY), DATE_SUB(DATE_ADD(CURRENT_DATE(), INTERVAL '17' HOUR), INTERVAL 1 DAY), 'Cancelado', 'Cancelamento comunicado 2 horas antes da sessão', 1, DATE_SUB(NOW(), INTERVAL 1 DAY), DATE_SUB(NOW(), INTERVAL 3 DAY))
ON DUPLICATE KEY UPDATE data_hora_inicio = VALUES(data_hora_inicio), data_hora_fim = VALUES(data_hora_fim), status = VALUES(status), observacoes = VALUES(observacoes), cancelamento_tardio = VALUES(cancelamento_tardio);

-- 6. COBRANÇA FINANCEIRA (RF19-RF22, RN07, RN08)
INSERT INTO pagamentos (id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento, criado_em) VALUES
(1, 1, 200.00, 'Pendente', NULL, NULL, NOW()),
(2, 2, 180.00, 'Pago', 'Pix', NOW(), NOW()),
(3, 3, 200.00, 'Pendente', NULL, NULL, NOW()),
(4, 4, 250.00, 'Pendente', NULL, NULL, NOW()),
(5, 5, 200.00, 'Pago', 'CartaoCredito', DATE_SUB(NOW(), INTERVAL 7 DAY), DATE_SUB(NOW(), INTERVAL 7 DAY)),
(6, 6, 200.00, 'Pago', 'Dinheiro', DATE_SUB(NOW(), INTERVAL 14 DAY), DATE_SUB(NOW(), INTERVAL 14 DAY)),
(7, 7, 100.00, 'Pago', 'Pix', DATE_SUB(NOW(), INTERVAL 1 DAY), DATE_SUB(NOW(), INTERVAL 3 DAY))
ON DUPLICATE KEY UPDATE valor = VALUES(valor), status = VALUES(status), forma_pagamento = VALUES(forma_pagamento), data_pagamento = VALUES(data_pagamento);

-- 7. NOTIFICAÇÕES WHATSAPP (RF12, RN03)
INSERT INTO notificacoes (id_notificacao, id_agendamento, tipo, canal, data_hora_agendada, data_hora_envio, status, tentativas_envio, alerta_agenda, mensagem) VALUES
(1, 1, 'Confirmacao', 'WhatsApp', NOW(), NOW(), 'Enviado', 1, 0, 'Olá, Mariana! Sua consulta foi agendada para amanhã às 14:00.'),
(2, 2, 'Lembrete', 'WhatsApp', NOW(), NOW(), 'Enviado', 1, 0, 'Lembrete Clinix: Olá Carlos, sua consulta é hoje às 10:30.'),
(3, 3, 'Confirmacao', 'WhatsApp', NOW(), NULL, 'Pendente', 0, 0, 'Olá Beatriz! Sua solicitação de consulta está aguardando confirmação.'),
(4, 4, 'Confirmacao', 'WhatsApp', NOW(), NOW(), 'Enviado', 1, 0, 'Olá Davi! Sua consulta está confirmada para daqui a 3 dias às 09:00.'),
(5, 7, 'Lembrete', 'WhatsApp', DATE_SUB(NOW(), INTERVAL 1 DAY), DATE_SUB(NOW(), INTERVAL 1 DAY), 'Falha', 2, 1, 'Tentativa de lembrete falhou: cancelamento tardio registrado pelo paciente.')
ON DUPLICATE KEY UPDATE status = VALUES(status), mensagem = VALUES(mensagem);

-- 8. SESSÕES DE PRONTUÁRIO (RF13-RF18, RN05, RN17)
INSERT INTO sessoes_prontuario (id_sessao, id_paciente, id_agendamento, data_registro, anotacoes_clinicas, informacoes_clinicas_relevantes, versao_atual, data_ultima_alteracao, motivo_alteracao, id_usuario_alteracao) VALUES
(1, 1, 6, DATE_SUB(NOW(), INTERVAL 14 DAY),
 'Paciente relata remissão das crises de pânico e melhora significativa na qualidade do sono. Foi mantida a prática do diário de pensamentos disfuncionais.',
 'Nega reações adversas medicamentosas. Humor eutímico, orientada no tempo e espaço.',
 1, NULL, NULL, NULL),
(2, 2, 5, DATE_SUB(NOW(), INTERVAL 7 DAY),
 'Paciente compareceu para reavaliação clínica. PA aferida em 135x85 mmHg. Relata discreta cefaleia matinal aos finais de semana. Orientado a manter restrição sódica e retorno em 30 dias para mapa de pressão.',
 'Histórico familiar positivo para AVC precoce (materno). Sedentário.',
 2, DATE_SUB(NOW(), INTERVAL 6 DAY), 'Complementação do histórico familiar de AVC relatado pelo paciente na checagem laboratorial (RN17)', 1)
ON DUPLICATE KEY UPDATE anotacoes_clinicas = VALUES(anotacoes_clinicas), versao_atual = VALUES(versao_atual);

-- 9. HISTÓRICO DE VERSÕES DE PRONTUÁRIO (RN17 - Imutabilidade e Auditoria Clínica)
INSERT INTO historico_versoes_prontuario (id_versao, id_sessao, numero_versao, texto_anterior, informacoes_clinicas_anteriores, data_modificacao, motivo_alteracao, id_usuario) VALUES
(1, 2, 1,
 'Paciente compareceu para reavaliação clínica. PA aferida em 135x85 mmHg. Relata discreta cefaleia matinal aos finais de semana. Orientado a manter restrição sódica.',
 'Sedentário.',
 DATE_SUB(NOW(), INTERVAL 6 DAY),
 'Complementação do histórico familiar de AVC relatado pelo paciente na checagem laboratorial (RN17)',
 1)
ON DUPLICATE KEY UPDATE motivo_alteracao = VALUES(motivo_alteracao);

-- 10. BLOQUEIOS DE AGENDA (RF11 - Gestão de Intervalos)
INSERT INTO bloqueios_agenda (id_bloqueio, id_profissional, tipo, data_hora_inicio, data_hora_fim, descricao, id_usuario_criacao, criado_em) VALUES
(1, 1, 'Almoco', DATE_ADD(CURRENT_DATE(), INTERVAL '0 12' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '0 13' DAY_HOUR), 'Intervalo de Almoço / Estudo Clínico (RF11)', 1, NOW()),
(2, 1, 'CompromissoPessoal', DATE_ADD(CURRENT_DATE(), INTERVAL '4 16' DAY_HOUR), DATE_ADD(CURRENT_DATE(), INTERVAL '4 18' DAY_HOUR), 'Reunião de Alinhamento Multidisciplinar', 1, NOW())
ON DUPLICATE KEY UPDATE descricao = VALUES(descricao);

-- 11. AUDITORIA LGPD (RF28, RN13, RQ07)
INSERT INTO logs_acesso (id_log, id_usuario, id_paciente, operacao, entidade, id_registro, detalhes, ip_origem, data_hora) VALUES
(1, 1, 1, 'Autenticacao (RF26)', 'Usuario', 1, 'Login realizado com sucesso como Profissional de Saúde', '127.0.0.1', NOW(3)),
(2, 1, 1, 'Acesso ao Prontuario (RN13)', 'SessaoProntuario', 1, 'Ficha médica consultada: Mariana Duarte Souza', '127.0.0.1', NOW(3)),
(3, 1, 2, 'Edicao de Prontuario (RN17)', 'SessaoProntuario', 2, 'Anotação aditada com justificativa: Complementação de histórico familiar', '127.0.0.1', NOW(3)),
(4, 2, NULL, 'Consulta Financeira (RF21)', 'Pagamento', NULL, 'Acesso ao relatório de faturamento consolidado da clínica', '127.0.0.1', NOW(3))
ON DUPLICATE KEY UPDATE detalhes = VALUES(detalhes);

-- 12. RESUMOS FINANCEIROS MENSAIS (RF22, RN09)
INSERT INTO resumos_financeiros_mensais (id_resumo, id_profissional, ano, mes, total_atendimentos, total_faturado, total_pendente, gerado_em) VALUES
(1, 1, 2026, 9, 12, 2400.00, 0.00, '2026-10-01 00:00:00'),
(2, 1, 2026, 10, 6, 1130.00, 650.00, NOW())
ON DUPLICATE KEY UPDATE total_faturado = VALUES(total_faturado), total_pendente = VALUES(total_pendente);

SET FOREIGN_KEY_CHECKS = 1;
