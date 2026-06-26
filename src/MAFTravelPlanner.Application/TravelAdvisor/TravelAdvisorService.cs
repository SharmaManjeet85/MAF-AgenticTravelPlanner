using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;
using MAFTravelPlanner.Application.AI.Models;
using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Application.TravelAdvisor;

public sealed class TravelAdvisorService
    : ITravelAdvisorService
{
    private readonly ILlmService _llmService;
    private readonly IToolExecutor _toolExecutor;

    public TravelAdvisorService(
        ILlmService llmService, IToolExecutor toolExecutor)
    {
        _llmService = llmService;
        _toolExecutor = toolExecutor;
    }

    public async Task<Result<TravelAdviceResponse>>
        GetAdviceAsync(
            TravelAdviceRequest request,
            CancellationToken cancellationToken = default)
    {
        var weather =
        await _toolExecutor.ExecuteAsync(
            "weather",
             new ToolContext(
                new Dictionary<string, object?>
                {
                    ["destination"] = request.Destination
                }),
            cancellationToken);

        var prompt =
            PromptBuilder.Build(request,weatherResult: weather);

        var llmResponse = await _llmService.GenerateAsync(
        new LlmRequest(
            Prompt: prompt,
            SystemPrompt:
                "You are an experienced travel advisor.",
            Options:
                new AiExecutionOptions(
                    AiResponseStyle.Balanced)),
        cancellationToken);

        return Result<TravelAdviceResponse>.Success(
            new TravelAdviceResponse(llmResponse.Content));
    }
}