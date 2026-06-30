using System.Text;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

internal static class TravelAdvicePromptBuilder
{
    public static string Build(
        TravelAdviceRequest request,
        IEnumerable<ToolResult> toolResults)
    {
        var builder = new StringBuilder();

        builder.AppendLine("Travel Planning Context");
        builder.AppendLine("=======================");
        builder.AppendLine();

        foreach (var tool in toolResults.Where(t => t.Success))
        {
            builder.AppendLine($"{tool.ToolName.ToUpperInvariant()} OBSERVATION");
            builder.AppendLine("--------------------------------");
            builder.AppendLine(tool.Content);
            builder.AppendLine();
        }

        builder.AppendLine("User Request");
        builder.AppendLine("------------");
        builder.AppendLine($"Destination : {request.Destination}");
        builder.AppendLine($"Duration    : {request.Days} days");
        builder.AppendLine($"Budget      : ₹{request.Budget}");
        builder.AppendLine($"Travel Style: {request.TravelStyle}");
        builder.AppendLine();

        builder.AppendLine("""
        Instructions
        ------------

        Use every available observation while preparing your response.

        If budget information is available,
        recommend accommodation, food and activities accordingly.

        If weather information is available,
        adjust recommendations based on the weather.

        Provide:

        - itinerary
        - packing suggestions
        - local tips
        - estimated spending

        Keep the response concise.
        """);

        return builder.ToString();
    }
}