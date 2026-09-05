using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.Abstractions.Persistence;

public interface IProductionOrderRepository
{
    Task AddAsync(
        ProductionOrder productionOrder,
        CancellationToken cancellationToken = default);

    Task<ProductionOrder?> GetByIdAsync(
        ProductionOrderId productionOrderId,
        CancellationToken cancellationToken = default);
}