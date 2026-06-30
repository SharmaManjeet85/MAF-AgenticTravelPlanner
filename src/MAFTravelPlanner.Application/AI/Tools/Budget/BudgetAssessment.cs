namespace MAFTravelPlanner.Application.AI.Tools.Budget;

public sealed record BudgetAssessment(
    decimal DailyBudget,
    decimal Hotel,
    decimal Food,
    decimal Transport,
    decimal Activities,
    string Rating);