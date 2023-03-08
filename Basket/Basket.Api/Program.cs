var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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







app.Run();
