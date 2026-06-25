using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

internal static class PromptBuilder
{
    public static string Build(
        TravelAdviceRequest request)
    {
        return $"""
You are an experienced travel advisor.

Destination: {request.Destination}
Duration: {request.Days} days
Budget: {request.Budget}
Travel Style: {request.TravelStyle}

Provide:

1. Recommended itinerary
2. Budget allocation
3. Key attractions
4. Travel tips

Keep response concise.
""";
    }
}