using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.CancelProductionOrder;

public sealed record CancelProductionOrderCommand(
    ProductionOrderId ProductionOrderId)
    : ICommand;