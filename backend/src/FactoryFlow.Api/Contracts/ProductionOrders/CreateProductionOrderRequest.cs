namespace FactoryFlow.Api.Contracts.ProductionOrders;

public sealed record CreateProductionOrderRequest(
    Guid ProductId,
    int Quantity);