using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Domain.ProductionOrders;

public sealed class ProductionOrder : AggregateRoot<ProductionOrderId>
{
    public ProductId ProductId { get; private set; }

    public int Quantity { get; private set; }

    public ProductionOrderStatus Status { get; private set; }

    private ProductionOrder(
        ProductionOrderId id,
        ProductId productId,
        int quantity)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        Status = ProductionOrderStatus.Draft;
    }

    public static Result<ProductionOrder> Create(
    ProductId productId,
    int quantity)
    {
        if (quantity <= 0)
        {
            return Result<ProductionOrder>.Failure(
                "Production order quantity must be greater than zero.");
        }

        var productionOrder = new ProductionOrder(
            ProductionOrderId.New(),
            productId,
            quantity);

        return Result<ProductionOrder>.Success(productionOrder);
    }

    public Result Schedule()
    {
        if (Status != ProductionOrderStatus.Draft)
        {
            return Result.Failure("Only draft production orders can be scheduled.");
        }

        Status = ProductionOrderStatus.Scheduled;

        return Result.Success();
    }

    public Result Start()
    {
        if (Status != ProductionOrderStatus.Scheduled)
        {
            return Result.Failure("Only scheduled production orders can be started.");
        }

        Status = ProductionOrderStatus.InProgress;

        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != ProductionOrderStatus.InProgress)
        {
            return Result.Failure("Only production orders in progress can be completed.");
        }

        Status = ProductionOrderStatus.Completed;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == ProductionOrderStatus.InProgress)
        {
            return Result.Failure("Production order cannot be cancelled while in progress.");
        }

        if (Status == ProductionOrderStatus.Completed)
        {
            return Result.Failure("Completed production orders cannot be cancelled.");
        }

        if (Status == ProductionOrderStatus.Cancelled)
        {
            return Result.Failure("Production order is already cancelled.");
        }

        Status = ProductionOrderStatus.Cancelled;

        return Result.Success();
    }
}