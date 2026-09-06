using FactoryFlow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FactoryFlow.IntegrationTests.Api;

public sealed class FactoryFlowWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private readonly string _databaseName =
        $"FactoryFlowApiTests_{Guid.NewGuid():N}";

    private string ConnectionString =>
        $"Server=(localdb)\\MSSQLLocalDB;" +
        $"Database={_databaseName};" +
        "Trusted_Connection=True;" +
        "TrustServerCertificate=True";

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<
                DbContextOptions<FactoryFlowDbContext>>();

            services.RemoveAll<FactoryFlowDbContext>();

            services.AddDbContext<FactoryFlowDbContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        });
    }

    public async Task InitializeDatabaseAsync()
    {
        using var scope = Services.CreateScope();

        var dbContext =
            scope.ServiceProvider
                .GetRequiredService<FactoryFlowDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    public async Task DeleteDatabaseAsync()
    {
        using var scope = Services.CreateScope();

        var dbContext =
            scope.ServiceProvider
                .GetRequiredService<FactoryFlowDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
    }
}