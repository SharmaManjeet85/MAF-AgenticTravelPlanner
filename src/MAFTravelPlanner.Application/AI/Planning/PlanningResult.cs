namespace MAFTravelPlanner.Application.AI.Planning;

public sealed record PlanningResult(
    bool RequiresTool,
    string? Tool,
    IReadOnlyDictionary<string, string> Arguments)
{
    public static PlanningResult Empty =>
        new(
            false,
            null,
            new Dictionary<string, string>());
}