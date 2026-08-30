using FactoryFlow.Domain.Factories;
using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.ProductionLines;
using FactoryFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FactoryFlow.IntegrationTests.Persistence;

public class MachinePersistenceTests
{
    [Fact]
    public async Task Machine_WithProductionLineAndFactory_ShouldBeSavedAndLoadedFromDatabase()
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

        var productionLine = ProductionLine.Create(
            factory.Id,
            "Production Line 1");

        var machine = Machine.Create(
            productionLine.Id,
            "CNC Machine");

        try
        {
            // Save
            await using (var writeContext =
                new FactoryFlowDbContext(options))
            {
                await writeContext.Database.MigrateAsync();

                writeContext.Factories.Add(factory);
                writeContext.ProductionLines.Add(productionLine);
                writeContext.Machines.Add(machine);

                await writeContext.SaveChangesAsync();
            }

            // Read
            await using (var readContext =
                new FactoryFlowDbContext(options))
            {
                var savedFactory =
                    await readContext.Factories.SingleAsync();

                var savedProductionLine =
                    await readContext.ProductionLines.SingleAsync();

                var savedMachine =
                    await readContext.Machines.SingleAsync();

                // Assert
                Assert.Equal(factory.Id, savedFactory.Id);

                Assert.Equal(
                    factory.Id,
                    savedProductionLine.FactoryId);

                Assert.Equal(
                    productionLine.Id,
                    savedMachine.ProductionLineId);

                Assert.Equal(
                    "CNC Machine",
                    savedMachine.Name);

                Assert.Equal(
                    MachineStatus.Stopped,
                    savedMachine.Status);
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