using FactoryFlow.Domain.Products;

namespace FactoryFlow.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);
}