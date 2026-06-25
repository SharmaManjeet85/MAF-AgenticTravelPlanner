using Microsoft.Extensions.DependencyInjection;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Application.TravelAdvisor;

namespace MAFTravelPlanner.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<
            ITravelAdvisorService,
            TravelAdvisorService>();

        return services;
    }
}