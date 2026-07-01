using System.Text.Json;

namespace MAFTravelPlanner.Application.AI.Planning;

public static class PlanningResultParser
{
    public static PlanningResult Parse(string json)
    {
        var dto =
            JsonSerializer.Deserialize<PlanningResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (dto?.ToolCalls == null)
        {
            return new PlanningResult(
                Array.Empty<PlannedToolCall>());
        }

        var calls =
            dto.ToolCalls
                .Select(tool =>
                    new PlannedToolCall(
                        tool.Tool,
                        tool.Arguments.ToDictionary(
                            x => x.Key,
                            x => ConvertValue(x.Value))))
                .ToList();

        return new PlanningResult(calls);
    }

    private static object? ConvertValue(JsonElement value)
    {
        return value.ValueKind switch
        {
            JsonValueKind.String =>
                value.GetString(),

            JsonValueKind.Number =>
                value.TryGetInt32(out var i)
                    ? i
                    : value.TryGetDecimal(out var d)
                        ? d
                        : value.GetDouble(),

            JsonValueKind.True => true,

            JsonValueKind.False => false,

            JsonValueKind.Null => null,

            _ => value.ToString()
        };
    }
}