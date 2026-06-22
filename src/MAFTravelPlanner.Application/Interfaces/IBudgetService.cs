using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IBudgetService
{
    Task<BudgetAnalysis> AnalyzeBudgetAsync(
        decimal budget,
        CancellationToken cancellationToken = default);
}