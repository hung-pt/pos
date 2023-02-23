using Api.Catalog;
using Api.Catalog.Routes;
using Sam.Application;
using Sam.Infrastructure;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

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
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => "ok");
    app.MapGet("/s", () => TypedResults.Redirect("/swagger/index.html"));
    //app.Urls.Add("http://localhost:5000");
}

//app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.MapGroup("/product").MapProductApi().WithTags("Product");

app.Run();
