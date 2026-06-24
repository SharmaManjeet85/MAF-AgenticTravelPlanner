namespace MAFTravelPlanner.Infrastructure.LLM;

public sealed class OllamaException : Exception
{
    public OllamaException(string message)
        : base(message)
    {
    }
}