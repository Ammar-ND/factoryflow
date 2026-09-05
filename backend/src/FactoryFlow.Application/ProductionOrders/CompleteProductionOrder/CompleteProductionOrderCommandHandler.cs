using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Common;

namespace FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;

public sealed class CompleteProductionOrderCommandHandler
    : ICommandHandler<CompleteProductionOrderCommand>
{
    private readonly IProductionOrderRepository _productionOrderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteProductionOrderCommandHandler(
        IProductionOrderRepository productionOrderRepository,
        IUnitOfWork unitOfWork)
    {
        _productionOrderRepository = productionOrderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        CompleteProductionOrderCommand command,
        CancellationToken cancellationToken)
    {
        var productionOrder =
            await _productionOrderRepository.GetByIdAsync(
                command.ProductionOrderId,
                cancellationToken);

        if (productionOrder is null)
        {
            return Result.Failure(
                "Production order was not found.");
        }

        var result = productionOrder.Complete();

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}