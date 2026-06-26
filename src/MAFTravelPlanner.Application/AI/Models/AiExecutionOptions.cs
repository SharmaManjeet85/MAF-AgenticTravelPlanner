namespace MAFTravelPlanner.Application.AI.Models;

public sealed record AiExecutionOptions(
    AiResponseStyle ResponseStyle = AiResponseStyle.Balanced,
    int? MaxOutputTokens = null);