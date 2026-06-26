namespace MAFTravelPlanner.Application.AI.Models;

public sealed record LlmResponse(
    string Content,
    AiResponseMetadata Metadata);