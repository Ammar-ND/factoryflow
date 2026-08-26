using FactoryFlow.Domain.Common;

namespace FactoryFlow.Domain.Products;

public sealed class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }

    public string Code { get; private set; }

    private Product(
        ProductId id,
        string name,
        string code)
        : base(id)
    {
        Name = name;
        Code = code;
    }

    public static Product Create(
        string name,
        string code)
    {
        return new Product(
            ProductId.New(),
            name,
            code);
    }
}