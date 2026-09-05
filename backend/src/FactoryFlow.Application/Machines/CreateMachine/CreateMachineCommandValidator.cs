using FluentValidation;

namespace FactoryFlow.Application.Machines.CreateMachine;

public sealed class CreateMachineCommandValidator
    : AbstractValidator<CreateMachineCommand>
{
    public CreateMachineCommandValidator()
    {
        RuleFor(command => command.ProductionLineId)
            .NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}