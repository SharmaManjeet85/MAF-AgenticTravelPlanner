using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IFlightService
{
    Task<FlightOption> FindFlightAsync(
        string destination,
        CancellationToken cancellationToken = default);
}