namespace MAFTravelPlanner.Application.AI.Models;

public sealed record AiResponseMetadata(
    string Model,
    TimeSpan Duration);