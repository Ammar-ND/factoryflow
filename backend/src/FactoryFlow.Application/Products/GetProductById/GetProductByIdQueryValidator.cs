using FluentValidation;

namespace FactoryFlow.Application.Products.GetProductById;

public sealed class GetProductByIdQueryValidator
    : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty();
    }
}