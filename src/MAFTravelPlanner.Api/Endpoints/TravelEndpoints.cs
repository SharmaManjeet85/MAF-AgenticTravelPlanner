using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Requests;


namespace MAFTravelPlanner.Api.Endpoints;

public static class TravelEndpoints
{
    public static IEndpointRouteBuilder MapTravelEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/api/travel/plan",
            async (
                TravelRequest request,
                ITravelPlannerOrchestrator orchestrator,
                CancellationToken cancellationToken) =>
            {
                var response =
                    await orchestrator.GeneratePlanAsync(
                        request,
                        cancellationToken);

                return Results.Ok(response);
            })
            .WithName("GenerateTravelPlan")
            .WithTags("Travel");

        return endpoints;
    }
}