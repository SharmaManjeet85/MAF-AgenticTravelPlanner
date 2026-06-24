using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MAFTravelPlanner.Infrastructure.Configuration;

namespace MAFTravelPlanner.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OllamaOptions>(
            configuration.GetSection(
                OllamaOptions.SectionName));

        return services;
    }
}