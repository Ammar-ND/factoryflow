using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Common;

namespace FactoryFlow.Application.Products.GetProductById;

public sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, Result<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductResponse>> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            query.ProductId,
            cancellationToken);

        if (product is null)
        {
            return Result<ProductResponse>.Failure(
                "Product was not found.");
        }

        var response = new ProductResponse(
            product.Id,
            product.Name,
            product.Code);

        return Result<ProductResponse>.Success(response);
    }
}