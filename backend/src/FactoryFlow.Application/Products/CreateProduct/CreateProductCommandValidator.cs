using FluentValidation;

namespace FactoryFlow.Application.Products.CreateProduct;

public sealed class CreateProductCommandValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.Code)
            .NotEmpty()
            .MaximumLength(100);
    }
}