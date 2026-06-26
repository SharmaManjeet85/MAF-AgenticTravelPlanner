namespace MAFTravelPlanner.Application.AI.Models;

public sealed record LlmRequest(
    string Prompt,
    string? SystemPrompt = null,
    AiExecutionOptions? Options = null);