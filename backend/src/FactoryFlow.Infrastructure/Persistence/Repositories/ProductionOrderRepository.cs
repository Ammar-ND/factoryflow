using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Infrastructure.Persistence.Repositories;

public sealed class ProductionOrderRepository
    : IProductionOrderRepository
{
    private readonly FactoryFlowDbContext _dbContext;

    public ProductionOrderRepository(
        FactoryFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        ProductionOrder productionOrder,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.ProductionOrders.AddAsync(
            productionOrder,
            cancellationToken);
    }

    public async Task<ProductionOrder?> GetByIdAsync(
    ProductionOrderId productionOrderId,
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductionOrders
            .FindAsync(
                [productionOrderId],
                cancellationToken);
    }
}