using FactoryFlow.Application.ProductionOrders.ScheduleProductionOrder;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.Tests.ProductionOrders.ScheduleProductionOrder;

public class ScheduleProductionOrderCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidId_ShouldBeValid()
    {
        var command =
            new ScheduleProductionOrderCommand(
                ProductionOrderId.New());

        var validator =
            new ScheduleProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldBeInvalid()
    {
        var command =
            new ScheduleProductionOrderCommand(
                new ProductionOrderId(Guid.Empty));

        var validator =
            new ScheduleProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}