using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Models;
using MAFTravelPlanner.Application.Common;
namespace MAFTravelPlanner.Application.Flight;

public sealed class FlightService : IFlightService
{
    public Task<Result<FlightOption>> FindFlightAsync(
        string destination,
        CancellationToken cancellationToken = default)
    {
        if(destination.ToLower() == "mars")
        {
            return Task.FromResult(
                Result<FlightOption>.Failure(Error.FlightNotFound("No flights available to Mars."))
            );
        }
        return Task.FromResult(
            Result<FlightOption>.Success(
                new FlightOption
                {
                    Airline = "ANA",
                    Price = 900,
                    Route = $"Delhi -> {destination}"
                }));
    }
}
