using FactoryFlow.Application.Machines.CreateMachine;
using FactoryFlow.Domain.ProductionLines;

namespace FactoryFlow.Application.Tests.Machines.CreateMachine;

public class CreateMachineCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_ShouldBeValid()
    {
        var command = new CreateMachineCommand(
            ProductionLineId.New(),
            "CNC Machine");

        var validator = new CreateMachineCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldBeInvalid()
    {
        var command = new CreateMachineCommand(
            ProductionLineId.New(),
            "");

        var validator = new CreateMachineCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
    }
}