using FactoryFlow.Application.ProductionOrders.CancelProductionOrder;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.Tests.ProductionOrders.CancelProductionOrder;

public class CancelProductionOrderCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidId_ShouldBeValid()
    {
        var command =
            new CancelProductionOrderCommand(
                ProductionOrderId.New());

        var validator =
            new CancelProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldBeInvalid()
    {
        var command =
            new CancelProductionOrderCommand(
                new ProductionOrderId(Guid.Empty));

        var validator =
            new CancelProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}