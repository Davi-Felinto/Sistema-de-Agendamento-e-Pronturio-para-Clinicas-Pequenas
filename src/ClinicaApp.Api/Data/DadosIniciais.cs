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
        var medico = new ProfissionalSaude(
            id: 1,
            nome: "Dr. Roberto Silva",
            login: "roberto.med",
            senhaPura: "SenhaSegura@2026",
            registroProfissional: "CRM-DF 12345",
            especialidade: "Clínica Geral"
        );
        authService.Cadastrar(medico);

        var admin = new Administrador(
            id: 2,
            nome: "Ana Costa",
            login: "ana.admin",
            senhaPura: "AdminMaster@2026",
            cargo: "Gerente de Operações"
        );
        authService.Cadastrar(admin);

        pacienteRepository.Adicionar(new Paciente(
            id: 1,
            nome: "Mariana Souza",
            documentoIdentificacao: "023.456.789-10",
            dataNascimento: new DateTime(1998, 7, 14),
            telefone: "(61) 99876-5432",
            email: "mariana.souza@email.com",
            endereco: "Asa Sul - Brasília/DF",
            alergias: "Dipirona"
        ));

        pacienteRepository.Adicionar(new Paciente(
            id: 2,
            nome: "Carlos Mendes",
            documentoIdentificacao: "111.222.333-44",
            dataNascimento: new DateTime(1985, 3, 22),
            telefone: "(61) 98123-4567",
            email: "carlos.mendes@email.com",
            endereco: "Águas Claras - Brasília/DF",
            alergias: "Nenhuma"
        ));

        // RN01, RN02 - Consulta pré-existente para demonstrar o bloqueio de conflito na interface
        var amanha14h = DateTime.Today.AddDays(1).AddHours(14);
        agendaService.AgendarConsulta(
            id: 1,
            pacienteId: 1,
            profissionalId: medico.Id,
            dataHoraInicio: amanha14h,
            dataHoraFim: amanha14h.AddMinutes(45),
            observacoes: "Consulta de rotina"
        );
    }
}

