using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.ProductionOrders.CancelProductionOrder;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using NSubstitute;

namespace FactoryFlow.Application.Tests.ProductionOrders.CancelProductionOrder;

public class CancelProductionOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenOrderIsDraft_ShouldCancelOrderAndReturnSuccess()
    {
        var productionOrderRepository =
            Substitute.For<IProductionOrderRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        var createResult = ProductionOrder.Create(
            ProductId.New(),
            100);

        var productionOrder = createResult.Value!;

        productionOrderRepository
            .GetByIdAsync(
                productionOrder.Id,
                Arg.Any<CancellationToken>())
            .Returns(productionOrder);

        var handler = new CancelProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var command = new CancelProductionOrderCommand(
            productionOrder.Id);

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.True(result.IsSuccess);

        Assert.Equal(
            ProductionOrderStatus.Cancelled,
            productionOrder.Status);

        await unitOfWork
            .Received(1)
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenOrderIsInProgress_ShouldReturnFailure()
    {
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

        var handler = new CancelProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var command = new CancelProductionOrderCommand(
            productionOrder.Id);

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.True(result.IsFailure);

        Assert.Equal(
            ProductionOrderStatus.InProgress,
            productionOrder.Status);

        await unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenOrderDoesNotExist_ShouldReturnFailure()
    {
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

        var handler = new CancelProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var command = new CancelProductionOrderCommand(
            productionOrderId);

        var result = await handler.Handle(
            command,
            CancellationToken.None);

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