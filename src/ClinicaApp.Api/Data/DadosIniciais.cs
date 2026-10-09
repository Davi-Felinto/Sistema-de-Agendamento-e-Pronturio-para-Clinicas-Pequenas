using ClinicaApp.Domain.Entities;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Services;

namespace ClinicaApp.Api.Data;

public static class DadosIniciais
{
    public static void Popular(IServiceProvider services)
    {
        var authService = services.GetRequiredService<AuthService>();
        var pacienteRepository = services.GetRequiredService<IPacienteRepository>();
        var agendaService = services.GetRequiredService<AgendaService>();

        // RF26, RQ08 - Usuários de demonstração com senha armazenada em hash + salt
        var medicoDavi = new ProfissionalSaude(
            id: 1,
            nome: "Dr. Davi Felinto",
            login: "davi.profissional@clinix.com",
            senhaPura: "123456",
            registroProfissional: "CRP-DF 12345",
            especialidade: "Psicologia Clínica & Neuropsicologia"
        );
        authService.Cadastrar(medicoDavi);

        var adminJuliana = new Administrador(
            id: 2,
            nome: "Juliana Costa",
            login: "admin@clinix.com",
            senhaPura: "123456",
            cargo: "Gestão & Recepção Clínica"
        );
        authService.Cadastrar(adminJuliana);

        var medicoRoberto = new ProfissionalSaude(
            id: 3,
            nome: "Dr. Roberto Silva",
            login: "roberto.med",
            senhaPura: "SenhaSegura@2026",
            registroProfissional: "CRM-DF 98765",
            especialidade: "Clínica Geral"
        );
        authService.Cadastrar(medicoRoberto);

        var adminAna = new Administrador(
            id: 4,
            nome: "Ana Costa",
            login: "ana.admin",
            senhaPura: "AdminMaster@2026",
            cargo: "Gerente de Operações"
        );
        authService.Cadastrar(adminAna);

        pacienteRepository.Adicionar(new Paciente(
            id: 1,
            nome: "Mariana Duarte Souza",
            documentoIdentificacao: "123.456.789-00",
            dataNascimento: new DateTime(1992, 5, 14),
            telefone: "(61) 98765-4321",
            email: "mariana.duarte@email.com",
            endereco: "Asa Norte, Bloco C, Apto 302 - Brasília/DF",
            alergias: "Penicilina, Dipirona",
            condicoesPreexistentes: "Rinite alérgica crônica, Ansiedade generalizada"
        ));

        pacienteRepository.Adicionar(new Paciente(
            id: 2,
            nome: "Carlos Eduardo Neves",
            documentoIdentificacao: "987.654.321-11",
            dataNascimento: new DateTime(1985, 11, 20),
            telefone: "(61) 99123-9988",
            email: "carlos.neves@email.com",
            endereco: "Águas Claras, Rua 24 Sul - Brasília/DF",
            alergias: "Nenhuma conhecida",
            condicoesPreexistentes: "Hipertensão leve"
        ));

        pacienteRepository.Adicionar(new Paciente(
            id: 3,
            nome: "Beatriz Vasconcelos Ramos",
            documentoIdentificacao: "456.789.012-33",
            dataNascimento: new DateTime(1998, 3, 8),
            telefone: "(61) 98222-1144",
            email: "beatriz.ramos@email.com",
            endereco: "Sudoeste, QMSW 5, Bloco B - Brasília/DF",
            alergias: "Frutos do mar",
            condicoesPreexistentes: "Insônia pontual"
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
}

