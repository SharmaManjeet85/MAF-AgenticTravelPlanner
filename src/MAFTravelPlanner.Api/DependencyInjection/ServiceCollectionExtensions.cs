using MAFTravelPlanner.Application.Budget;
using MAFTravelPlanner.Application.Flight;
using MAFTravelPlanner.Application.Hotel;
using MAFTravelPlanner.Application.Interfaces;

using TravelPlanner.Application.Orchestration;

namespace MAFTravelPlanner.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTravelPlannerServices(
        this IServiceCollection services)
    {

        services.AddScoped<IFlightService, FlightService>();

        services.AddScoped<IHotelService, HotelService>();

        services.AddScoped<IBudgetService, BudgetService>();

        services.AddScoped<
            ITravelPlannerOrchestrator,
            TravelPlannerOrchestrator>();

        return services;
    }
}