using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Products.GetProductById;

public sealed record GetProductByIdQuery(
    ProductId ProductId)
    : IQuery<Result<ProductResponse>>;