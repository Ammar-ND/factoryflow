namespace FactoryFlow.Domain.Factories;

public readonly record struct FactoryId(Guid Value)
{
    public static FactoryId New()
    {
        return new FactoryId(Guid.NewGuid());
    }
}