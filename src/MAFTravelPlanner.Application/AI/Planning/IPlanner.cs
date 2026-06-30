namespace MAFTravelPlanner.Application.AI.Planning;

public interface IPlanner
{
    Task<PlanningResult> PlanAsync(
        PlannerRequest request,
        CancellationToken cancellationToken = default);
}