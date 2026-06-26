namespace MAFTravelPlanner.Application.AI.Tools;

public sealed record ToolResult(
    string ToolName,
    bool Success,
    string Content,
    string? Error = null);