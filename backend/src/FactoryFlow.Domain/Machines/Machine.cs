using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.ProductionLines;

namespace FactoryFlow.Domain.Machines;

public sealed class Machine : AggregateRoot<MachineId>
{
    public ProductionLineId ProductionLineId { get; private set; }

    public string Name { get; private set; }

    public MachineStatus Status { get; private set; }

    private Machine(
        MachineId id,
        ProductionLineId productionLineId,
        string name)
        : base(id)
    {
        ProductionLineId = productionLineId;
        Name = name;
        Status = MachineStatus.Stopped;
    }

    public static Machine Create(
        ProductionLineId productionLineId,
        string name)
    {
        return new Machine(
            MachineId.New(),
            productionLineId,
            name);
    }

    public Result Start()
    {
        if (Status == MachineStatus.UnderMaintenance)
        {
            return Result.Failure("Machine cannot start while under maintenance.");
        }

        if (Status == MachineStatus.Running)
        {
            return Result.Failure("Machine is already running.");
        }

        Status = MachineStatus.Running;

        return Result.Success();
    }

    public Result Stop()
    {
        if (Status == MachineStatus.UnderMaintenance)
        {
            return Result.Failure("Machine cannot be stopped while under maintenance.");
        }

        if (Status == MachineStatus.Stopped)
        {
            return Result.Failure("Machine is already stopped.");
        }

        Status = MachineStatus.Stopped;

        return Result.Success();
    }

    public Result StartMaintenance()
    {
        if (Status == MachineStatus.Running)
        {
            return Result.Failure("Machine must be stopped before maintenance.");
        }

        if (Status == MachineStatus.UnderMaintenance)
        {
            return Result.Failure("Machine is already under maintenance.");
        }

        Status = MachineStatus.UnderMaintenance;

        return Result.Success();
    }

    public Result CompleteMaintenance()
    {
        if (Status != MachineStatus.UnderMaintenance)
        {
            return Result.Failure("Machine is not under maintenance.");
        }

        Status = MachineStatus.Stopped;

        return Result.Success();
    }
}