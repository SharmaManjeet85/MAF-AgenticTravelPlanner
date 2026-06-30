namespace MAFTravelPlanner.Application.AI.Tools;

public interface IToolExecutor
{
    Task<ToolResult> ExecuteAsync(
        string toolName,
        ToolContext context,
        CancellationToken cancellationToken = default);

    IReadOnlyCollection<ToolDefinition> GetAvailableTools();
}