using System.Net;
using System.Net.Http.Json;
using FactoryFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryFlow.IntegrationTests.Api.Products;

public class CreateProductApiTests
{
    [Fact]
    public async Task Post_WithValidRequest_ShouldCreateProductAndReturnCreated()
    {
        await using var factory =
            new FactoryFlowWebApplicationFactory();

        await factory.InitializeDatabaseAsync();

        try
        {
            var client = factory.CreateClient();

            var request = new
            {
                Name = "Steel Gear",
                Code = "PRD-001"
            };

            var response = await client.PostAsJsonAsync(
                "/api/products",
                request);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            using var scope =
                factory.Services.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<FactoryFlowDbContext>();

            var product =
                await dbContext.Products.SingleAsync();

            Assert.Equal("Steel Gear", product.Name);
            Assert.Equal("PRD-001", product.Code);
        }
        finally
        {
            await factory.DeleteDatabaseAsync();
        }
    }

    [Fact]
    public async Task Post_WithInvalidRequest_ShouldReturnBadRequest()
    {
        await using var factory =
            new FactoryFlowWebApplicationFactory();

        await factory.InitializeDatabaseAsync();

        try
        {
            var client = factory.CreateClient();

            var request = new
            {
                Name = "",
                Code = ""
            };

            var response = await client.PostAsJsonAsync(
                "/api/products",
                request);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }
        finally
        {
            await factory.DeleteDatabaseAsync();
        }
    }
}