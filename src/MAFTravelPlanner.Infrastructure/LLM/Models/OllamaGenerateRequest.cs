using System.Text.Json.Serialization;

namespace MAFTravelPlanner.Infrastructure.LLM.Models;

public sealed class OllamaGenerateRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    [JsonPropertyName("stream")]
    public bool Stream { get; set; }

    [JsonPropertyName("system")]
    public string? System { get; set; }
}