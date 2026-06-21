namespace MAFTravelPlanner.Contracts.Responses;

public sealed class TravelPlanResponse
{
    public string FlightRecommendation { get; init; } = string.Empty;

    public string HotelRecommendation { get; init; } = string.Empty;

    public string BudgetAnalysis { get; init; } = string.Empty;
}