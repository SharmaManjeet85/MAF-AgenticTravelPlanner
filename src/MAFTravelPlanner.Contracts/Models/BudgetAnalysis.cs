namespace MAFTravelPlanner.Contracts.Models;

public sealed record BudgetAnalysis
{
    public required decimal TotalBudget { get; init; }

    public required bool IsSufficient { get; init; }

    public required string Summary { get; init; }
}