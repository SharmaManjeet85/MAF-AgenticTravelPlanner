using System.Text;
using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Infrastructure.AI.Tools.Budget;

public sealed class BudgetTool : ITool
{
    public ToolDefinition Definition =>
        new(
            Name: "budget",
            Description:
                "Analyzes the travel budget and recommends a daily spending allocation.",
                Parameters:
                [
                    new ToolParameter(
                        "budget",
                        "Total trip budget.",
                        true),

                    new ToolParameter(
                        "days",
                        "Trip duration in days.",
                        true)
                ]);

    public Task<ToolResult> ExecuteAsync(
        ToolContext context,
        CancellationToken cancellationToken = default)
    {
        if (!context.Parameters.TryGetValue("budget", out var budgetValue) ||
            !context.Parameters.TryGetValue("days", out var daysValue))
        {
            return Task.FromResult(
                new ToolResult(
                    ToolName: Definition.Name,
                    Success: false,
                    Content: string.Empty,
                    Error: "Missing required parameters: budget or days."));
        }

        var budget = Convert.ToDecimal(budgetValue);
        var days = Convert.ToInt32(daysValue);

        if (days <= 0)
        {
            return Task.FromResult(
                new ToolResult(
                    ToolName: Definition.Name,
                    Success: false,
                    Content: string.Empty,
                    Error: "Trip duration must be greater than zero."));
        }

        var dailyBudget = budget / days;

        var hotel = Math.Round(dailyBudget * 0.50m, 2);
        var food = Math.Round(dailyBudget * 0.20m, 2);
        var transport = Math.Round(dailyBudget * 0.10m, 2);
        var activities = Math.Round(dailyBudget * 0.20m, 2);

        var rating =
            dailyBudget switch
            {
                < 3000m => "Budget",
                < 7000m => "Comfortable",
                _ => "Luxury"
            };

        var output = new StringBuilder();

        output.AppendLine("Budget Assessment");
        output.AppendLine("-----------------");
        output.AppendLine($"Total Budget : ₹{budget:N0}");
        output.AppendLine($"Duration     : {days} days");
        output.AppendLine($"Daily Budget : ₹{dailyBudget:N0}");
        output.AppendLine();
        output.AppendLine("Suggested Allocation");
        output.AppendLine($"Hotel       : ₹{hotel:N0}/day");
        output.AppendLine($"Food        : ₹{food:N0}/day");
        output.AppendLine($"Transport   : ₹{transport:N0}/day");
        output.AppendLine($"Activities  : ₹{activities:N0}/day");
        output.AppendLine();
        output.AppendLine($"Budget Rating : {rating}");

        return Task.FromResult(
            new ToolResult(
                ToolName: Definition.Name,
                Success: true,
                Content: output.ToString()));
    }
}