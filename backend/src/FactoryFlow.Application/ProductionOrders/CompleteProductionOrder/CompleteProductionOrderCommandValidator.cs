using FluentValidation;

namespace FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;

public sealed class CompleteProductionOrderCommandValidator
    : AbstractValidator<CompleteProductionOrderCommand>
{
    public CompleteProductionOrderCommandValidator()
    {
        RuleFor(command => command.ProductionOrderId)
            .NotEmpty();
    }
}