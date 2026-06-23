using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Requests;
using MAFTravelPlanner.Contracts.Responses;

namespace TravelPlanner.Application.Orchestration;

public sealed class TravelPlannerOrchestrator
    : ITravelPlannerOrchestrator
{
    private readonly IFlightService _flightService;
    private readonly IHotelService _hotelService;
    private readonly IBudgetService _budgetService;

    public TravelPlannerOrchestrator(
        IFlightService flightService,
        IHotelService hotelService,
        IBudgetService budgetService)
    {
        _flightService = flightService;
        _hotelService = hotelService;
        _budgetService = budgetService;
    }

    public async Task<Result<TravelPlanResponse>> GeneratePlanAsync(
        TravelRequest request,
        CancellationToken cancellationToken = default)
    {
        var flightResult = await _flightService.FindFlightAsync(
            request.Destination,
            cancellationToken);

        if(flightResult.IsFailure)
        {
            return Result<TravelPlanResponse>.Failure(flightResult.Error);
        }            

        var hotelResult = await _hotelService.FindHotelAsync(
            request.Destination,
            cancellationToken);

        if(hotelResult.IsFailure)
        {
            return Result<TravelPlanResponse>.Failure(hotelResult.Error);
        }

        var budgetResult = await _budgetService.AnalyzeBudgetAsync(
            request.Budget,
            cancellationToken);

        if(budgetResult.IsFailure)
        {
            return Result<TravelPlanResponse>.Failure(budgetResult.Error);
        }

        return Result<TravelPlanResponse>.Success(
            new TravelPlanResponse
            {
                Flight = flightResult.Value!,
                Hotel = hotelResult.Value!,
                Budget = budgetResult.Value!
            });
    }
}