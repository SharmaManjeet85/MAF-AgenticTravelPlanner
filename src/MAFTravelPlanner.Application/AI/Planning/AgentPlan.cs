namespace MAFTravelPlanner.Application.AI.Planning;

public sealed record AgentPlan(
    bool RequiresTool,
    PlannedToolCall? ToolCall);