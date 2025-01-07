using FluentValidation;
using Retro.Greeter.Contracts.Request;

namespace Retro.Greeter.Contracts.Validation;

public class CreateRequestValidationRuleSet : AbstractValidator<CreateSessionRequest>
{
    public CreateRequestValidationRuleSet()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.EntryEpoch).GreaterThan(0).WithMessage("EntryEpoch must be greater than 0");
        RuleFor(x => x.ExitEpoch).GreaterThan(0).WithMessage("ExitEpoch must be greater than 0");
        RuleFor(x => x.Route).NotEmpty().WithMessage("Route is required");
        RuleFor(x => x.UserAgent).NotEmpty().WithMessage("UserAgent is required");
        RuleFor(x => x.IpAddress).NotEmpty().WithMessage("IpAddress is required");
    }
}