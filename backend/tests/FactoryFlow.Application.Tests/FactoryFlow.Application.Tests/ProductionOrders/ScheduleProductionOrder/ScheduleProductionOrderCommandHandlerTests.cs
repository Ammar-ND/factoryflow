using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.ProductionOrders.ScheduleProductionOrder;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using NSubstitute;

namespace FactoryFlow.Application.Tests.ProductionOrders.ScheduleProductionOrder;

public class ScheduleProductionOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenOrderExists_ShouldScheduleOrderAndReturnSuccess()
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

        productionOrderRepository
            .GetByIdAsync(
                productionOrder.Id,
                Arg.Any<CancellationToken>())
            .Returns(productionOrder);

        var handler = new ScheduleProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var command =
            new ScheduleProductionOrderCommand(
                productionOrder.Id);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(
            ProductionOrderStatus.Scheduled,
            productionOrder.Status);

        await unitOfWork
            .Received(1)
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

        var handler = new ScheduleProductionOrderCommandHandler(
            productionOrderRepository,
            unitOfWork);

        var command =
            new ScheduleProductionOrderCommand(
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