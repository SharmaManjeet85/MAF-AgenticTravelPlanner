using Microsoft.Extensions.DependencyInjection;

namespace MAFTravelPlanner.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        return services;
    }
}