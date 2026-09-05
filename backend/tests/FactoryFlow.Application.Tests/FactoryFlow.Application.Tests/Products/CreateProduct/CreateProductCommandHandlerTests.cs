using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Application.Products.CreateProduct;
using FactoryFlow.Domain.Products;
using NSubstitute;

namespace FactoryFlow.Application.Tests.Products.CreateProduct;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateProductAndReturnSuccess()
    {
        // Arrange
        var productRepository =
            Substitute.For<IProductRepository>();

        var unitOfWork =
            Substitute.For<IUnitOfWork>();

        unitOfWork
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var handler = new CreateProductCommandHandler(
            productRepository,
            unitOfWork);

        var command = new CreateProductCommand(
            "Test Product",
            "PRD-001");

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        await productRepository.Received(1).AddAsync(
            Arg.Is<Product>(product =>
                product.Name == "Test Product" &&
                product.Code == "PRD-001"),
            Arg.Any<CancellationToken>());

        await unitOfWork.Received(1).SaveChangesAsync(
            Arg.Any<CancellationToken>());
    }
}