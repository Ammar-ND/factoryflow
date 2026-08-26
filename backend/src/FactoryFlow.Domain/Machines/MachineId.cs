namespace FactoryFlow.Domain.Machines;

public readonly record struct MachineId(Guid Value)
{
    public static MachineId New()
    {
        return new MachineId(Guid.NewGuid());
    }
}