using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

internal static class PromptBuilder
{
    public static string Build(
    TravelAdviceRequest request,
    ToolResult weatherResult)
    {
        return $"""
    Travel Planning Context

    Weather Information
    -------------------

    {weatherResult.Content}

    User Request
    ------------

    Destination : {request.Destination}

    Duration    : {request.Days} days

    Budget      : ₹{request.Budget}

    Travel Style: {request.TravelStyle}

    Instructions
    ------------

    Prepare a practical travel itinerary.

    Use the weather information while making recommendations.

    Suggest suitable outdoor and indoor activities.

    Keep the response concise.

    """;
    }
}