using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Domain.Tests.ProductionOrders;

public class ProductionOrderTests
{
    [Fact]
    public void Create_WhenQuantityIsZero_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();

        // Act
        var result = ProductionOrder.Create(productId, 0);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Production order quantity must be greater than zero.",
            result.Error);
    }

    [Fact]
    public void Create_WhenQuantityIsNegative_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();

        // Act
        var result = ProductionOrder.Create(productId, -10);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Production order quantity must be greater than zero.",
            result.Error);
    }

    [Fact]
    public void Create_WhenQuantityIsValid_ShouldCreateDraftProductionOrder()
    {
        // Arrange
        var productId = ProductId.New();

        // Act
        var result = ProductionOrder.Create(productId, 100);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(productId, result.Value.ProductId);
        Assert.Equal(100, result.Value.Quantity);
        Assert.Equal(
            ProductionOrderStatus.Draft,
            result.Value.Status);
    }

    [Fact]
    public void Schedule_WhenOrderIsDraft_ShouldSetStatusToScheduled()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        // Act
        var result = order.Schedule();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(
            ProductionOrderStatus.Scheduled,
            order.Status);
    }

    [Fact]
    public void Schedule_WhenOrderIsAlreadyScheduled_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();

        // Act
        var result = order.Schedule();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Only draft production orders can be scheduled.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.Scheduled,
            order.Status);
    }

    [Fact]
    public void Start_WhenOrderIsScheduled_ShouldSetStatusToInProgress()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();

        // Act
        var result = order.Start();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(
            ProductionOrderStatus.InProgress,
            order.Status);
    }

    [Fact]
    public void Start_WhenOrderIsDraft_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        // Act
        var result = order.Start();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Only scheduled production orders can be started.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.Draft,
            order.Status);
    }

    [Fact]
    public void Complete_WhenOrderIsInProgress_ShouldSetStatusToCompleted()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();
        order.Start();

        // Act
        var result = order.Complete();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(
            ProductionOrderStatus.Completed,
            order.Status);
    }

    [Fact]
    public void Complete_WhenOrderIsNotInProgress_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        // Act
        var result = order.Complete();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Only production orders in progress can be completed.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.Draft,
            order.Status);
    }

    [Fact]
    public void Cancel_WhenOrderIsDraft_ShouldSetStatusToCancelled()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        // Act
        var result = order.Cancel();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(
            ProductionOrderStatus.Cancelled,
            order.Status);
    }

    [Fact]
    public void Cancel_WhenOrderIsScheduled_ShouldSetStatusToCancelled()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();

        // Act
        var result = order.Cancel();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(
            ProductionOrderStatus.Cancelled,
            order.Status);
    }

    [Fact]
    public void Cancel_WhenOrderIsInProgress_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();
        order.Start();

        // Act
        var result = order.Cancel();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Production order cannot be cancelled while in progress.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.InProgress,
            order.Status);
    }

    [Fact]
    public void Cancel_WhenOrderIsCompleted_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Schedule();
        order.Start();
        order.Complete();

        // Act
        var result = order.Cancel();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Completed production orders cannot be cancelled.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.Completed,
            order.Status);
    }

    [Fact]
    public void Cancel_WhenOrderIsAlreadyCancelled_ShouldReturnFailure()
    {
        // Arrange
        var productId = ProductId.New();
        var createResult = ProductionOrder.Create(productId, 100);

        var order = createResult.Value!;

        order.Cancel();

        // Act
        var result = order.Cancel();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Production order is already cancelled.",
            result.Error);

        Assert.Equal(
            ProductionOrderStatus.Cancelled,
            order.Status);
    }
}