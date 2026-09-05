using FactoryFlow.Application.ProductionOrders.CreateProductionOrder;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Tests.ProductionOrders.CreateProductionOrder;

public class CreateProductionOrderCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_ShouldBeValid()
    {
        var command = new CreateProductionOrderCommand(
            ProductId.New(),
            100);

        var validator =
            new CreateProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithZeroQuantity_ShouldBeInvalid()
    {
        var command = new CreateProductionOrderCommand(
            ProductId.New(),
            0);

        var validator =
            new CreateProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_WithNegativeQuantity_ShouldBeInvalid()
    {
        var command = new CreateProductionOrderCommand(
            ProductId.New(),
            -10);

        var validator =
            new CreateProductionOrderCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}