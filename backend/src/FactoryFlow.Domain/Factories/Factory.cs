using FactoryFlow.Domain.Common;

namespace FactoryFlow.Domain.Factories;

public sealed class Factory : AggregateRoot<FactoryId>
{
    public string Name { get; private set; }

    private Factory(FactoryId id, string name)
        : base(id)
    {
        Name = name;
    }

    public static Factory Create(string name)
    {
        return new Factory(
            FactoryId.New(),
            name);
    }
}