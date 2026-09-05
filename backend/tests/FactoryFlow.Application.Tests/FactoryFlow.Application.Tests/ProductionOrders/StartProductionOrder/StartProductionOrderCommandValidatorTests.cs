using FactoryFlow.Application.ProductionOrders.StartProductionOrder;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.Tests.ProductionOrders.StartProductionOrder;

public class StartProductionOrderCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidId_ShouldBeValid()
    {
        var command =
            new StartProductionOrderCommand(
                ProductionOrderId.New());

        var validator =
            new StartProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldBeInvalid()
    {
        var command =
            new StartProductionOrderCommand(
                new ProductionOrderId(Guid.Empty));

        var validator =
            new StartProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}