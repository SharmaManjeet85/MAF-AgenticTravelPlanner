using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Infrastructure.Configuration;
using MAFTravelPlanner.Infrastructure.LLM.Models;
using MAFTravelPlanner.Application.AI.Models;
using System.Diagnostics;

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

    public async Task<LlmResponse> GenerateAsync(
        LlmRequest llmRequest,
        CancellationToken cancellationToken = default)
    {

        var request = new OllamaGenerateRequest
        {
            Model = _options.Model,
            Prompt = llmRequest.Prompt,
            Stream = false
        };

        var json = JsonSerializer.Serialize(request);

        var stopwatch = Stopwatch.StartNew();

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

        stopwatch.Stop();

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
       // return result?.Response ?? string.Empty;
return new LlmResponse(
    result?.Response ?? string.Empty,
    new AiResponseMetadata(
        _options.Model,
        stopwatch.Elapsed));
}
}