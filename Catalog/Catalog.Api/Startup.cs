using Catalog.Api.Helpers;
using Catalog.Api.Routes;
using Catalog.Application;
using Catalog.Infrastructure;

namespace Catalog.Api;

public class Startup {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config) {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        //services.AddServiceDiscovery(o => o.UseEureka());
        // config serilog
        services.AddRedis();

        //
        services.AddRequestHandlers();
        services.AddPersistence(_config.GetConnectionString("EcomShopDatabase"));
        services.AddMemoryCache();
        services.AddOutputCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {
        if (env.IsDevelopment()) {
            app.UseSwagger(c => c.RouteTemplate = "api/{documentName}/swagger.json");
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/api/v1/swagger.json", "Catalog.API v1");
                c.RoutePrefix = "api";
            });
            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapGroup("/product").MapProductApi().WithTags("Product");
                routeBuilder.MapGroup("/productline").MapProductLineApi().WithTags("ProductLine");
            });
        }
        
        app.UseResponseCaching();
        app.UseOutputCache();
    }
}
