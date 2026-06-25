using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Api.Endpoints;

public static class TravelAdvisorEndpoints
{
    public static IEndpointRouteBuilder
        MapTravelAdvisorEndpoints(
            this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/api/travel/advice",
            async (
                TravelAdviceRequest request,
                ITravelAdvisorService advisor,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await advisor.GetAdviceAsync(
                        request,
                        cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(
                        result.Error);
                }

                return Results.Ok(
                    result.Value);
            });

        return endpoints;
    }
}