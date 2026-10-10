using ClinicaApp.Api.Data;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Infrastructure.External;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Infrastructure.MySQL;
using ClinicaApp.Services;

var builder = WebApplication.CreateBuilder(args);

// String de conexão com MySQL (Banco de Dados II)
var connectionString = builder.Configuration.GetConnectionString("ClinixDb");
bool forcarMemoria = args.Contains("--in-memory") || builder.Configuration.GetValue<bool>("UseInMemory");
bool usarMySql = !forcarMemoria && !string.IsNullOrWhiteSpace(connectionString) && !connectionString.Contains("sua_senha");

Console.WriteLine(usarMySql
    ? "==> [Clinix] Provedor de Dados Ativo: MySQL 8.0 (Persistência Relacional)"
    : "==> [Clinix] Provedor de Dados Ativo: Em Memoria (Mock / Fallback)");

if (usarMySql)
{
    // Persistência relacional MySQL com Dapper
    builder.Services.AddScoped<IUsuarioRepository>(_ => new MySqlUsuarioRepository(connectionString!));
    builder.Services.AddScoped<IPacienteRepository>(_ => new MySqlPacienteRepository(connectionString!));
    builder.Services.AddScoped<IAgendamentoRepository>(_ => new MySqlAgendamentoRepository(connectionString!));
    builder.Services.AddScoped<IProntuarioRepository>(_ => new MySqlProntuarioRepository(connectionString!));
    builder.Services.AddScoped<IPagamentoRepository>(_ => new MySqlPagamentoRepository(connectionString!));
    builder.Services.AddScoped<ILogAcessoRepository>(_ => new MySqlLogAcessoRepository(connectionString!));
}
else
{
    // Fallback em memória para desenvolvimento rápido e testes de front-end
    builder.Services.AddSingleton<IUsuarioRepository, InMemoryUsuarioRepository>();
    builder.Services.AddSingleton<IPacienteRepository, InMemoryPacienteRepository>();
    builder.Services.AddSingleton<IAgendamentoRepository, InMemoryAgendamentoRepository>();
    builder.Services.AddSingleton<IProntuarioRepository, InMemoryProntuarioRepository>();
    builder.Services.AddSingleton<IPagamentoRepository, InMemoryPagamentoRepository>();
    builder.Services.AddSingleton<ILogAcessoRepository, InMemoryLogAcessoRepository>();
}

builder.Services.AddSingleton<INotificador, NotificadorWhatsApp>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AgendaService>();
builder.Services.AddScoped<ProntuarioService>();
builder.Services.AddScoped<FinanceiroService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Popula dados mockados apenas em modo InMemory (no MySQL os dados vêm de 02_dados_iniciais.sql)
if (!usarMySql)
{
    using var scope = app.Services.CreateScope();
    DadosIniciais.Popular(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();

