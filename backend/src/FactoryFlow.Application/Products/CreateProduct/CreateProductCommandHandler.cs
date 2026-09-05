using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Products.CreateProduct;

public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, Result<ProductId>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductId>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = Product.Create(
            command.Name,
            command.Code);

        await _productRepository.AddAsync(
            product,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}