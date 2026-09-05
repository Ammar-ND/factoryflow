using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.ProductionOrders.CreateProductionOrder;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using NSubstitute;

namespace FactoryFlow.Application.Tests.ProductionOrders.CreateProductionOrder;

public class CreateProductionOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateProductionOrderAndReturnSuccess()
    {
        // Arrange
        var productionOrderRepository =
            Substitute.For<IProductionOrderRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        unitOfWork
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var handler = new CreateProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var productId = ProductId.New();

        var command = new CreateProductionOrderCommand(
            productId,
            100);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        await productionOrderRepository
            .Received(1)
            .AddAsync(
                Arg.Is<ProductionOrder>(order =>
                    order.ProductId == productId &&
                    order.Quantity == 100 &&
                    order.Status == ProductionOrderStatus.Draft),
                Arg.Any<CancellationToken>());

        await unitOfWork
            .Received(1)
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }
}