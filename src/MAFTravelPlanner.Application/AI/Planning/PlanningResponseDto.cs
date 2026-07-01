using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAFTravelPlanner.Application.AI.Planning;

public sealed class PlanningResponseDto
{
    [JsonPropertyName("toolCalls")]
    public List<ToolCallDto>? ToolCalls { get; set; }
}

public sealed class ToolCallDto
{
    [JsonPropertyName("tool")]
    public string Tool { get; set; } = string.Empty;

    [JsonPropertyName("arguments")]
    public Dictionary<string, JsonElement> Arguments { get; set; }
        = new();
}