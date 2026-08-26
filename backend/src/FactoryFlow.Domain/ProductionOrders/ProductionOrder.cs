using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Domain.ProductionOrders;

public sealed class ProductionOrder : AggregateRoot<ProductionOrderId>
{
    public ProductId ProductId { get; private set; }

    public int Quantity { get; private set; }

    private ProductionOrder(
        ProductionOrderId id,
        ProductId productId,
        int quantity)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static ProductionOrder Create(
        ProductId productId,
        int quantity)
    {
        return new ProductionOrder(
            ProductionOrderId.New(),
            productId,
            quantity);
    }
}