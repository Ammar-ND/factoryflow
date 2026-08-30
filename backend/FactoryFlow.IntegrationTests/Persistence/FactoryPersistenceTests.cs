using FactoryFlow.Domain.Factories;
using FactoryFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FactoryFlow.IntegrationTests.Persistence;

public class FactoryPersistenceTests
{
    [Fact]
    public async Task Factory_ShouldBeSavedAndLoadedFromDatabase()
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

        var factory = Factory.Create("Test Factory");

        try
        {
            // Save
            await using (var writeContext =
                new FactoryFlowDbContext(options))
            {
                await writeContext.Database.MigrateAsync();

                writeContext.Factories.Add(factory);

                await writeContext.SaveChangesAsync();
            }

            // Read
            await using (var readContext =
                new FactoryFlowDbContext(options))
            {
                var savedFactory =
                    await readContext.Factories.SingleAsync();

                // Assert
                Assert.Equal(factory.Id, savedFactory.Id);
                Assert.Equal("Test Factory", savedFactory.Name);
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