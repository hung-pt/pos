using Catalog.Api.Helpers;
using Catalog.Api.Routes;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddServiceDiscovery(o => o.UseEureka());
builder.Services.AddRedis();

//
builder.Services.AddRequestHandlers();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("EcomShopDatabase"));

builder.Services.AddMemoryCache();
builder.Services.AddOutputCache();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger(c => c.RouteTemplate = "api/{documentName}/swagger.json");
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/api/v1/swagger.json", "Catalog.API v1");
        c.RoutePrefix = "api";
    });
}
app.MapGroup("/product").MapProductApi().WithTags("Product");
app.MapGroup("/productline").MapProductLineApi().WithTags("ProductLine");
app.UseResponseCaching();
app.UseOutputCache();
app.Run();

//namespace Catalog.Api;

//public class Program {
//    public static void Main(string[] args) {
//        CreateHostBuilder(args).Build().Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webBuilder => {
//                webBuilder.UseStartup<Startup>();
//            });
//}
