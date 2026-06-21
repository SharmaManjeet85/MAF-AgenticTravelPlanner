using MAFTravelPlanner.Contracts.Requests;
using MAFTravelPlanner.Contracts.Responses;

namespace MAFTravelPlanner.Tools.Interfaces;

public interface ITravelPlanningTool
{
    Task<TravelPlanResponse> GeneratePlanAsync(
        TravelRequest request,
        CancellationToken cancellationToken = default);
}