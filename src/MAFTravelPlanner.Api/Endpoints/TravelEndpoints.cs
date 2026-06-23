using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Requests;
using FluentValidation;

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
                IValidator<TravelRequest> validator,
                ITravelPlannerOrchestrator orchestrator,
                CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var response =
                    await orchestrator.GeneratePlanAsync(
                        request,
                        cancellationToken);

                if (response.IsFailure)
                {
                    return response.Error.Code switch
                    {
                        "FLIGHT_NOT_FOUND"
                            => Results.NotFound(response.Error),
                        
                        "HOTEL_NOT_FOUND"
                            => Results.NotFound(response.Error),

                        "BUDGET_EXCEEDED"
                            => Results.Conflict(response.Error),

                        _ => Results.BadRequest(response.Error)
                    };
                }                

                return Results.Ok(response);
            })
            .WithName("GenerateTravelPlan")
            .WithTags("Travel");

        return endpoints;
    }
}