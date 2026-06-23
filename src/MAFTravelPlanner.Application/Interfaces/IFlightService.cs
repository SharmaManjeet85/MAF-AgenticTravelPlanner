using MAFTravelPlanner.Contracts.Models;
using MAFTravelPlanner.Application.Common;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IFlightService
{
    Task<Result<FlightOption>> FindFlightAsync(
        string destination,
        CancellationToken cancellationToken = default);
}