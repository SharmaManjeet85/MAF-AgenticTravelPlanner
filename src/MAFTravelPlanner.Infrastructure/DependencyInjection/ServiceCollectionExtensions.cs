using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MAFTravelPlanner.Infrastructure.Configuration;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Infrastructure.LLM;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Infrastructure.Tools;
using MAFTravelPlanner.Infrastructure.AI.Tools.Budget;

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

        services.AddHttpClient<ILlmService, OllamaLlmService>(
            (sp, client) =>
            {
                var options =
                    sp.GetRequiredService<
                        Microsoft.Extensions.Options.IOptions<OllamaOptions>>()
                    .Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(
                    options.TimeoutSeconds);
            });
        services.AddScoped<ITool, WeatherTool>();
        services.AddScoped<ITool, BudgetTool>();
        services.AddScoped<IToolExecutor, ToolExecutor>();

        return services;
    }
}