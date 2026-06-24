using MAFTravelPlanner.Application.Interfaces;

namespace MAFTravelPlanner.Api.Endpoints;

public static class AiEndpoints
{
    public static IEndpointRouteBuilder
        MapAiEndpoints(
            this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/api/ai/test",
            async (
                ILlmService llmService,
                CancellationToken cancellationToken) =>
            {
                var response =
                    await llmService.GenerateAsync(
                        "Explain microservices in 3 lines.",
                        cancellationToken);

                return Results.Ok(response);
            });

        return endpoints;
    }
}