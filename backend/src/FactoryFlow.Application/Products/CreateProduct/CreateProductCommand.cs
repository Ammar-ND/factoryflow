using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Code)
    : ICommand<Result<ProductId>>;