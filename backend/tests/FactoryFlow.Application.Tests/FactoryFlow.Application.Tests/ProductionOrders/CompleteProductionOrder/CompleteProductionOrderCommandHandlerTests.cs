using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using NSubstitute;

namespace FactoryFlow.Application.Tests.ProductionOrders.CompleteProductionOrder;

public class CompleteProductionOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenOrderIsInProgress_ShouldCompleteOrderAndReturnSuccess()
    {
        // Arrange
        var productionOrderRepository =
            Substitute.For<IProductionOrderRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        var createResult = ProductionOrder.Create(
            ProductId.New(),
            100);

        var productionOrder = createResult.Value!;

        productionOrder.Schedule();
        productionOrder.Start();

        productionOrderRepository
            .GetByIdAsync(
                productionOrder.Id,
                Arg.Any<CancellationToken>())
            .Returns(productionOrder);

        var handler =
            new CompleteProductionOrderCommandHandler(
                productionOrderRepository,
                unitOfWork);

        var command =
            new CompleteProductionOrderCommand(
                productionOrder.Id);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(
            ProductionOrderStatus.Completed,
            productionOrder.Status);

        await unitOfWork
            .Received(1)
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenOrderIsScheduled_ShouldReturnFailure()
    {
        // Arrange
        var productionOrderRepository =
            Substitute.For<IProductionOrderRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        var createResult = ProductionOrder.Create(
            ProductId.New(),
            100);

        var productionOrder = createResult.Value!;

        productionOrder.Schedule();

        productionOrderRepository
            .GetByIdAsync(
                productionOrder.Id,
                Arg.Any<CancellationToken>())
            .Returns(productionOrder);

        var handler =
            new CompleteProductionOrderCommandHandler(
                productionOrderRepository,
                unitOfWork);

        var command =
            new CompleteProductionOrderCommand(
                productionOrder.Id);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);

        Assert.Equal(
            ProductionOrderStatus.Scheduled,
            productionOrder.Status);

        await unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenOrderDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var productionOrderRepository =
            Substitute.For<IProductionOrderRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        var productionOrderId =
            ProductionOrderId.New();

        productionOrderRepository
            .GetByIdAsync(
                productionOrderId,
                Arg.Any<CancellationToken>())
            .Returns((ProductionOrder?)null);

        var handler =
            new CompleteProductionOrderCommandHandler(
                productionOrderRepository,
                unitOfWork);

        var command =
            new CompleteProductionOrderCommand(
                productionOrderId);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);

        Assert.Equal(
            "Production order was not found.",
            result.Error);

        await unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }
}