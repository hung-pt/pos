using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http.Json;

namespace Catalog.Api.FunctionalTests;

//class TestWebAppFactory : WebApplicationFactory<Program> {
//    protected override IHost CreateHost(IHostBuilder builder) {
//        // shared extra set up goes here
//        return base.CreateHost(builder);
//    }
//}

[TestClass]
public class ProductLineApiTests {
    private HttpClient _httpClient;

    [TestInitialize()]
    public void TestInitialize() {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.ConfigureServices(services => {
                    var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<CatalogDbContext>));
                    services.Remove(descriptor);
                    services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
                });
            });
        _httpClient = factory.CreateClient();

        //using var scope = factory.Services.CreateScope();
        //var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        //dbContext.Database.Migrate();
    }


    [TestMethod]
    public async Task GetProductLine_ReturnsOkResult() {
        // Arrange


        // Act
        var response = await _httpClient.GetStringAsync("productline");

        // Assert
        Assert.IsNotNull(response);
        //Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
}
