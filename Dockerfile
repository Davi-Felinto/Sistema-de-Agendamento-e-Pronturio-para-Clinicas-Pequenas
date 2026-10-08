# 1. Estágio de Build (SDK do .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto primeiro (ajuda no cache do Docker)
COPY ["src/ClinicaApp.Api/ClinicaApp.Api.csproj", "src/ClinicaApp.Api/"]
COPY ["src/ClinicaApp/ClinicaApp.csproj", "src/ClinicaApp/"]
RUN dotnet restore "src/ClinicaApp.Api/ClinicaApp.Api.csproj"

# Copia todo o resto do código
COPY src/ src/
WORKDIR "/src/src/ClinicaApp.Api"

# Compila o projeto em modo Release
RUN dotnet publish "ClinicaApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Estágio de Execução (Apenas o Runtime, imagem super leve)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# No .NET 8 rodando em container, a porta padrão é 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ClinicaApp.Api.dll"]

