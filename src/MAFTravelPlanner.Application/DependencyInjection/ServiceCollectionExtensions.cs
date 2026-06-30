using Microsoft.Extensions.DependencyInjection;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Application.TravelAdvisor;
using MAFTravelPlanner.Application.AI.Planning;

namespace MAFTravelPlanner.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<
            ITravelAdvisorService,
            TravelAdvisorService>();
            
        services.AddScoped<IPlanner, Planner>();

        return services;
    }
}