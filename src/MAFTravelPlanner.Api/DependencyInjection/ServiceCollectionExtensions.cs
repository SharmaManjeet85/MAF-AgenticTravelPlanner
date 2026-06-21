using MAFTravelPlanner.Tools;
using MAFTravelPlanner.Tools.Interfaces;

namespace MAFTravelPlanner.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTravelPlannerServices(
        this IServiceCollection services)
    {
        services.AddScoped<ITravelPlanningTool,
                           TravelPlanningTool>();

        return services;
    }
}