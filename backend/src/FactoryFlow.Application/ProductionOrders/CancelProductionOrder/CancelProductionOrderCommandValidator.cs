using FluentValidation;

namespace FactoryFlow.Application.ProductionOrders.CancelProductionOrder;

public sealed class CancelProductionOrderCommandValidator
    : AbstractValidator<CancelProductionOrderCommand>
{
    public CancelProductionOrderCommandValidator()
    {
        RuleFor(command => command.ProductionOrderId)
            .NotEmpty();
    }
}