using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CepController : ControllerBase
{
    private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

    // GET: api/cep/{cep}
    [HttpGet("{cep}")]
    public async Task<IActionResult> Consultar(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return BadRequest(new { sucesso = false, erro = "CEP não informado." });

        var limpo = System.Text.RegularExpressions.Regex.Replace(cep, @"\D", "");
        if (limpo.Length != 8)
            return BadRequest(new { sucesso = false, erro = "CEP deve conter exatamente 8 dígitos numéricos." });

        try
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{limpo}/json/");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { sucesso = false, erro = "Serviço ViaCEP temporariamente indisponível." });

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("erro", out var erroProp) && erroProp.GetBoolean())
            {
                return NotFound(new { sucesso = false, erro = "CEP não encontrado na base oficial dos Correios." });
            }

            var logradouro = doc.RootElement.TryGetProperty("logradouro", out var log) ? log.GetString() : "";
            var bairro = doc.RootElement.TryGetProperty("bairro", out var bai) ? bai.GetString() : "";
            var localidade = doc.RootElement.TryGetProperty("localidade", out var loc) ? loc.GetString() : "";
            var uf = doc.RootElement.TryGetProperty("uf", out var u) ? u.GetString() : "";
            var complemento = doc.RootElement.TryGetProperty("complemento", out var comp) ? comp.GetString() : "";

            return Ok(new
            {
                sucesso = true,
                cep = limpo,
                logradouro = logradouro ?? "",
                bairro = bairro ?? "",
                cidade = localidade ?? "",
                estadoUf = uf ?? "",
                complemento = complemento ?? ""
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { sucesso = false, erro = $"Falha ao consultar CEP: {ex.Message}" });
        }
    }
}
