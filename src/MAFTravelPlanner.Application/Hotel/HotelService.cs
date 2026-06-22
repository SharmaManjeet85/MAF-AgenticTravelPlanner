using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Hotel;

public sealed class HotelService : IHotelService
{
    public Task<HotelOption> FindHotelAsync(
        string destination,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new HotelOption
            {
                Name = "Tokyo Central Hotel",
                PricePerNight = 120,
                Rating = 4
            });
    }
}