using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.AI.Models;
using MAFTravelPlanner.Application.AI.Planning;
using MAFTravelPlanner.Application.AI.Prompts;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;

namespace MAFTravelPlanner.Application.TravelAdvisor;

public sealed class TravelAdvisorService : ITravelAdvisorService
{
    private readonly ILlmService _llmService;
    private readonly IToolExecutor _toolExecutor;

    private readonly IPlanner _planner;

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
        //--------------------------------------------------------
        // STEP 1 : Ask the LLM whether a tool should be executed
        //--------------------------------------------------------

            var plan =
            await _planner.PlanAsync(
                new PlannerRequest(
                    $"""
                    Plan a {request.Days}-day {request.TravelStyle} trip to {request.Destination}
                    with a budget of ₹{request.Budget}.

                    Decide whether any tool should be used before generating travel advice.
                    """),
                cancellationToken);
        //--------------------------------------------------------
        // STEP 2 : Execute the selected tool (if any)
        //--------------------------------------------------------

                var toolResults = new List<ToolResult>();

                if (plan.RequiresTool &&
                    !string.IsNullOrWhiteSpace(plan.Tool))
                {
                    var context =
                        new ToolContext(
                            plan.Arguments.ToDictionary(
                                x => x.Key,
                                x => (object?)x.Value));

                    var toolResult =
                        await _toolExecutor.ExecuteAsync(
                            plan.Tool,
                            context,
                            cancellationToken);

                    toolResults.Add(toolResult);
                }

        //--------------------------------------------------------
        // STEP 3 : Generate the final travel advice
        //--------------------------------------------------------

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
                                AiResponseStyle.Balanced)),
                    cancellationToken);

        return Result<TravelAdviceResponse>.Success(
            new TravelAdviceResponse(
                llmResponse.Content));
    }
}