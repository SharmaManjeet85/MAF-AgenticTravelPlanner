using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Budget;

public sealed class BudgetService : IBudgetService
{
    public Task<Result<BudgetAnalysis>> AnalyzeBudgetAsync(
        decimal budget,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Result<BudgetAnalysis>.Success(
                new BudgetAnalysis
            {
                TotalBudget = budget,
                IsSufficient = budget >= 2000,
                Summary = budget >= 2000
                    ? "Budget appears sufficient."
                    : "Budget may be tight."
            }));
    }
}