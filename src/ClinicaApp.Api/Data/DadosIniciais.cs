using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Data;

public static class DadosIniciais
{
    public static void Popular(IServiceProvider services, bool apenasAdicionais = false)
    {
        var avaliacaoRepo = services.GetService<IAvaliacaoRepository>();
        var exameRepo = services.GetService<IExameLaboratorioRepository>();

        if (!apenasAdicionais)
        {
            var authService = services.GetRequiredService<AuthService>();
            var pacienteRepository = services.GetRequiredService<IPacienteRepository>();
            var agendaService = services.GetRequiredService<AgendaService>();

        // RF26, RQ08 - Usuários de demonstração com senha armazenada em hash + salt
        var medicoDavi = new ProfissionalSaude(
            id: 1,
            nome: "Dr. Davi Felinto",
            login: "davi@clinix.com",
            senhaPura: "123456",
            registroProfissional: "CRP-DF 12345",
            especialidade: "Psicologia Clínica & Neuropsicologia"
        );
        authService.Cadastrar(medicoDavi);

        var adminLucas = new Administrador(
            id: 2,
            nome: "Lucas Pereira",
            login: "lucas@clinix.com",
            senhaPura: "123456",
            cargo: "Engenharia de Requisitos & Gestão de Processos"
        );
        authService.Cadastrar(adminLucas);

        var medicoMiguel = new ProfissionalSaude(
            id: 3,
            nome: "Dr. Miguel Silva",
            login: "miguel@clinix.com",
            senhaPura: "123456",
            registroProfissional: "CRM-DF 54321",
            especialidade: "Clínica Geral & Telemedicina"
        );
        authService.Cadastrar(medicoMiguel);

        var adminIsaac = new Administrador(
            id: 4,
            nome: "Isaac Santos",
            login: "isaac@clinix.com",
            senhaPura: "123456",
            cargo: "Administrador de Banco de Dados (DBA) & Auditor LGPD"
        );
        authService.Cadastrar(adminIsaac);

        var medicoVanessa = new ProfissionalSaude(
            id: 5,
            nome: "Dra. Vanessa Oliveira",
            login: "vanessa@clinix.com",
            senhaPura: "123456",
            registroProfissional: "CRM-DF 67890",
            especialidade: "Pediatria & Saúde da Família"
        );
        authService.Cadastrar(medicoVanessa);

        var adminJuliana = new Administrador(
            id: 6,
            nome: "Juliana Costa",
            login: "juliana@clinix.com",
            senhaPura: "123456",
            cargo: "Recepção & Atendimento ao Paciente"
        );
        authService.Cadastrar(adminJuliana);


        pacienteRepository.Adicionar(new Paciente(
            id: 1,
            nome: "Mariana Duarte Souza",
            documentoIdentificacao: "123.456.789-00",
            dataNascimento: new DateTime(1992, 5, 14),
            telefone: "(61) 98765-4321",
            email: "mariana.duarte@email.com",
            endereco: "SQN 204 Bloco B, 302 - Asa Norte, Brasília - DF, CEP: 70842-020",
            alergias: "Penicilina, Dipirona",
            condicoesPreexistentes: "Rinite alérgica crônica, Ansiedade generalizada",
            ativo: true,
            cep: "70842020",
            logradouro: "SQN 204 Bloco B",
            numero: "302",
            complemento: "Apto 302",
            bairro: "Asa Norte",
            cidade: "Brasília",
            estadoUf: "DF"
        ));

        pacienteRepository.Adicionar(new Paciente(
            id: 2,
            nome: "Carlos Eduardo Neves",
            documentoIdentificacao: "987.654.321-11",
            dataNascimento: new DateTime(1985, 11, 20),
            telefone: "(61) 99123-9988",
            email: "carlos.neves@email.com",
            endereco: "Rua 24 Sul, Lote 5 - Águas Claras, Brasília - DF, CEP: 71926-000",
            alergias: "Nenhuma conhecida",
            condicoesPreexistentes: "Hipertensão leve",
            ativo: true,
            cep: "71926000",
            logradouro: "Rua 24 Sul",
            numero: "Lote 5",
            complemento: "Ed. Monet",
            bairro: "Águas Claras",
            cidade: "Brasília",
            estadoUf: "DF"
        ));

        pacienteRepository.Adicionar(new Paciente(
            id: 3,
            nome: "Beatriz Vasconcelos Ramos",
            documentoIdentificacao: "456.789.012-33",
            dataNascimento: new DateTime(1998, 3, 8),
            telefone: "(61) 98222-1144",
            email: "beatriz.ramos@email.com",
            endereco: "QMSW 5 Bloco B, 104 - Sudoeste, Brasília - DF, CEP: 70680-500",
            alergias: "Frutos do mar",
            condicoesPreexistentes: "Insônia pontual",
            ativo: true,
            cep: "70680500",
            logradouro: "QMSW 5 Bloco B",
            numero: "104",
            complemento: "Bloco B",
            bairro: "Sudoeste",
            cidade: "Brasília",
            estadoUf: "DF"
        ));

        // RN01, RN02 - Consulta pré-existente para amanhã às 14h para demonstrar bloqueio de conflito
        var amanha14h = DateTime.Today.AddDays(1).AddHours(14);
        agendaService.AgendarConsulta(
            id: 1,
            pacienteId: 1,
            profissionalId: medicoDavi.Id,
            dataHoraInicio: amanha14h,
            dataHoraFim: amanha14h.AddMinutes(60),
            observacoes: "Acompanhamento quinzenal com foco em manejo de estresse"
        );
    }

        // RF32, RN21 - Seed inicial de Ouvidoria e Avaliações de Satisfação
        if (avaliacaoRepo != null)
        {
            avaliacaoRepo.Adicionar(new AvaliacaoAtendimento(
                id: 1,
                agendamentoId: 1,
                pacienteId: 1,
                nomePaciente: "Mariana Duarte Souza",
                dataAvaliacao: DateTime.Today.AddDays(-2),
                notaRecepcao: 5,
                notaAtendimentoClinico: 5,
                notaEspacoFisico: 5,
                comentarioOuvidoria: "Atendimento impecável! A secretária foi super atenciosa e o Dr. Davi me acolheu com muita empatia.",
                anonimo: false
            ));

            avaliacaoRepo.Adicionar(new AvaliacaoAtendimento(
                id: 2,
                agendamentoId: 2,
                pacienteId: 2,
                nomePaciente: "Carlos Eduardo Neves",
                dataAvaliacao: DateTime.Today.AddDays(-5),
                notaRecepcao: 4,
                notaAtendimentoClinico: 5,
                notaEspacoFisico: 5,
                comentarioOuvidoria: "A consulta foi excelente e esclarecedora. Apenas um pequeno atraso de 10 minutos na recepção, mas ambiente muito confortável.",
                anonimo: false
            ));
        }

        // RF34, RN23 - Seed inicial de Rastreamento de Exames Laboratoriais
        if (exameRepo != null)
        {
            exameRepo.Adicionar(new PedidoExameLaboratorio(
                id: 1,
                pacienteId: 1,
                nomePaciente: "Mariana Duarte Souza",
                nomeExameAmostra: "Painel Alérgico & IgE Total (Sangue)",
                laboratorioParceiro: "Laboratório Sabin Medicina Diagnóstica",
                dataColeta: DateTime.Today.AddDays(-3),
                dataPrevisaoLaudo: DateTime.Today.AddDays(2),
                codigoRastreio: "SABIN-2026-9812",
                statusAmostra: StatusAmostraLaboratorio.EmAnalise,
                observacoes: "Amostra biológica enviada via motoboy refrigerada. Aguardando laudo para retorno."
            ));

            exameRepo.Adicionar(new PedidoExameLaboratorio(
                id: 2,
                pacienteId: 2,
                nomePaciente: "Carlos Eduardo Neves",
                nomeExameAmostra: "Mapeamento Pressórico & MAPA 24h",
                laboratorioParceiro: "Exame Laboratório e Imagem",
                dataColeta: DateTime.Today.AddDays(-10),
                dataPrevisaoLaudo: DateTime.Today.AddDays(-3),
                dataRecebimentoLaudo: DateTime.Today.AddDays(-2),
                codigoRastreio: "EXM-77412",
                statusAmostra: StatusAmostraLaboratorio.LaudoLiberado,
                observacoes: "Laudo emitido e arquivado no prontuário. Paciente apto para reavaliação clínica."
            ));
        }
    }
}

