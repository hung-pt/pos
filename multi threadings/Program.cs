using Consul;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using multi_threadings;
using multi_threadings.HealthCheck;
using multi_threadings.Routes;
using Sam.Application;
using Sam.Infrastructure;
using Sam.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SchemaFilter<OfficeSchemaFilter>());
builder.Services.AddCustomHealthChecks();
builder.Services.AddCqrs();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("EcomShopDatabase"));

//
builder.Services.AddSingleton<IConsulClient, ConsulClient>();

var app = builder.Build();
//await RegisterThisConsulAgent(app.Services);




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    await Utilities.EnsureDbAsync(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Lifetime.ApplicationStopping.Register(() => { });
    app.MapGet("/", () => "ok");
    app.MapGet("/s", () => TypedResults.Redirect("/swagger/index.html"));
    app.MapGet("/d", () => "hello there");
    app.MapGet("/d/{id}", (string id) => $"hello there, {id}");
    //app.Urls.Add("https://localhost:3000");
}

// #### #### ####    
app.UseHttpsRedirection();
app.MapGroup("/consul").MapConsulApi().WithTags("Consul");
app.MapGroup("/office").MapOfficesApi().WithTags("Office");
app.MapCustomHealthChecks();

app.UseAuthorization();
app.MapControllers();

app.Run();





