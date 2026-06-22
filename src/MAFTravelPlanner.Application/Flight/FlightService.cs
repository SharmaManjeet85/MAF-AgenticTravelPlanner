using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Flight;

public sealed class FlightService : IFlightService
{
    public Task<FlightOption> FindFlightAsync(
        string destination,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new FlightOption
            {
                Airline = "ANA",
                Price = 900,
                Route = $"Delhi -> {destination}"
            });
    }
}