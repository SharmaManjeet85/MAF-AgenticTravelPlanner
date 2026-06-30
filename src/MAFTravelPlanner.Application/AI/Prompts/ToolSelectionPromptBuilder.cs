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
        You are an AI Travel Planning Assistant.

        Your ONLY responsibility is to decide whether one tool should be executed before generating travel advice.

        Return ONLY valid JSON.

        If a tool is required:

        {
          "requiresTool": true,
          "tool": "<tool-name>",
          "arguments": {
          }
        }

        Otherwise:

        {
          "requiresTool": false
        }

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