using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Products.GetProductById;

public sealed record ProductResponse(
    ProductId Id,
    string Name,
    string Code);