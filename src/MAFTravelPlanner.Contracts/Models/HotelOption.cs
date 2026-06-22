namespace MAFTravelPlanner.Contracts.Models;

public sealed record HotelOption
{
    public required string Name { get; init; }

    public required decimal PricePerNight { get; init; }

    public required int Rating { get; init; }
}