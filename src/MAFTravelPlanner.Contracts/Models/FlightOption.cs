namespace MAFTravelPlanner.Contracts.Models;

public sealed record FlightOption
{
    public required string Airline { get; init; }

    public required decimal Price { get; init; }

    public required string Route { get; init; }
}