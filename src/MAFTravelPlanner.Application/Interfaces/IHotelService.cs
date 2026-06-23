using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IHotelService
{
    Task<Result<HotelOption>> FindHotelAsync(
        string destination,
        CancellationToken cancellationToken = default);
}