using MAFTravelPlanner.Contracts.TravelAdvisor;
using MAFTravelPlanner.Application.Common;

namespace MAFTravelPlanner.Application.Interfaces;

public interface ITravelAdvisorService
{
    Task<Result<TravelAdviceResponse>> GetAdviceAsync(
        TravelAdviceRequest request,
        CancellationToken cancellationToken = default);
}