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

    public async Task<TravelPlanResponse> GeneratePlanAsync(
        TravelRequest request,
        CancellationToken cancellationToken = default)
    {
        var flight = await _flightService.FindFlightAsync(
            request.Destination,
            cancellationToken);

        var hotel = await _hotelService.FindHotelAsync(
            request.Destination,
            cancellationToken);

        var budget = await _budgetService.AnalyzeBudgetAsync(
            request.Budget,
            cancellationToken);

        return new TravelPlanResponse
        {
            Flight = flight,
            Hotel = hotel,
            Budget = budget
        };
    }
}