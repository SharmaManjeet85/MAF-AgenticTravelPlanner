using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

public sealed class TravelAdvisorService
    : ITravelAdvisorService
{
    private readonly ILlmService _llmService;

    public TravelAdvisorService(
        ILlmService llmService)
    {
        _llmService = llmService;
    }

    public async Task<Result<TravelAdviceResponse>>
        GetAdviceAsync(
            TravelAdviceRequest request,
            CancellationToken cancellationToken = default)
    {
        var prompt =
            PromptBuilder.Build(request);

        var advice =
            await _llmService.GenerateAsync(
                new LlmRequest(prompt),
                cancellationToken);

        return Result<TravelAdviceResponse>.Success(
            new TravelAdviceResponse(advice));
    }
}