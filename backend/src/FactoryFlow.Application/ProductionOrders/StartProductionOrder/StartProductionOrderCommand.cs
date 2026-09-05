using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.StartProductionOrder;

public sealed record StartProductionOrderCommand(
    ProductionOrderId ProductionOrderId)
    : ICommand;