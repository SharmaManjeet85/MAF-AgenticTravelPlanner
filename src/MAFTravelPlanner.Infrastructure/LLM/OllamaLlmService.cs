using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.AI.Models;
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

    public async Task<LlmResponse> GenerateAsync(
        LlmRequest request,
        CancellationToken cancellationToken = default)
    {
        var model = request.Purpose switch
        {
            AiPurpose.Planning => _options.PlannerModel,
            AiPurpose.Generation => _options.GeneralModel,
            _ => _options.GeneralModel
        };
        var ollamaRequest = new OllamaGenerateRequest
        {
            Model =  model,
            Prompt = request.Prompt,
            System = request.SystemPrompt,
            Stream = false
        };

        Console.WriteLine("===== LLM REQUEST =====");
        Console.WriteLine($"Purpose : {request.Purpose}");
        Console.WriteLine($"Model   : {ollamaRequest.Model}");

        var json = JsonSerializer.Serialize(ollamaRequest);

        var response = await _httpClient.PostAsync(
            "/api/generate",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"),
            cancellationToken);

        var responseContent =
            await response.Content.ReadAsStringAsync(cancellationToken);


        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Ollama returned {(int)response.StatusCode} ({response.StatusCode}). " +
                $"Response: {responseContent}");
        }

        var result =
            JsonSerializer.Deserialize<OllamaGenerateResponse>(
                responseContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (result is null)
        {
            throw new InvalidOperationException(
                "Unable to deserialize Ollama response.");
        }

        Console.WriteLine("===== LLM RESPONSE =====");
        Console.WriteLine($"Model    : {result.Model}");
        Console.WriteLine($"Duration : {result.TotalDuration}");

        var duration =
        TimeSpan.FromMilliseconds(
        result.TotalDuration / 1_000_000.0);

        return new LlmResponse(
            Content: result.Response,
            Metadata: new AiResponseMetadata(
                result.Model,
                duration));
            }
        }