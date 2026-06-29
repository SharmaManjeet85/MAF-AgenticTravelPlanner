using System.Text.Json;

namespace MAFTravelPlanner.Application.AI.Planning;

public static class PlanningResultParser
{
    public static PlanningResult Parse(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return PlanningResult.Empty;
        }

        try
        {
            var dto = JsonSerializer.Deserialize<PlanningResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (dto is null)
            {
                return PlanningResult.Empty;
            }

            return new PlanningResult(
                dto.RequiresTool,
                dto.Tool,
                dto.Arguments);
        }
        catch
        {
            // If the LLM returns invalid JSON,
            // fail gracefully.
            return PlanningResult.Empty;
        }
    }
}