using System.Text.Json.Serialization;

namespace MAFTravelPlanner.Application.AI.Planning;

internal sealed class PlanningResponseDto
{
    [JsonPropertyName("requiresTool")]
    public bool RequiresTool { get; init; }

    [JsonPropertyName("tool")]
    public string? Tool { get; init; }

    [JsonPropertyName("arguments")]
    public Dictionary<string, string> Arguments { get; init; } = [];
}