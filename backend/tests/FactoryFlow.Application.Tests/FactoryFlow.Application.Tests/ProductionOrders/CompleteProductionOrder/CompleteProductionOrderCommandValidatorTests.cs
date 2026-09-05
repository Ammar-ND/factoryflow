using FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.Tests.ProductionOrders.CompleteProductionOrder;

public class CompleteProductionOrderCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidId_ShouldBeValid()
    {
        var command =
            new CompleteProductionOrderCommand(
                ProductionOrderId.New());

        var validator =
            new CompleteProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldBeInvalid()
    {
        var command =
            new CompleteProductionOrderCommand(
                new ProductionOrderId(Guid.Empty));

        var validator =
            new CompleteProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}