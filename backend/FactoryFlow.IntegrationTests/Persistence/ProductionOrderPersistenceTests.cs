using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using FactoryFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FactoryFlow.IntegrationTests.Persistence;

public class ProductionOrderPersistenceTests
{
    [Fact]
    public async Task ProductionOrder_WithProduct_ShouldBeSavedAndLoadedFromDatabase()
    {
        // Arrange
        var databaseName =
            $"FactoryFlowIntegrationTests_{Guid.NewGuid():N}";

        var connectionString =
            $"Server=(localdb)\\MSSQLLocalDB;" +
            $"Database={databaseName};" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True";

        var options =
            new DbContextOptionsBuilder<FactoryFlowDbContext>()
                .UseSqlServer(connectionString)
                .Options;

        var product = Product.Create(
            "Test Product",
            "PRD-001");

        var createResult =
            ProductionOrder.Create(product.Id, 100);

        var productionOrder = createResult.Value!;

        try
        {
            // Save
            await using (var writeContext =
                new FactoryFlowDbContext(options))
            {
                await writeContext.Database.MigrateAsync();

                writeContext.Products.Add(product);
                writeContext.ProductionOrders.Add(productionOrder);

                await writeContext.SaveChangesAsync();
            }

            // Read
            await using (var readContext =
                new FactoryFlowDbContext(options))
            {
                var savedProduct =
                    await readContext.Products.SingleAsync();

                var savedProductionOrder =
                    await readContext.ProductionOrders.SingleAsync();

                // Assert
                Assert.Equal(product.Id, savedProduct.Id);

                Assert.Equal(
                    product.Id,
                    savedProductionOrder.ProductId);

                Assert.Equal(
                    100,
                    savedProductionOrder.Quantity);

                Assert.Equal(
                    ProductionOrderStatus.Draft,
                    savedProductionOrder.Status);
            }
        }
        finally
        {
            // Cleanup
            await using var cleanupContext =
                new FactoryFlowDbContext(options);

            await cleanupContext.Database.EnsureDeletedAsync();
        }
    }
}