using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Infrastructure.Tools;

public sealed class WeatherTool : IWeatherTool
{
    public ToolDefinition Definition =>
        new(
            "weather",
            "Returns current weather information for a destination.");

    public Task<ToolResult> ExecuteAsync(
        ToolContext context,
        CancellationToken cancellationToken = default)
    {
        if (!context.Parameters.TryGetValue(
                "destination",
                out var destination))
        {
            return Task.FromResult(
                new ToolResult(
                    Definition.Name,
                    false,
                    string.Empty,
                    "Destination parameter is required."));
        }

        // Temporary deterministic implementation
        var observation = $"""
            Destination: {destination}
            Temperature: 32°C
            Condition: Sunny
            Humidity: 74%
            Best Time Outdoors: Morning and Evening
            """;

        return Task.FromResult(
            new ToolResult(
                Definition.Name,
                true,
                observation));
    }
}