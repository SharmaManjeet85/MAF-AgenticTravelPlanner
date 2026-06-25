namespace MAFTravelPlanner.Application.AI;

public interface ILlmService
{
    Task<string> GenerateAsync(
        LlmRequest request,
        CancellationToken cancellationToken = default);
}