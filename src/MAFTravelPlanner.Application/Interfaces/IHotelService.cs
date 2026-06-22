using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IHotelService
{
    Task<HotelOption> FindHotelAsync(
        string destination,
        CancellationToken cancellationToken = default);
}