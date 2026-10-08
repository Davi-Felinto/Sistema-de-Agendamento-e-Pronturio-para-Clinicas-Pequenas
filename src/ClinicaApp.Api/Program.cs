using ClinicaApp.Api.Data;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Infrastructure.External;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsuarioRepository, InMemoryUsuarioRepository>();
builder.Services.AddSingleton<IPacienteRepository, InMemoryPacienteRepository>();
builder.Services.AddSingleton<IAgendamentoRepository, InMemoryAgendamentoRepository>();
builder.Services.AddSingleton<IProntuarioRepository, InMemoryProntuarioRepository>();
builder.Services.AddSingleton<IPagamentoRepository, InMemoryPagamentoRepository>();
builder.Services.AddSingleton<ILogAcessoRepository, InMemoryLogAcessoRepository>();
builder.Services.AddSingleton<INotificador, NotificadorWhatsApp>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AgendaService>();
builder.Services.AddScoped<ProntuarioService>();
builder.Services.AddScoped<FinanceiroService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DadosIniciais.Popular(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();

