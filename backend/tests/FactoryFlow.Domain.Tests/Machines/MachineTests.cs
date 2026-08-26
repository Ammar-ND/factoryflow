using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.ProductionLines;

namespace FactoryFlow.Domain.Tests.Machines;

public class MachineTests
{
    [Fact]
    public void Start_WhenMachineIsStopped_ShouldSetStatusToRunning()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        // Act
        var result = machine.Start();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MachineStatus.Running, machine.Status);
    }

    [Fact]
    public void Start_WhenMachineIsAlreadyRunning_ShouldReturnFailure()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        machine.Start();

        // Act
        var result = machine.Start();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Machine is already running.", result.Error);
        Assert.Equal(MachineStatus.Running, machine.Status);
    }

    [Fact]
    public void Start_WhenMachineIsUnderMaintenance_ShouldReturnFailure()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        machine.StartMaintenance();

        // Act
        var result = machine.Start();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Machine cannot start while under maintenance.",
            result.Error);
        Assert.Equal(
            MachineStatus.UnderMaintenance,
            machine.Status);
    }

    [Fact]
    public void Stop_WhenMachineIsRunning_ShouldSetStatusToStopped()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        machine.Start();

        // Act
        var result = machine.Stop();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MachineStatus.Stopped, machine.Status);
    }

    [Fact]
    public void Stop_WhenMachineIsAlreadyStopped_ShouldReturnFailure()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        // Act
        var result = machine.Stop();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Machine is already stopped.", result.Error);
        Assert.Equal(MachineStatus.Stopped, machine.Status);
    }

    [Fact]
    public void StartMaintenance_WhenMachineIsStopped_ShouldSetStatusToUnderMaintenance()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        // Act
        var result = machine.StartMaintenance();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MachineStatus.UnderMaintenance, machine.Status);
    }

    [Fact]
    public void StartMaintenance_WhenMachineIsRunning_ShouldReturnFailure()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        machine.Start();

        // Act
        var result = machine.StartMaintenance();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Machine must be stopped before maintenance.",
            result.Error);
        Assert.Equal(MachineStatus.Running, machine.Status);
    }

    [Fact]
    public void CompleteMaintenance_WhenMachineIsUnderMaintenance_ShouldSetStatusToStopped()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        machine.StartMaintenance();

        // Act
        var result = machine.CompleteMaintenance();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MachineStatus.Stopped, machine.Status);
    }

    [Fact]
    public void CompleteMaintenance_WhenMachineIsNotUnderMaintenance_ShouldReturnFailure()
    {
        // Arrange
        var productionLineId = ProductionLineId.New();
        var machine = Machine.Create(productionLineId, "CNC Machine");

        // Act
        var result = machine.CompleteMaintenance();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Machine is not under maintenance.",
            result.Error);
        Assert.Equal(MachineStatus.Stopped, machine.Status);
    }
}