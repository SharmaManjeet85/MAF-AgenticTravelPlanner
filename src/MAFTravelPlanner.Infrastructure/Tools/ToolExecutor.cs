using MAFTravelPlanner.Application.AI.Tools;

namespace MAFTravelPlanner.Infrastructure.Tools;

public sealed class ToolExecutor : IToolExecutor
{
    private readonly IReadOnlyDictionary<string, ITool> _tools;

    public ToolExecutor(IEnumerable<ITool> tools)
    {
        _tools = tools.ToDictionary(
            x => x.Definition.Name,
            StringComparer.OrdinalIgnoreCase);
    }

    public async Task<ToolResult> ExecuteAsync(
        string toolName,
        ToolContext context,
        CancellationToken cancellationToken = default)
    {
        if (!_tools.TryGetValue(
            toolName,
            out var tool))
        {
            return new ToolResult(
                toolName,
                false,
                string.Empty,
                $"Tool '{toolName}' not found.");
        }

        return await tool.ExecuteAsync(
            context,
            cancellationToken);
    }
}