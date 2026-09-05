using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly FactoryFlowDbContext _dbContext;

    public ProductRepository(FactoryFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(
            product,
            cancellationToken);
    }
}