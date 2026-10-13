using System.Threading.RateLimiting;
using ClinicaApp.Api.Data;
using ClinicaApp.Api.Seguranca;
using ClinicaApp.Domain.Interfaces;
using ClinicaApp.Infrastructure.External;
using ClinicaApp.Infrastructure.InMemory;
using ClinicaApp.Infrastructure.MySQL;
using ClinicaApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddSingleton<TokenService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AgendaService>();
builder.Services.AddScoped<ProntuarioService>();
builder.Services.AddScoped<FinanceiroService>();

// RF26, RN12 - Autenticação por token JWT: todo endpoint com [Authorize] exige login
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<TokenService>((options, tokenService) =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = TokenService.Emissor,
            ValidateAudience = true,
            ValidAudience = TokenService.Emissor,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = tokenService.Chave,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });
builder.Services.AddAuthorization();

// Proteção contra força bruta: no máximo 5 tentativas de login por minuto por IP
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("login", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "desconhecido",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1)
            }));
});

// Suporte a CORS para o frontend (tanto no GitHub Pages quanto local)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers(options => options.Filters.Add<ErrosDeNegocioFilter>());
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
else
{
    app.UseHsts();
}

// Cabeçalhos de segurança em todas as respostas
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "no-referrer";
    headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.tailwindcss.com https://unpkg.com; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "img-src 'self' data:; " +
        "connect-src 'self' https://clinix-api-c58x.onrender.com; " +
        "object-src 'none'; base-uri 'self'; form-action 'self'; frame-ancestors 'none'";
    await next();
});

app.UseCors();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Rota pública para checagem rápida de integridade da API
app.MapGet("/api/health", () => Results.Ok(new { status = "ok" })).AllowAnonymous();

app.Run();
