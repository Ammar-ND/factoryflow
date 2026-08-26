using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Factories;

namespace FactoryFlow.Domain.ProductionLines;

public sealed class ProductionLine : AggregateRoot<ProductionLineId>
{
    public FactoryId FactoryId { get; private set; }

    public string Name { get; private set; }

    private ProductionLine(
        ProductionLineId id,
        FactoryId factoryId,
        string name)
        : base(id)
    {
        FactoryId = factoryId;
        Name = name;
    }

    public static ProductionLine Create(
        FactoryId factoryId,
        string name)
    {
        return new ProductionLine(
            ProductionLineId.New(),
            factoryId,
            name);
    }
}