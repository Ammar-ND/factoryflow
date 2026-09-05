using FactoryFlow.Application.Abstractions.Messaging;
using FactoryFlow.Domain.Common;
using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.ProductionLines;

namespace FactoryFlow.Application.Machines.CreateMachine;

public sealed record CreateMachineCommand(
    ProductionLineId ProductionLineId,
    string Name)
    : ICommand<Result<MachineId>>;