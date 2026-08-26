namespace FactoryFlow.Domain.ProductionOrders;

public readonly record struct ProductionOrderId(Guid Value)
{
    public static ProductionOrderId New()
    {
        return new ProductionOrderId(Guid.NewGuid());
    }
}