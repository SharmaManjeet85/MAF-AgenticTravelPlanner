namespace MAFTravelPlanner.Application.AI.Planning;

public interface IAgentPlanner
{
    Task<AgentPlan> CreatePlanAsync(
        string userRequest,
        CancellationToken cancellationToken = default);
}