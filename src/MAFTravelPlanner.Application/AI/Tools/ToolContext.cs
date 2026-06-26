namespace MAFTravelPlanner.Application.AI.Tools;

public sealed record ToolContext(
    IReadOnlyDictionary<string, object?> Parameters);