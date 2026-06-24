using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Infrastructure.Configuration;
using MAFTravelPlanner.Infrastructure.LLM.Models;

namespace MAFTravelPlanner.Infrastructure.LLM;

public sealed class OllamaLlmService : ILlmService
{
    private readonly HttpClient _httpClient;
    private readonly OllamaOptions _options;

    public OllamaLlmService(
        HttpClient httpClient,
        IOptions<OllamaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var request = new OllamaGenerateRequest
        {
            Model = _options.Model,
            Prompt = prompt,
            Stream = false
        };

        var json = JsonSerializer.Serialize(request);

        var response = await _httpClient.PostAsync(
            "api/generate",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"),
            cancellationToken);

        var responseJson =
            await response.Content.ReadAsStringAsync(
                cancellationToken);


        if (!response.IsSuccessStatusCode)
        {
                throw new OllamaException(
                    $"Ollama returned {response.StatusCode}");
        }
        var result =
            JsonSerializer.Deserialize<
                OllamaGenerateResponse>(
                responseJson);

        // return responseJson;
        return result?.Response ?? string.Empty;
    }
}