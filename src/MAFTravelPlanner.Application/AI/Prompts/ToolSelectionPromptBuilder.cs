using System.Text;

namespace MAFTravelPlanner.Application.AI.Prompts;

public static class ToolSelectionPromptBuilder
{
    public static string Build(
        string userRequest,
        IEnumerable<string> availableTools)
    {
        var builder = new StringBuilder();

        builder.AppendLine("""
You are an AI planner.

Your responsibility is ONLY to decide
whether a tool should be executed.

Return ONLY valid JSON.

If no tool is required return

{
  "requiresTool": false
}

If a tool is required return

{
  "requiresTool": true,
  "tool": "<tool-name>",
  "arguments": {
  }
}

Available Tools

""");

        foreach (var tool in availableTools)
        {
            builder.AppendLine($"- {tool}");
        }

        builder.AppendLine();

        builder.AppendLine("User Request");

        builder.AppendLine(userRequest);

        return builder.ToString();
    }
}