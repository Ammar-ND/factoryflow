using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.CreateProductionOrder;

public sealed record CreateProductionOrderCommand(
    ProductId ProductId,
    int Quantity)
    : ICommand<Result<ProductionOrderId>>;