using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;

public sealed record CompleteProductionOrderCommand(
    ProductionOrderId ProductionOrderId)
    : ICommand;