using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Contracts.Responses;

public sealed record TravelPlanResponse
{
    public required FlightOption Flight { get; init; }
    public required HotelOption Hotel { get; init; }
    public required BudgetAnalysis Budget { get; init; }
}