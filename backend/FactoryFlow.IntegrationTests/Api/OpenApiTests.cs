using System.Net;

namespace FactoryFlow.IntegrationTests.Api;

public class OpenApiTests
{
    [Fact]
    public async Task OpenApiDocument_ShouldBeAvailable()
    {
        await using var factory =
            new FactoryFlowWebApplicationFactory();

        var client = factory.CreateClient();

        var response = await client.GetAsync(
            "/openapi/v1.json");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        var content =
            await response.Content.ReadAsStringAsync();

        Assert.Contains(
            "/api/products",
            content);

        Assert.Contains(
            "/api/production-orders",
            content);
    }
}