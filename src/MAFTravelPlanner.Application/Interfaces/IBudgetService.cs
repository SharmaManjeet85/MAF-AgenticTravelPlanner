using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Contracts.Models;

namespace MAFTravelPlanner.Application.Interfaces;

public interface IBudgetService
{
    Task<Result<BudgetAnalysis>> AnalyzeBudgetAsync(
        decimal budget,
        CancellationToken cancellationToken = default);
}