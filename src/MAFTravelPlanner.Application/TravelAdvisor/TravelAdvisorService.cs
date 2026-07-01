using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.AI.Models;
using MAFTravelPlanner.Application.AI.Planning;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

public sealed class TravelAdvisorService : ITravelAdvisorService
{
    private readonly ILlmService _llmService;
    private readonly IPlanner _planner;
    private readonly IToolExecutor _toolExecutor;

    public TravelAdvisorService(
        ILlmService llmService,
        IPlanner planner,
        IToolExecutor toolExecutor)
    {
        _llmService = llmService;
        _planner = planner;
        _toolExecutor = toolExecutor;
    }

    public async Task<Result<TravelAdviceResponse>> GetAdviceAsync(
        TravelAdviceRequest request,
        CancellationToken cancellationToken = default)
    {
        //----------------------------------------------------
        // STEP 1 : Planner
        //----------------------------------------------------

        var plan =
            await _planner.PlanAsync(
                new PlannerRequest(
                    $"""
                    Plan a {request.Days}-day {request.TravelStyle} trip to {request.Destination}
                    with a budget of ₹{request.Budget}.

                    Decide which tools should be executed before generating travel advice.
                    """),
                cancellationToken);

        //----------------------------------------------------
        // STEP 2 : Execute all tools in parallel
        //----------------------------------------------------

        var toolTasks =
            plan.ToolCalls.Select(async toolCall =>
            {
                var context =
                    new ToolContext(
                        toolCall.Arguments.ToDictionary(
                            x => x.Key,
                            x => (object?)x.Value));

                Console.WriteLine($"Executing Tool : {toolCall.ToolName}");

                return await _toolExecutor.ExecuteAsync(
                    toolCall.ToolName,
                    context,
                    cancellationToken);
            });

        var toolResults =
            (await Task.WhenAll(toolTasks))
            .ToList();

        //----------------------------------------------------
        // STEP 3 : Generate final response
        //----------------------------------------------------

        var prompt =
            TravelAdvicePromptBuilder.Build(
                request,
                toolResults);

        var llmResponse =
            await _llmService.GenerateAsync(
                new LlmRequest(
                    Prompt: prompt,
                    SystemPrompt:
                        "You are an experienced travel advisor.",
                    Options:
                        new AiExecutionOptions(
                            AiResponseStyle.Balanced),
                    Purpose: AiPurpose.Generation),
                cancellationToken);

        return Result<TravelAdviceResponse>.Success(
            new TravelAdviceResponse(
                llmResponse.Content));
    }
}