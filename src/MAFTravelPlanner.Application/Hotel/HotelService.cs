using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Hotel;

public sealed class HotelService : IHotelService
{
    public Task<Result<HotelOption>> FindHotelAsync(
        string destination,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Result<HotelOption>.Success(
                new HotelOption
                {
                    Name = "Tokyo Central Hotel",
                    PricePerNight = 120,
                    Rating = 4
                }
            ));
    }
}