namespace MAFTravelPlanner.Application.AI.Tools;

public interface ITool
{
    ToolDefinition Definition { get; }

    Task<ToolResult> ExecuteAsync(
        ToolContext context,
        CancellationToken cancellationToken = default);
}