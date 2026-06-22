using MAFTravelPlanner.Contracts.Requests;
using MAFTravelPlanner.Contracts.Responses;

namespace MAFTravelPlanner.Application.Interfaces;

public interface ITravelPlannerOrchestrator
{
    Task<TravelPlanResponse> GeneratePlanAsync(
        TravelRequest request,
        CancellationToken cancellationToken = default);
}