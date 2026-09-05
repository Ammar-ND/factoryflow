using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.Machines.CreateMachine;
using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.ProductionLines;
using NSubstitute;

namespace FactoryFlow.Application.Tests.Machines.CreateMachine;

public class CreateMachineCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateMachineAndReturnSuccess()
    {
        // Arrange
        var machineRepository =
            Substitute.For<IMachineRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        unitOfWork
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var handler = new CreateMachineCommandHandler(
            machineRepository,
            unitOfWork);

        var productionLineId = ProductionLineId.New();

        var command = new CreateMachineCommand(
            productionLineId,
            "CNC Machine");

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        await machineRepository.Received(1).AddAsync(
            Arg.Is<Machine>(machine =>
                machine.Name == "CNC Machine" &&
                machine.ProductionLineId == productionLineId &&
                machine.Status == MachineStatus.Stopped),
            Arg.Any<CancellationToken>());

        await unitOfWork.Received(1).SaveChangesAsync(
            Arg.Any<CancellationToken>());
    }
}