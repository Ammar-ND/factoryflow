using FluentValidation;

namespace FactoryFlow.Application.ProductionOrders.ScheduleProductionOrder;

public sealed class ScheduleProductionOrderCommandValidator
    : AbstractValidator<ScheduleProductionOrderCommand>
{
    public ScheduleProductionOrderCommandValidator()
    {
        RuleFor(command => command.ProductionOrderId)
            .NotEmpty();
    }
}