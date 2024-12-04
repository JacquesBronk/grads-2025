using FluentValidation;
using Retro.Stock.Contracts.Request;

namespace Retro.Stock.Contracts.Validation;

public class UpsertStockRequestValidationRuleSet: AbstractValidator<UpsertStockRequest>
{
    public UpsertStockRequestValidationRuleSet()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        RuleFor(x => x.Tags).NotEmpty().WithMessage("Tags are required");
        RuleFor(x => x.Sku).NotEmpty().WithMessage("Sku is required");
        RuleFor(x => x.Condition).IsInEnum().WithMessage("Invalid Condition, must be New = 0, Used = 1 or Refurbished = 2");
        RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required");
        RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required");
    }

}