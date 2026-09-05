using FluentValidation;

namespace FactoryFlow.Application.ProductionOrders.CreateProductionOrder;

public sealed class CreateProductionOrderCommandValidator
    : AbstractValidator<CreateProductionOrderCommand>
{
    public CreateProductionOrderCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty();

        RuleFor(command => command.Quantity)
            .GreaterThan(0);
    }
}