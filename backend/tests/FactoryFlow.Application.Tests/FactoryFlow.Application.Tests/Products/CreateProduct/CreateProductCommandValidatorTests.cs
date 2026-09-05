using FactoryFlow.Application.Products.CreateProduct;

namespace FactoryFlow.Application.Tests.Products.CreateProduct;

public class CreateProductCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_ShouldBeValid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();

        var command = new CreateProductCommand(
            "Test Product",
            "PRD-001");

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldBeInvalid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();

        var command = new CreateProductCommand(
            "",
            "PRD-001");

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyCode_ShouldBeInvalid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();

        var command = new CreateProductCommand(
            "Test Product",
            "");

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }
}