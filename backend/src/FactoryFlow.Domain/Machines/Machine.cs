using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.ProductionLines;

namespace FactoryFlow.Domain.Machines;

public sealed class Machine : AggregateRoot<MachineId>
{
    public ProductionLineId ProductionLineId { get; private set; }

    public string Name { get; private set; }

    private Machine(
        MachineId id,
        ProductionLineId productionLineId,
        string name)
        : base(id)
    {
        ProductionLineId = productionLineId;
        Name = name;
    }

    public static Machine Create(
        ProductionLineId productionLineId,
        string name)
    {
        return new Machine(
            MachineId.New(),
            productionLineId,
            name);
    }
}