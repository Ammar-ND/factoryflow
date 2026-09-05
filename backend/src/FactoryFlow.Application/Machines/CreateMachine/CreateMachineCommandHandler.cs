using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Machines;

namespace FactoryFlow.Application.Machines.CreateMachine;

public sealed class CreateMachineCommandHandler
    : ICommandHandler<CreateMachineCommand, Result<MachineId>>
{
    private readonly IMachineRepository _machineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMachineCommandHandler(
        IMachineRepository machineRepository,
        IUnitOfWork unitOfWork)
    {
        _machineRepository = machineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MachineId>> Handle(
        CreateMachineCommand command,
        CancellationToken cancellationToken)
    {
        var machine = Machine.Create(
            command.ProductionLineId,
            command.Name);

        await _machineRepository.AddAsync(
            machine,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<MachineId>.Success(machine.Id);
    }
}