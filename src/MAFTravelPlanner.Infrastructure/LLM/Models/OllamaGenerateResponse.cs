using System.Text.Json.Serialization;

namespace MAFTravelPlanner.Infrastructure.LLM.Models;

public sealed class OllamaGenerateResponse
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;

    // Nanoseconds
    [JsonPropertyName("total_duration")]
    public long TotalDuration { get; set; }
}