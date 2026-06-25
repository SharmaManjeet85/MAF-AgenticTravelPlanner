namespace MAFTravelPlanner.Application.AI;

public sealed record LlmResponse(
    string Content,
    string Model);