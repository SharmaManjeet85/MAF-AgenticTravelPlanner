using System.Text.Json;

namespace MAFTravelPlanner.Application.AI.Planning;

public static class PlanningResultParser
{
    public static PlanningResult Parse(string json)
    {
        var result = JsonSerializer.Deserialize<PlanningResult>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (result is null)
        {
            throw new InvalidOperationException(
                "Unable to parse planning response.");
        }

        return result;
    }
}