using MAFTravelPlanner.Contracts.Requests;
using MAFTravelPlanner.Tools.Interfaces;

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
                ITravelPlanningTool service,
                CancellationToken cancellationToken) =>
            {
                var response =
                    await service.GeneratePlanAsync(
                        request,
                        cancellationToken);

                return Results.Ok(response);
            })
            .WithName("GenerateTravelPlan")
            .WithTags("Travel");

        return endpoints;
    }
}