namespace MAFTravelPlanner.Infrastructure.Configuration;

public sealed class OllamaOptions
{
    public const string SectionName = "Ollama";

    public string BaseUrl { get; set; } = string.Empty;

    public string PlannerModel { get; set; } = string.Empty;

    public string GeneralModel { get; set; } = string.Empty;

    public int TimeoutSeconds { get; set; }
}