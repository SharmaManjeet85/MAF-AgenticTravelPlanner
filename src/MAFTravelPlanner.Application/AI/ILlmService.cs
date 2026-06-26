using MAFTravelPlanner.Application.AI.Models;

namespace MAFTravelPlanner.Application.AI;

public interface ILlmService
{
    Task<LlmResponse> GenerateAsync(
        LlmRequest request,
        CancellationToken cancellationToken = default);
}