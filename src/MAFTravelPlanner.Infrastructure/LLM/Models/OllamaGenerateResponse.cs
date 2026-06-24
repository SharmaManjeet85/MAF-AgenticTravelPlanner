using System.Text.Json.Serialization;

namespace MAFTravelPlanner.Infrastructure.LLM.Models;

public sealed class OllamaGenerateResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;
}