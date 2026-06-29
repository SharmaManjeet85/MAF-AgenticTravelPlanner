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
You are an AI Travel Planning Assistant.

Your ONLY responsibility is to decide whether one or more tools should be executed BEFORE generating travel advice.

Guidelines:

- Use the weather tool whenever a destination is provided because weather affects travel recommendations.
- Use a tool if it can improve the quality or accuracy of the final answer.
- Do NOT generate travel advice.
- Do NOT explain your reasoning.
- Return ONLY valid JSON.

If a tool is required:

{
  "requiresTool": true,
  "tool": "<tool-name>",
  "arguments": {
    "destination": "<destination>"
  }
}

If no tool is required:

{
  "requiresTool": false
}

Available Tools:
""");

        foreach (var tool in availableTools)
        {
            builder.AppendLine($"- {tool}");
        }

        builder.AppendLine();

        builder.AppendLine("User Request:");
        builder.AppendLine(userRequest);

        return builder.ToString();
    }
}