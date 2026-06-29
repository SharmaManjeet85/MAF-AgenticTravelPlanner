using MAFTravelPlanner.Application.Common;
using MAFTravelPlanner.Application.AI;
using MAFTravelPlanner.Application.AI.Models;
using MAFTravelPlanner.Application.AI.Planning;
using MAFTravelPlanner.Application.AI.Prompts;
using MAFTravelPlanner.Application.AI.Tools;
using MAFTravelPlanner.Application.Interfaces;
using MAFTravelPlanner.Contracts.TravelAdvisor;
using MAFTravelPlanner.Application.Prompts;

namespace MAFTravelPlanner.Application.TravelAdvisor;

public sealed class TravelAdvisorService : ITravelAdvisorService
{
    private readonly ILlmService _llmService;
    private readonly IToolExecutor _toolExecutor;

    public TravelAdvisorService(
        ILlmService llmService,
        IToolExecutor toolExecutor)
    {
        _llmService = llmService;
        _toolExecutor = toolExecutor;
    }

    public async Task<Result<TravelAdviceResponse>> GetAdviceAsync(
        TravelAdviceRequest request,
        CancellationToken cancellationToken = default)
    {
        //--------------------------------------------------------
        // STEP 1 : Ask the LLM whether a tool should be executed
        //--------------------------------------------------------

            var planningPrompt =
                ToolSelectionPromptBuilder.Build(
                    $"""
            Plan a {request.Days}-day {request.TravelStyle} trip to {request.Destination}
            with a budget of ₹{request.Budget}.

            Decide whether any tool should be used before generating travel advice.
            """,
            new[] { "weather" });

            var planningResponse =
                await _llmService.GenerateAsync(
                    new LlmRequest(
                        Prompt: planningPrompt,
                        SystemPrompt:
                            """
                            You are an AI planner.

                            Return ONLY valid JSON.

                            Do not explain your answer.
                            """,
                    Options:
                        new AiExecutionOptions(
                            AiResponseStyle.Deterministic)),
                            cancellationToken);

                Console.WriteLine("===== PLANNER OUTPUT =====");
                Console.WriteLine(planningResponse.Content);

                var plan =
                    PlanningResultParser.Parse(
                        planningResponse.Content);

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