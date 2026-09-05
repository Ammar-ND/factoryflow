using FluentValidation;

namespace FactoryFlow.Application.ProductionOrders.StartProductionOrder;

public sealed class StartProductionOrderCommandValidator
    : AbstractValidator<StartProductionOrderCommand>
{
    public StartProductionOrderCommandValidator()
    {
        RuleFor(command => command.ProductionOrderId)
            .NotEmpty();
    }
}