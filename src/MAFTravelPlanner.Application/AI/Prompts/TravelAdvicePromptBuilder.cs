using System.Text;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.Prompts;

internal static class TravelAdvicePromptBuilder
{
    public static string Build(
        TravelAdviceRequest request,
        IReadOnlyCollection<ToolResult> toolResults)
    {
        var prompt = new StringBuilder();

        prompt.AppendLine("""
        Travel Planning Context

        Tool Observations
        -----------------

        """);

        foreach (var tool in toolResults)
        {
            prompt.AppendLine($"Tool : {tool.ToolName}");

            if (tool.Success)
            {
                prompt.AppendLine(tool.Content);
            }
            else
            {
                prompt.AppendLine($"Error : {tool.Error}");
            }

            prompt.AppendLine();
        }

        prompt.AppendLine($"""
        User Request
        ------------

        Destination : {request.Destination}

        Duration    : {request.Days} days

        Budget      : ₹{request.Budget}

        Travel Style: {request.TravelStyle}

        Instructions
        ------------

        Prepare a practical travel itinerary.

        Use all available observations.

        Keep the response concise.
        """);

        return prompt.ToString();
    }
}