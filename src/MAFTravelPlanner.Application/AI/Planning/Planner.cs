using MAFTravelPlanner.Application.AI.Models;
using MAFTravelPlanner.Application.AI.Prompts;
using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Application.AI.Planning;

public sealed class Planner : IPlanner
{
    private readonly ILlmService _llmService;
    private readonly IToolExecutor _toolExecutor;

    public Planner(
        ILlmService llmService,
        IToolExecutor toolExecutor)
    {
        _llmService = llmService;
        _toolExecutor = toolExecutor;
    }

    public async Task<PlanningResult> PlanAsync(
        PlannerRequest request,
        CancellationToken cancellationToken = default)
    {
        var prompt =
            ToolSelectionPromptBuilder.Build(
                request.UserRequest,
                _toolExecutor.GetAvailableTools());

        Console.WriteLine("===== PLANNER PROMPT =====");
        Console.WriteLine(prompt);

       var response =
                 await _llmService.GenerateAsync(
        new LlmRequest(
            Prompt: prompt,
            SystemPrompt:
            """
            You are an AI planner.

            Return ONLY valid JSON.
            """,
            Options: new AiExecutionOptions(
                AiResponseStyle.Deterministic),
            Purpose: AiPurpose.Planning),
        cancellationToken);

        Console.WriteLine("===== PLANNER OUTPUT =====");
        Console.WriteLine(response.Content);

        return PlanningResultParser.Parse(
            response.Content);
    }
}