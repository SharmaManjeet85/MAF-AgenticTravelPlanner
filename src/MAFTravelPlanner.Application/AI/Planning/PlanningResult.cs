namespace MAFTravelPlanner.Application.AI.Planning;

public sealed record PlanningResult(
    bool RequiresTool,
    string? Tool,
    IReadOnlyDictionary<string, string> Arguments);