using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Budget;

public sealed class BudgetService : IBudgetService
{
    public Task<BudgetAnalysis> AnalyzeBudgetAsync(
        decimal budget,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new BudgetAnalysis
            {
                TotalBudget = budget,
                IsSufficient = budget >= 2000,
                Summary = budget >= 2000
                    ? "Budget appears sufficient."
                    : "Budget may be tight."
            });
    }
}