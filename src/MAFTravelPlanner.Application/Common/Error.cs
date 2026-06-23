namespace MAFTravelPlanner.Application.Common;

public sealed record Error(
    string Code,
    string Message)
{
    public static readonly Error None =
        new(string.Empty, string.Empty);

    public static Error Validation(
        string message)
        => new("VALIDATION_ERROR", message);

    public static Error FlightNotFound(
        string message)
        => new("FLIGHT_NOT_FOUND", message);

    public static Error HoteltNotFound(
        string message)
        => new("HOTEL_NOT_FOUND", message);
    
    public static Error BudgetExceeded(
        string message)
        => new("BUDGET_EXCEEDED", message);

    public static Error Conflict(
        string message)
        => new("CONFLICT", message);

    public static Error Internal(
        string message)
        => new("INTERNAL_ERROR", message);
}