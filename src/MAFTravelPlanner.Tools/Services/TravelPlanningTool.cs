using MAFTravelPlanner.Contracts.Requests;
using MAFTravelPlanner.Contracts.Responses;
using MAFTravelPlanner.Tools.Interfaces;

namespace MAFTravelPlanner.Tools;

public sealed class TravelPlanningTool : ITravelPlanningTool
{
    public Task<TravelPlanResponse> GeneratePlanAsync(
        TravelRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new TravelPlanResponse
        {
            FlightRecommendation =
                $"Sample flight to {request.Destination}",

            HotelRecommendation =
                $"Sample hotel for {request.Destination}",

            BudgetAnalysis =
                $"Budget {request.Budget} appears sufficient"
        };

        return Task.FromResult(response);
    }
}