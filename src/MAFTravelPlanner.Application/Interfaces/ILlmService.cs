namespace MAFTravelPlanner.Application.Interfaces;

public interface ILlmService
{
    Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}