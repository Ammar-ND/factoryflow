using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.ProductionOrders;

namespace FactoryFlow.Application.ProductionOrders.CreateProductionOrder;

public sealed class CreateProductionOrderCommandHandler
    : ICommandHandler<
        CreateProductionOrderCommand,
        Result<ProductionOrderId>>
{
    private readonly IProductionOrderRepository _productionOrderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductionOrderCommandHandler(
        IProductionOrderRepository productionOrderRepository,
        IUnitOfWork unitOfWork)
    {
        _productionOrderRepository = productionOrderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductionOrderId>> Handle(
        CreateProductionOrderCommand command,
        CancellationToken cancellationToken)
    {
        var createResult = ProductionOrder.Create(
            command.ProductId,
            command.Quantity);

        if (createResult.IsFailure)
        {
            return Result<ProductionOrderId>.Failure(
                createResult.Error!);
        }

        var productionOrder = createResult.Value!;

        await _productionOrderRepository.AddAsync(
            productionOrder,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<ProductionOrderId>.Success(
            productionOrder.Id);
    }
}