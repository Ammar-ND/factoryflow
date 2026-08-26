namespace FactoryFlow.Domain.ProductionLines;

public readonly record struct ProductionLineId(Guid Value)
{
    public static ProductionLineId New()
    {
        return new ProductionLineId(Guid.NewGuid());
    }
}