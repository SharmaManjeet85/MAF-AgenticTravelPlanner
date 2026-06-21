namespace MAFTravelPlanner.Contracts.Requests;

public sealed class TravelRequest
{
    public string Destination { get; init; } = string.Empty;

    public int Days { get; init; }

    public decimal Budget { get; init; }
}