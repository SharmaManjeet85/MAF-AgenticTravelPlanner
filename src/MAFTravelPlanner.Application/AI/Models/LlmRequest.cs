namespace MAFTravelPlanner.Application.AI.Models;

public sealed record LlmRequest(
    string Prompt,
    string? SystemPrompt = null,
    AiExecutionOptions? Options = null,
    AiPurpose Purpose = AiPurpose.Generation
    );

public enum AiPurpose
{
    Planning,
    Generation,
    Embedding
}