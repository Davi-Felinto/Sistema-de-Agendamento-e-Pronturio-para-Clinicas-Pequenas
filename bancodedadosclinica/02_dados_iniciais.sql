-- ============================================================================
-- PROJETO INTEGRADOR - ENGENHARIA DE SOFTWARE (CEUB)
-- Disciplina: Banco de Dados II (MySQL) & POO (C# / .NET 8)
-- Script DML: Carga Completa de Demonstração para Todo o Mês (Outubro/2026)
--
-- Cobre: 12 Pacientes, 37 Consultas no mês (passadas, hoje e futuras),
--        Prontuários com Versionamento (RN17), Cobranças (RN07/RN08),
--        Notificações WhatsApp (RF12), Bloqueios de Agenda (RF11),
--        Logs de Auditoria LGPD (RN13/RQ07) e Fechamentos Financeiros.
-- ============================================================================

USE clinix_db;

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- 1. USUÁRIOS DO SISTEMA (RF26, RQ08)
-- ----------------------------------------------------------------------------
INSERT INTO usuarios (id_usuario, nome, login, senha_hash, perfil, ativo, criado_em) VALUES
(1, 'Dr. Davi Felinto',      'davi@clinix.com',    '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Profissional',  1, '2026-09-01 08:00:00'),
(2, 'Lucas Pereira',        'lucas@clinix.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Administrador', 1, '2026-09-01 08:00:00'),
(3, 'Dr. Miguel Silva',     'miguel@clinix.com',  '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Profissional',  1, '2026-09-01 08:00:00'),
(4, 'Isaac Santos',         'isaac@clinix.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Administrador', 1, '2026-09-01 08:00:00'),
(5, 'Dra. Vanessa Oliveira', 'vanessa@clinix.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Profissional',  1, '2026-09-01 08:00:00'),
(6, 'Juliana Costa',        'juliana@clinix.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Administrador', 1, '2026-09-01 08:00:00')
ON DUPLICATE KEY UPDATE nome = VALUES(nome), login = VALUES(login), senha_hash = VALUES(senha_hash), perfil = VALUES(perfil), ativo = VALUES(ativo);

INSERT INTO profissionais_saude (id_usuario, perfil, registro_profissional, especialidade) VALUES
(1, 'Profissional', 'CRP-DF 12345', 'Psicologia Clínica & Neuropsicologia'),
(3, 'Profissional', 'CRM-DF 54321', 'Clínica Geral & Telemedicina'),
(5, 'Profissional', 'CRM-DF 67890', 'Pediatria & Saúde da Família')
ON DUPLICATE KEY UPDATE registro_profissional = VALUES(registro_profissional), especialidade = VALUES(especialidade);

INSERT INTO administradores (id_usuario, perfil, cargo) VALUES
(2, 'Administrador', 'Engenharia de Requisitos & Gestão de Processos'),
(4, 'Administrador', 'Administrador de Banco de Dados (DBA) & Auditor LGPD'),
(6, 'Administrador', 'Recepção & Atendimento ao Paciente')
ON DUPLICATE KEY UPDATE cargo = VALUES(cargo);


-- ----------------------------------------------------------------------------
-- 2. PACIENTES CADASTRADOS (RF01-RF05, RN16)
-- ----------------------------------------------------------------------------
INSERT INTO pacientes (id_paciente, id_profissional_responsavel, nome, documento_identificacao, data_nascimento, telefone, email, endereco, alergias, condicoes_preexistentes, ativo, data_cadastro) VALUES
(1,  1, 'Mariana Duarte Souza',      '123.456.789-00', '1992-05-14', '(61) 98765-4321', 'mariana.duarte@email.com',  'Asa Norte, Bloco C, Apto 302 - Brasília/DF',     'Penicilina, Dipirona', 'Rinite alérgica crônica, Ansiedade generalizada', 1, '2026-08-15 10:00:00'),
(2,  1, 'Carlos Eduardo Neves',      '987.654.321-11', '1985-11-20', '(61) 99123-9988', 'carlos.neves@email.com',   'Águas Claras, Rua 24 Sul - Brasília/DF',        'Nenhuma conhecida',    'Hipertensão leve, Sobrepeso',                     1, '2026-08-20 14:30:00'),
(3,  1, 'Beatriz Vasconcelos Ramos',  '456.789.012-33', '1998-03-08', '(61) 98222-1144', 'beatriz.ramos@email.com',  'Sudoeste, QMSW 5, Bloco B - Brasília/DF',        'Frutos do mar',        'Insônia pontual, Cefaleia tensional',             1, '2026-08-25 09:15:00'),
(4,  1, 'Davi Felinto',              '097.327.679-90', '2004-09-07', '(61) 99912-9760', 'davifd0978@gmail.com',     'Asa Sul, SQS 308 - Brasília/DF',                 'Nenhuma conhecida',    'Check-up anual, Saúde preventiva',                1, '2026-09-01 11:00:00'),
(5,  1, 'Lucas Henrique Pereira',    '234.567.890-12', '1990-07-22', '(61) 98455-1234', 'lucas.pereira@email.com',  'Asa Sul, SQS 412, Bloco K - Brasília/DF',        'Dipirona',             'Enxaqueca crônica com aura',                      1, '2026-09-05 16:00:00'),
(6,  1, 'Juliana Mendes Carvalho',   '345.678.901-23', '1988-12-05', '(61) 99344-5678', 'juliana.mendes@email.com', 'Guará II, QE 30, Conjunto A - Guará/DF',        'Nenhuma conhecida',    'Hipotireoidismo em tratamento',                   1, '2026-09-10 10:45:00'),
(7,  1, 'Rafael Guimarães Castro',   '567.890.123-45', '2001-04-18', '(61) 98111-7788', 'rafael.castro@email.com',  'Taguatinga Norte, QND 14 - Taguatinga/DF',       'Ibuprofeno',           'Reabilitação de ligamento cruzado anterior',      1, '2026-09-12 15:20:00'),
(8,  1, 'Camila Silveira Rocha',     '678.901.234-56', '1995-09-30', '(61) 99555-8899', 'camila.rocha@email.com',   'Lago Norte, SHIN QL 8 - Brasília/DF',            'Amoxicilina',          'Burnout ocupacional, Ansiedade',                  1, '2026-09-15 08:30:00'),
(9,  1, 'Gabriel Monteiro Dias',     '789.012.345-67', '1982-01-15', '(61) 98777-3344', 'gabriel.dias@email.com',   'Lago Sul, SHIS QL 12 - Brasília/DF',             'Nenhuma conhecida',    'Diabetes Mellitus Tipo 2, Hipertensão',           1, '2026-09-18 13:00:00'),
(10, 1, 'Fernanda Pires Oliveira',   '890.123.456-78', '1993-06-25', '(61) 99222-4455', 'fernanda.pires@email.com', 'Asa Norte, SQN 206, Bloco H - Brasília/DF',     'Nenhuma conhecida',    'Acompanhamento puerperal pós-parto',              1, '2026-09-20 17:10:00'),
(11, 1, 'Thiago Alcantara Ribeiro',  '901.234.567-89', '1979-08-11', '(61) 98888-6677', 'thiago.ribeiro@email.com', 'Sobradinho, Quadra 04 - Sobradinho/DF',         'Sulfa',                'Lombalgia mecânico-postural, Sedentarismo',        1, '2026-09-22 11:30:00'),
(12, 1, 'Larissa Fontana Morais',    '012.345.678-90', '2003-11-03', '(61) 99444-2211', 'larissa.morais@email.com', 'Plano Piloto, SCN Quadra 1 - Brasília/DF',        'Nenhuma conhecida',    'TDAH subtipo desatento (em avaliação)',           1, '2026-09-25 14:00:00')
ON DUPLICATE KEY UPDATE nome = VALUES(nome), documento_identificacao = VALUES(documento_identificacao), telefone = VALUES(telefone), email = VALUES(email), endereco = VALUES(endereco), alergias = VALUES(alergias), condicoes_preexistentes = VALUES(condicoes_preexistentes), ativo = VALUES(ativo);

-- ----------------------------------------------------------------------------
-- 3. REGISTROS DE BASE LEGAL LGPD (RQ01, RQ02)
-- ----------------------------------------------------------------------------
INSERT INTO registros_base_legal (id_registro, id_paciente, base_legal, finalidade, observacoes, data_registro, id_usuario_registro) VALUES
(1,  1,  'TutelaDeSaude', 'Tratamento psicoterapêutico contínuo', 'Termo de admissão clínica assinado', '2026-08-15 10:05:00', 1),
(2,  2,  'TutelaDeSaude', 'Acompanhamento clínico geral e cardiovascular', 'Ficha clínica arquivada', '2026-08-20 14:35:00', 1),
(3,  3,  'TutelaDeSaude', 'Manejo de distúrbio do sono e triagem', 'Consentimento colhido na recepção', '2026-08-25 09:20:00', 1),
(4,  4,  'TutelaDeSaude', 'Acompanhamento preventivo e check-up', 'Consentimento digital confirmado', '2026-09-01 11:05:00', 1),
(5,  5,  'TutelaDeSaude', 'Investigação neurológica de cefaleias', 'Consentimento livre e esclarecido', '2026-09-05 16:05:00', 1),
(6,  6,  'TutelaDeSaude', 'Acompanhamento endocrinológico e clínico', 'Termo de consentimento firmado', '2026-09-10 10:50:00', 1),
(7,  7,  'TutelaDeSaude', 'Fisioterapia e reabilitação ortopédica', 'Consentimento para manipulação física', '2026-09-12 15:25:00', 1),
(8,  8,  'TutelaDeSaude', 'Psicoterapia para estresse ocupacional', 'Consentimento assinado', '2026-09-15 08:35:00', 1),
(9,  9,  'TutelaDeSaude', 'Controle metabólico de diabetes e hipertensão', 'Protocolo assistencial', '2026-09-18 13:05:00', 1),
(10, 10, 'TutelaDeSaude', 'Acompanhamento pós-parto e saúde da mulher', 'Termo assistencial', '2026-09-20 17:15:00', 1),
(11, 11, 'TutelaDeSaude', 'Tratamento fisioterápico para coluna', 'Consentimento de admissão', '2026-09-22 11:35:00', 1),
(12, 12, 'TutelaDeSaude', 'Avaliação neuropsicológica e psicometria', 'Consentimento dos responsáveis', '2026-09-25 14:05:00', 1)
ON DUPLICATE KEY UPDATE finalidade = VALUES(finalidade), observacoes = VALUES(observacoes);

-- ----------------------------------------------------------------------------
-- 4. AGENDAMENTOS DO MÊS INTEIRO (OUTUBRO/2026) (RF06-RF11, RN01, RN02, RN10)
-- ----------------------------------------------------------------------------
-- Semana 1 (01/10 a 02/10) - Realizadas
-- Semana 2 (05/10 a 09/10) - Realizadas e 1 cancelamento tardio
-- Dia Atual (10/10) - Consultas de Hoje
-- Semana 3 (13/10 a 16/10) - Próxima semana
-- Semana 4 (19/10 a 23/10) - Terceira semana
-- Semana 5 (26/10 a 30/10) - Fim do mês
INSERT INTO agendamentos (id_agendamento, id_paciente, id_profissional, data_hora_inicio, data_hora_fim, status, observacoes, cancelamento_tardio, data_cancelamento, criado_em) VALUES
-- Semana 1 (Passadas - Concluídas)
(1,  1,  1, '2026-10-01 09:00:00', '2026-10-01 10:00:00', 'Concluido', 'Sessão clínica de acompanhamento - Manejo de ansiedade', 0, NULL, '2026-09-20 10:00:00'),
(2,  2,  1, '2026-10-01 14:00:00', '2026-10-01 15:00:00', 'Concluido', 'Retorno clínico geral - Aferição de PA e rotina', 0, NULL, '2026-09-21 11:00:00'),
(3,  5,  1, '2026-10-02 10:30:00', '2026-10-02 11:30:00', 'Concluido', 'Avaliação inicial de enxaqueca crônica', 0, NULL, '2026-09-22 14:00:00'),
(4,  6,  1, '2026-10-02 15:00:00', '2026-10-02 16:00:00', 'Concluido', 'Controle de hipotireoidismo e revisão laboratorial', 0, NULL, '2026-09-23 09:30:00'),

-- Semana 2 (Passadas - Concluídas e 1 Cancelamento Tardio)
(5,  7,  1, '2026-10-05 09:00:00', '2026-10-05 10:00:00', 'Concluido', 'Reabilitação motora pós-cirúrgica de joelho', 0, NULL, '2026-09-25 08:00:00'),
(6,  8,  1, '2026-10-05 11:00:00', '2026-10-05 12:00:00', 'Concluido', 'Atendimento psicoterápico focado em burnout', 0, NULL, '2026-09-26 15:00:00'),
(7,  9,  1, '2026-10-05 14:30:00', '2026-10-05 15:30:00', 'Concluido', 'Controle glicêmico e metabólico do Diabetes', 0, NULL, '2026-09-27 10:00:00'),
(8,  10, 1, '2026-10-06 10:00:00', '2026-10-06 11:00:00', 'Concluido', 'Revisão puerperal de 60 dias', 0, NULL, '2026-09-28 11:30:00'),
(9,  11, 1, '2026-10-06 16:00:00', '2026-10-06 17:00:00', 'Concluido', 'Avaliação postural e contratura lombar', 0, NULL, '2026-09-29 09:00:00'),
(10, 12, 1, '2026-10-07 09:00:00', '2026-10-07 10:00:00', 'Concluido', 'Bateria de testagem neuropsicológica (Sessão 1)', 0, NULL, '2026-09-30 14:00:00'),
(11, 3,  1, '2026-10-07 14:00:00', '2026-10-07 15:00:00', 'Concluido', 'Consulta sobre insônia inicial e higiene do sono', 0, NULL, '2026-10-01 16:00:00'),
(12, 1,  1, '2026-10-08 10:30:00', '2026-10-08 11:30:00', 'Concluido', 'Terapia cognitivo-comportamental quinzenal', 0, NULL, '2026-10-02 10:00:00'),
(13, 2,  1, '2026-10-08 15:00:00', '2026-10-08 16:00:00', 'Concluido', 'Resultado do MAPA de pressão de 24h', 0, NULL, '2026-10-03 11:00:00'),
(14, 5,  1, '2026-10-09 09:00:00', '2026-10-09 10:00:00', 'Concluido', 'Retorno de conduta medicamentosa preventiva', 0, NULL, '2026-10-04 14:30:00'),
(15, 3,  1, '2026-10-09 16:00:00', '2026-10-09 17:00:00', 'Cancelado', 'Cancelamento comunicado com 2h de antecedência (RN10)', 1, '2026-10-09 14:00:00', '2026-10-05 10:00:00'),

-- Dia Atual (10/10/2026 - Sábado - Consultas de Hoje)
(16, 4,  1, '2026-10-10 09:00:00', '2026-10-10 10:00:00', 'Confirmado', 'Avaliação preventiva de rotina anual', 0, NULL, '2026-10-06 09:00:00'),
(17, 6,  1, '2026-10-10 10:30:00', '2026-10-10 11:30:00', 'Confirmado', 'Reavaliação de dosagem hormonal', 0, NULL, '2026-10-06 14:00:00'),
(18, 8,  1, '2026-10-10 14:00:00', '2026-10-10 15:00:00', 'Confirmado', 'Psicoterapia de suporte - Manejo de estressores', 0, NULL, '2026-10-07 10:00:00'),
(19, 7,  1, '2026-10-10 16:00:00', '2026-10-10 17:00:00', 'Confirmado', 'Exercícios proprioceptivos e fortalecimento', 0, NULL, '2026-10-07 15:30:00'),

-- Semana 3 (13/10 a 16/10 - Pós-Feriado)
(20, 9,  1, '2026-10-13 09:00:00', '2026-10-13 10:00:00', 'Confirmado', 'Avaliação de hemoglobina glicada e curva glicêmica', 0, NULL, '2026-10-08 08:30:00'),
(21, 10, 1, '2026-10-13 11:00:00', '2026-10-13 12:00:00', 'Confirmado', 'Consulta de puerpério tardio e planejamento', 0, NULL, '2026-10-08 11:00:00'),
(22, 12, 1, '2026-10-14 10:00:00', '2026-10-14 11:00:00', 'Confirmado', 'Aplicação de testes de atenção e memória operacional', 0, NULL, '2026-10-08 14:00:00'),
(23, 1,  1, '2026-10-14 14:00:00', '2026-10-14 15:00:00', 'Confirmado', 'Acompanhamento quinzenal - Avaliação de metas', 0, NULL, '2026-10-08 16:00:00'),
(24, 2,  1, '2026-10-15 09:30:00', '2026-10-15 10:30:00', 'Confirmado', 'Acompanhamento cardiológico de rotina', 0, NULL, '2026-10-09 09:00:00'),
(25, 11, 1, '2026-10-15 16:00:00', '2026-10-15 17:00:00', 'Pendente',   'Fisioterapia preventiva para coluna lombar', 0, NULL, '2026-10-09 10:30:00'),
(26, 5,  1, '2026-10-16 10:00:00', '2026-10-16 11:00:00', 'Confirmado', 'Checagem de resposta ao novo esquema de cefaleia', 0, NULL, '2026-10-09 15:00:00'),

-- Semana 4 (19/10 a 23/10)
(27, 6,  1, '2026-10-19 09:00:00', '2026-10-19 10:00:00', 'Confirmado', 'Consulta de manutenção clínica', 0, NULL, '2026-10-10 08:00:00'),
(28, 7,  1, '2026-10-19 14:00:00', '2026-10-19 15:00:00', 'Pendente',   'Reavaliação ortopédica para liberação de corrida', 0, NULL, '2026-10-10 08:30:00'),
(29, 8,  1, '2026-10-21 10:30:00', '2026-10-21 11:30:00', 'Confirmado', 'Sessão de psicoterapia - Reestruturação cognitiva', 0, NULL, '2026-10-10 09:00:00'),
(30, 3,  1, '2026-10-21 15:00:00', '2026-10-21 16:00:00', 'Confirmado', 'Remarcação da consulta cancelada em 09/10', 0, NULL, '2026-10-10 09:30:00'),
(31, 4,  1, '2026-10-23 09:00:00', '2026-10-23 10:00:00', 'Confirmado', 'Devolutiva do hemograma e bioquímica de rotina', 0, NULL, '2026-10-10 10:00:00'),
(32, 10, 1, '2026-10-23 11:00:00', '2026-10-23 12:00:00', 'Confirmado', 'Avaliação global de saúde feminina', 0, NULL, '2026-10-10 10:30:00'),

-- Semana 5 (26/10 a 30/10 - Reta final do mês)
(33, 12, 1, '2026-10-26 14:00:00', '2026-10-26 15:00:00', 'Confirmado', 'Devolutiva neuropsicológica e entrega de laudo (RF18)', 0, NULL, '2026-10-10 11:00:00'),
(34, 11, 1, '2026-10-27 16:00:00', '2026-10-27 17:00:00', 'Pendente',   'Reavaliação biomecânica e orientações de pilates', 0, NULL, '2026-10-10 11:30:00'),
(35, 1,  1, '2026-10-28 10:00:00', '2026-10-28 11:00:00', 'Confirmado', 'Fechamento de ciclo de acompanhamento psicológico', 0, NULL, '2026-10-10 12:00:00'),
(36, 9,  1, '2026-10-29 15:00:00', '2026-10-29 16:00:00', 'Confirmado', 'Reavaliação trimestral de controle cardiovascular', 0, NULL, '2026-10-10 12:30:00'),
(37, 2,  1, '2026-10-30 09:30:00', '2026-10-30 10:30:00', 'Confirmado', 'Consulta de manutenção e ajuste de anti-hipertensivo', 0, NULL, '2026-10-10 13:00:00')
ON DUPLICATE KEY UPDATE data_hora_inicio = VALUES(data_hora_inicio), data_hora_fim = VALUES(data_hora_fim), status = VALUES(status), observacoes = VALUES(observacoes), cancelamento_tardio = VALUES(cancelamento_tardio);

-- ----------------------------------------------------------------------------
-- 5. SESSÕES DE PRONTUÁRIO (RF13-RF18, RN05, RN17)
-- ----------------------------------------------------------------------------
-- Prontuários detalhados vinculados às consultas já realizadas no mês
INSERT INTO sessoes_prontuario (id_sessao, id_paciente, id_agendamento, data_registro, anotacoes_clinicas, informacoes_clinicas_relevantes, versao_atual, data_ultima_alteracao, motivo_alteracao, id_usuario_alteracao) VALUES
(1, 1, 1, '2026-10-01 10:00:00',
 'Paciente relata melhora sensível das crises de pânico. Boa adesão à técnica de respiração diafragmática. Sono mais regularizado (média de 7h/noite). Mantida conduta terapêutica com registro de pensamentos automáticos.',
 'Humor eutímico, orientada globalmente. Ausência de ideação autolesiva ou ideação suicida.',
 1, NULL, NULL, NULL),

(2, 2, 2, '2026-10-01 15:00:00',
 'Paciente em acompanhamento de hipertensão leve. PA aferida em consultório: 132x84 mmHg. FC: 72 bpm. Nega queixas de palpitações ou tonturas. Solicitado MAPA de 24h para elucidação de picos de pressão.',
 'Sedentário, histórico familiar paterno de infarto agudo do miocárdio aos 55 anos. Orientada redução de sódio na dieta.',
 1, NULL, NULL, NULL),

(3, 5, 3, '2026-10-02 11:30:00',
 'Primeira consulta. Queixa principal: episódios de cefaleia pulsátil unilateral à esquerda, acompanhada de fotofobia e náusea, com frequência de 3 vezes por semana há 4 meses. Diagnosticada migrânea sem aura.',
 'Alergia grave documentada a Dipirona. Prescrito triptano para crises e recomendado diário de dor.',
 1, NULL, NULL, NULL),

(4, 6, 4, '2026-10-02 16:00:00',
 'Paciente traz exames laboratoriais recentes. TSH: 2,75 mUI/L (valor de referência 0,4 - 4,0). T4 Livre: 1,15 ng/dL. Excelente controle do hipotireoidismo com Levotiroxina 75 mcg em jejum.',
 'Queixa de queda leve de cabelo associada a estresse. Solicitada dosagem de ferritina e vitamina D.',
 1, NULL, NULL, NULL),

(5, 7, 5, '2026-10-05 10:00:00',
 'Avaliação pós-operatória de LCA joelho direito (60 dias de pós-op). Boa evolução cicatricial. ADM ativa: flexão de 115 graus, extensão completa (0 graus). Ausência de derrame articular significativo.',
 'Iniciado protocolo de propriocepção e fortalecimento isométrico de quadríceps e isquiotibiais.',
 1, NULL, NULL, NULL),

(6, 8, 6, '2026-10-05 12:00:00',
 'Relata sobrecarga intensa no ambiente corporativo (gestão de projetos). Sintomas relatados: exaustão emocional crônica, despersonalização leve no trabalho e bruxismo noturno. Quadro compatível com Burnout.',
 'Alergia a Amoxicilina. Trabalhadas estratégias de estabelecimento de limites e pausas ativas na rotina.',
 1, NULL, NULL, NULL),

(7, 9, 7, '2026-10-05 15:30:00',
 'Consulta de controle de DM2. Glicemia de jejum recente: 118 mg/dL. HbA1c: 6,4% (meta < 7%). Boa adesão à Metformina 850mg 2x/dia. Exame dos pés sem sinais de neuropatia periférica ou lesões de pele.',
 'Pulsos pediosos e tibiais posteriores cheios e simétricos. Sensibilidade preservada ao monofilamento.',
 1, NULL, NULL, NULL),

(8, 10, 8, '2026-10-06 11:00:00',
 'Revisão puerperal de 60 dias pós-parto normal. Involução uterina completa. Cicatrização perineal íntegra. Amamentação exclusiva em livre demanda com boa pega e ganho ponderal satisfatório do lactente.',
 'Rastreio de depressão pós-parto negativo (Escala de Edimburgo: 3 pontos). Liberada para atividades físicas leves.',
 1, NULL, NULL, NULL),

(9, 11, 9, '2026-10-06 17:00:00',
 'Queixa de dor lombar baixa há 3 semanas, com piora ao permanecer sentado por mais de 2 horas. Teste de Lasègue negativo bilateralmente. Ausência de radiculopatia ou déficits motores/sensoriais.',
 'Lombalgia puramente postural/mecânica decorrente de ergonomia inadequada no home office. Prescrito repouso ativo e fisioterapia.',
 1, NULL, NULL, NULL),

(10, 12, 10, '2026-10-07 10:00:00',
 'Primeira sessão da bateria neuropsicológica. Aplicada Escala de Avaliação de TDAH e teste de Atenção Concentrada (TEACO-FF). Apresentou oscilações significativas de foco sustentado após 15 minutos.',
 'Inteligência fluida e raciocínio lógico preservados. Marcada segunda sessão para avaliação executiva.',
 1, NULL, NULL, NULL),

(11, 3, 11, '2026-10-07 15:00:00',
 'Queixa de dificuldade de início do sono (latência > 60 min). Relata uso de smartphone na cama até tarde. Estabelecido plano de Higiene do Sono: banho morno, corte de telas 1h antes de deitar e horário fixo para acordar.',
 'Alergia a frutos do mar confirmada. Orientada a evitar cafeína após as 14h.',
 1, NULL, NULL, NULL),

(12, 1, 12, '2026-10-08 11:30:00',
 'Evolução clínica favorável. Paciente relata ter conseguido apresentar seminário no trabalho sem taquicardia descompensatória. Utilizou com sucesso a técnica de enfrentamento cognitivo.',
 'Paciente relatou novo episódio leve de rinite alérgica na mudança de tempo. Sem sintomas obstrutivos severos.',
 2, '2026-10-08 14:00:00', 'Aditamento para incluir dados sobre o histórico recente de sintomas alérgicos sazonais (RN17)', 1),

(13, 2, 13, '2026-10-08 16:00:00',
 'Análise do MAPA 24h: Média de vigília 126x82 mmHg, média de sono 114x72 mmHg. Descenso noturno fisiológico mantido (> 10%). Ausência de hipertensão sustentada moderada/grave. Mantida conduta não-farmacológica.',
 'Recomendada prática de caminhada aeróbica 150 min/semana e retorno em 60 dias.',
 1, NULL, NULL, NULL),

(14, 5, 14, '2026-10-09 10:00:00',
 'Retorno com diário de cefaleia preenchido. Redução na intensidade das crises após início da profilaxia. Crises reduzidas de 3 para 1 episódio semanal de intensidade leve.',
 'Boa tolerância medicamentosa, sem sonolência excessiva ou efeitos colaterais gástricos.',
 1, NULL, NULL, NULL)
ON DUPLICATE KEY UPDATE anotacoes_clinicas = VALUES(anotacoes_clinicas), informacoes_clinicas_relevantes = VALUES(informacoes_clinicas_relevantes), versao_atual = VALUES(versao_atual);

-- ----------------------------------------------------------------------------
-- 6. HISTÓRICO DE VERSÕES DE PRONTUÁRIO (RN17 - Imutabilidade e Auditoria Clínica)
-- ----------------------------------------------------------------------------
INSERT INTO historico_versoes_prontuario (id_versao, id_sessao, numero_versao, texto_anterior, informacoes_clinicas_anteriores, data_modificacao, motivo_alteracao, id_usuario) VALUES
(1, 12, 1,
 'Evolução clínica favorável. Paciente relata ter conseguido apresentar seminário no trabalho sem taquicardia descompensatória. Utilizou com sucesso a técnica de enfrentamento cognitivo.',
 'Humor eutímico, orientada.',
 '2026-10-08 14:00:00',
 'Aditamento para incluir dados sobre o histórico recente de sintomas alérgicos sazonais (RN17)',
 1)
ON DUPLICATE KEY UPDATE motivo_alteracao = VALUES(motivo_alteracao);

-- ----------------------------------------------------------------------------
-- 7. COBRANÇA FINANCEIRA (RF19-RF22, RN07, RN08)
-- ----------------------------------------------------------------------------
-- Consultas passadas (1 a 14) quitadas, consulta cancelada (15) com taxa quitada, e consultas futuras (16 a 37) pendentes
INSERT INTO pagamentos (id_pagamento, id_agendamento, valor, status, forma_pagamento, data_pagamento, criado_em) VALUES
-- Consultas realizadas (Semana 1 e 2)
(1,  1,  200.00, 'Pago',     'Pix',           '2026-10-01 10:05:00', '2026-09-20 10:00:00'),
(2,  2,  180.00, 'Pago',     'CartaoDebito',  '2026-10-01 15:05:00', '2026-09-21 11:00:00'),
(3,  3,  250.00, 'Pago',     'CartaoCredito', '2026-10-02 11:35:00', '2026-09-22 14:00:00'),
(4,  4,  200.00, 'Pago',     'Pix',           '2026-10-02 16:05:00', '2026-09-23 09:30:00'),
(5,  5,  180.00, 'Pago',     'Dinheiro',      '2026-10-05 10:05:00', '2026-09-25 08:00:00'),
(6,  6,  200.00, 'Pago',     'Pix',           '2026-10-05 12:05:00', '2026-09-26 15:00:00'),
(7,  7,  220.00, 'Pago',     'CartaoCredito', '2026-10-05 15:35:00', '2026-09-27 10:00:00'),
(8,  8,  200.00, 'Pago',     'Pix',           '2026-10-06 11:05:00', '2026-09-28 11:30:00'),
(9,  9,  180.00, 'Pago',     'CartaoDebito',  '2026-10-06 17:05:00', '2026-09-29 09:00:00'),
(10, 10, 250.00, 'Pago',     'Pix',           '2026-10-07 10:05:00', '2026-09-30 14:00:00'),
(11, 11, 200.00, 'Pago',     'CartaoCredito', '2026-10-07 15:05:00', '2026-10-01 16:00:00'),
(12, 12, 200.00, 'Pago',     'Pix',           '2026-10-08 11:35:00', '2026-10-02 10:00:00'),
(13, 13, 180.00, 'Pago',     'Dinheiro',      '2026-10-08 16:05:00', '2026-10-03 11:00:00'),
(14, 14, 200.00, 'Pago',     'Pix',           '2026-10-09 10:05:00', '2026-10-04 14:30:00'),
(15, 15, 100.00, 'Pago',     'Pix',           '2026-10-09 14:30:00', '2026-10-05 10:00:00'),

-- Consultas de Hoje (10/10)
(16, 16, 250.00, 'Pendente', NULL,            NULL,                  '2026-10-06 09:00:00'),
(17, 17, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-06 14:00:00'),
(18, 18, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-07 10:00:00'),
(19, 19, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-07 15:30:00'),

-- Consultas Futuras do Mês
(20, 20, 220.00, 'Pendente', NULL,            NULL,                  '2026-10-08 08:30:00'),
(21, 21, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-08 11:00:00'),
(22, 22, 250.00, 'Pendente', NULL,            NULL,                  '2026-10-08 14:00:00'),
(23, 23, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-08 16:00:00'),
(24, 24, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-09 09:00:00'),
(25, 25, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-09 10:30:00'),
(26, 26, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-09 15:00:00'),
(27, 27, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-10 08:00:00'),
(28, 28, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-10 08:30:00'),
(29, 29, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-10 09:00:00'),
(30, 30, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-10 09:30:00'),
(31, 31, 250.00, 'Pendente', NULL,            NULL,                  '2026-10-10 10:00:00'),
(32, 32, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-10 10:30:00'),
(33, 33, 250.00, 'Pendente', NULL,            NULL,                  '2026-10-10 11:00:00'),
(34, 34, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-10 11:30:00'),
(35, 35, 200.00, 'Pendente', NULL,            NULL,                  '2026-10-10 12:00:00'),
(36, 36, 220.00, 'Pendente', NULL,            NULL,                  '2026-10-10 12:30:00'),
(37, 37, 180.00, 'Pendente', NULL,            NULL,                  '2026-10-10 13:00:00')
ON DUPLICATE KEY UPDATE valor = VALUES(valor), status = VALUES(status), forma_pagamento = VALUES(forma_pagamento), data_pagamento = VALUES(data_pagamento);

-- ----------------------------------------------------------------------------
-- 8. NOTIFICAÇÕES WHATSAPP (RF12, RN03)
-- ----------------------------------------------------------------------------
INSERT INTO notificacoes (id_notificacao, id_agendamento, tipo, canal, data_hora_agendada, data_hora_envio, status, tentativas_envio, alerta_agenda, mensagem) VALUES
(1,  1,  'Confirmacao', 'WhatsApp', '2026-09-20 10:00:00', '2026-09-20 10:01:00', 'Enviado',  1, 0, 'Olá Mariana! Sua consulta está confirmada para 01/10 às 09:00.'),
(2,  2,  'Confirmacao', 'WhatsApp', '2026-09-21 11:00:00', '2026-09-21 11:02:00', 'Enviado',  1, 0, 'Olá Carlos! Sua consulta está confirmada para 01/10 às 14:00.'),
(3,  12, 'Lembrete',    'WhatsApp', '2026-10-07 10:00:00', '2026-10-07 10:01:00', 'Enviado',  1, 0, 'Lembrete Clinix: Mariana, sua consulta é amanhã às 10:30.'),
(4,  15, 'Lembrete',    'WhatsApp', '2026-10-08 16:00:00', '2026-10-08 16:02:00', 'Falha',    2, 1, 'Lembrete falhou: cancelamento tardio informado pela paciente.'),
(5,  16, 'Lembrete',    'WhatsApp', '2026-10-09 09:00:00', '2026-10-09 09:01:00', 'Enviado',  1, 0, 'Lembrete Clinix: Davi, sua consulta é hoje às 09:00.'),
(6,  17, 'Lembrete',    'WhatsApp', '2026-10-09 10:30:00', '2026-10-09 10:31:00', 'Enviado',  1, 0, 'Lembrete Clinix: Juliana, sua consulta é hoje às 10:30.'),
(7,  18, 'Lembrete',    'WhatsApp', '2026-10-09 14:00:00', '2026-10-09 14:01:00', 'Enviado',  1, 0, 'Lembrete Clinix: Camila, sua consulta é hoje às 14:00.'),
(8,  20, 'Confirmacao', 'WhatsApp', '2026-10-08 08:30:00', '2026-10-08 08:31:00', 'Enviado',  1, 0, 'Olá Gabriel! Consulta confirmada para 13/10 às 09:00.'),
(9,  23, 'Confirmacao', 'WhatsApp', '2026-10-08 16:00:00', '2026-10-08 16:01:00', 'Enviado',  1, 0, 'Olá Mariana! Consulta confirmada para 14/10 às 14:00.'),
(10, 25, 'Confirmacao', 'WhatsApp', '2026-10-09 10:30:00', NULL,                  'Pendente', 0, 0, 'Olá Thiago! Consulta agendada para 15/10 aguardando confirmação.')
ON DUPLICATE KEY UPDATE status = VALUES(status), mensagem = VALUES(mensagem);

-- ----------------------------------------------------------------------------
-- 9. BLOQUEIOS DE AGENDA (RF11 - Gestão de Intervalos e Feriados)
-- ----------------------------------------------------------------------------
INSERT INTO bloqueios_agenda (id_bloqueio, id_profissional, tipo, data_hora_inicio, data_hora_fim, descricao, id_usuario_criacao, criado_em) VALUES
(1, 1, 'Feriado',            '2026-10-12 08:00:00', '2026-10-12 18:00:00', 'Feriado Nacional - Nossa Senhora Aparecida / Dia das Crianças', 1, '2026-09-01 08:00:00'),
(2, 1, 'Almoco',             '2026-10-10 12:00:00', '2026-10-10 13:00:00', 'Intervalo de Almoço / Estudo Clínico (RF11)',                 1, '2026-10-01 08:00:00'),
(3, 1, 'CompromissoPessoal', '2026-10-16 14:00:00', '2026-10-16 18:00:00', 'Congresso Centro-Oeste de Neuropsicologia & Saúde Mental',   1, '2026-10-01 08:00:00'),
(4, 1, 'CompromissoPessoal', '2026-10-28 14:00:00', '2026-10-28 16:00:00', 'Reunião Clínica e Supervisão Multidisciplinar',             1, '2026-10-01 08:00:00')
ON DUPLICATE KEY UPDATE descricao = VALUES(descricao);

-- ----------------------------------------------------------------------------
-- 10. AUDITORIA LGPD (RF28, RN13, RQ07)
-- ----------------------------------------------------------------------------
INSERT INTO logs_acesso (id_log, id_usuario, id_paciente, operacao, entidade, id_registro, detalhes, ip_origem, data_hora) VALUES
(1, 1, 1,    'Autenticacao (RF26)',        'Usuario',          1,    'Login efetuado com sucesso como Profissional', '127.0.0.1', '2026-10-01 08:50:00.123'),
(2, 1, 1,    'Acesso ao Prontuario (RN13)', 'SessaoProntuario', 1,    'Consulta à ficha clínica de Mariana Duarte Souza', '127.0.0.1', '2026-10-01 09:05:00.456'),
(3, 1, 2,    'Acesso ao Prontuario (RN13)', 'SessaoProntuario', 2,    'Consulta ao histórico cardiológico de Carlos Neves', '127.0.0.1', '2026-10-01 14:10:00.789'),
(4, 1, 1,    'Edicao de Prontuario (RN17)', 'SessaoProntuario', 12,   'Aditamento de anotação com justificativa clínica (RN17)', '127.0.0.1', '2026-10-08 14:01:00.012'),
(5, 2, NULL, 'Consulta Financeira (RF21)',  'Pagamento',        NULL, 'Emissão de relatório consolidado de receitas do mês', '127.0.0.1', '2026-10-09 17:30:00.345'),
(6, 1, 4,    'Acesso ao Prontuario (RN13)', 'Paciente',         4,    'Abertura de ficha cadastral preventiva: Davi Felinto', '127.0.0.1', '2026-10-10 08:55:00.678')
ON DUPLICATE KEY UPDATE detalhes = VALUES(detalhes);

-- ----------------------------------------------------------------------------
-- 11. RESUMOS FINANCEIROS MENSAIS (RF22, RN09)
-- ----------------------------------------------------------------------------
INSERT INTO resumos_financeiros_mensais (id_resumo, id_profissional, ano, mes, total_atendimentos, total_faturado, total_pendente, gerado_em) VALUES
(1, 1, 2026, 9,  22, 4400.00, 0.00,    '2026-10-01 00:00:00'),
(2, 1, 2026, 10, 37, 2940.00, 4610.00, '2026-10-10 03:00:00')
ON DUPLICATE KEY UPDATE total_atendimentos = VALUES(total_atendimentos), total_faturado = VALUES(total_faturado), total_pendente = VALUES(total_pendente);

SET FOREIGN_KEY_CHECKS = 1;
