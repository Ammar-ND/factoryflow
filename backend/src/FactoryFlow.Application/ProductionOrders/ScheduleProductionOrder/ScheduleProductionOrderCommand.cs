using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.ScheduleProductionOrder;

public sealed record ScheduleProductionOrderCommand(
    ProductionOrderId ProductionOrderId)
    : ICommand;