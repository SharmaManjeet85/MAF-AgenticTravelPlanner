namespace MAFTravelPlanner.Application.AI.Planning;

public sealed record PlanningResult(
    IReadOnlyList<PlannedToolCall> ToolCalls)
{
    public bool HasToolCalls =>
        ToolCalls.Count > 0;
}