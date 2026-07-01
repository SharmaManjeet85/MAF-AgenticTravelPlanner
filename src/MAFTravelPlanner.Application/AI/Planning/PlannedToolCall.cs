namespace MAFTravelPlanner.Application.AI.Planning;

public sealed record PlannedToolCall(
    string ToolName,
    IReadOnlyDictionary<string, object?> Arguments);