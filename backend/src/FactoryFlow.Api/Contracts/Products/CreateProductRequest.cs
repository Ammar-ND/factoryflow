namespace FactoryFlow.Api.Contracts.Products;

public sealed record CreateProductRequest(
    string Name,
    string Code);