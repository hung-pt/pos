using Api.Catalog.Helpers;
using Api.Catalog.Routes;
using Ddd.Application;
using Ddd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddServiceDiscovery(o => o.UseEureka());
builder.Services.AddRedis();
builder.Services.AddCqrs();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("EcomShopDatabase"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger(c => c.RouteTemplate = "api/{documentName}/swagger.json");
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/api/v1/swagger.json", "Catalog.API v1");
        c.RoutePrefix = "api";
    });
    app.MapGet("/", () => "service is running");
}

app.UseAuthorization();
app.MapGroup("/product").MapProductApi().WithTags("Product");

app.Run();
