using System.Text;
using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Application.AI.Prompts;

public static class ToolSelectionPromptBuilder
{
    public static string Build(
        string userRequest,
        IEnumerable<ToolDefinition> availableTools)
    {
        var builder = new StringBuilder();

        builder.AppendLine("""
        You are an AI Planner.

        Your ONLY responsibility is to decide which tools should be executed.

        You MUST NOT answer the user's request.

        Return ONLY valid JSON.

        The JSON MUST exactly follow this schema:

        {
        "toolCalls": [
            {
            "tool": "<tool-name>",
            "arguments": {
            }
            }
        ]
        }

        Rules

        - You may return zero, one or multiple tool calls.
        - If no tool is required, return:

        {
        "toolCalls": []
        }

        Never explain your answer.

        Never return Markdown.

        Return JSON only.

        Available Tools

        ========================

        """);

        foreach (var tool in availableTools)
        {
            builder.AppendLine($"Tool: {tool.Name}");
            builder.AppendLine();

            builder.AppendLine("Description:");
            builder.AppendLine(tool.Description);
            builder.AppendLine();

            builder.AppendLine("Parameters:");

            foreach (var parameter in tool.Parameters)
            {
                builder.AppendLine(
                    $"- {parameter.Name} {(parameter.Required ? "(required)" : "(optional)")}");
                builder.AppendLine($"  {parameter.Description}");
            }

            builder.AppendLine();
            builder.AppendLine("----------------------------------------");
            builder.AppendLine();
        }

        builder.AppendLine("User Request");
        builder.AppendLine();
        builder.AppendLine(userRequest);

        return builder.ToString();
    }
}