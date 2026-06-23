using FluentValidation;
using MAFTravelPlanner.Contracts.Requests;

namespace MAFTravelPlanner.Application.Validation;

public sealed class TravelRequestValidator
    : AbstractValidator<TravelRequest>
{
    public TravelRequestValidator()
    {
        RuleFor(x => x.Destination)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Days)
            .GreaterThan(0)
            .LessThanOrEqualTo(60);

        RuleFor(x => x.Budget)
            .GreaterThan(0);
    }
}