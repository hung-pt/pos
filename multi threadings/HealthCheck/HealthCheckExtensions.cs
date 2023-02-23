using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace multi_threadings.HealthCheck;

public static class HealthCheckExtensions {
    // 
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services) {
        //services.AddHostedService<StartupBackgroundService>();
        //services.AddSingleton<StartupHealthCheck>();
        services.AddHealthChecks()
            //.AddCheck<StartupHealthCheck>(
            //    "Startup"
            //    , tags: new[] { "ready" } // 
            //    )
            .AddCheck<BasicHealthCheck>("Sample");
            //.AddCheck<NonCriticalHealthCheck>("NonCritical");
        return services;
    }

    //
    public static IEndpointRouteBuilder MapCustomHealthChecks(this IEndpointRouteBuilder endpoints) {
        endpoints.MapHealthChecks("/healthz", new HealthCheckOptions { ResponseWriter = CustomizeHealthReport() });
        //// 
        //endpoints.MapHealthChecks("/healthz/ready", new HealthCheckOptions {
        //    Predicate = healthCheck => healthCheck.Tags.Contains("ready") //
        //});
        //endpoints.MapHealthChecks("/healthz/live", new HealthCheckOptions {
        //    Predicate = _ => true //
        //});
        return endpoints;

        static Func<HttpContext, HealthReport, Task> CustomizeHealthReport() {
            return async (context, report) => {
                var result = JsonSerializer.Serialize(new {
                    Status = report.Status.ToString(),
                    Info = report.Entries.Select(e => new {
                        e.Key,
                        e.Value.Description,
                        Status = Enum.GetName(typeof(HealthStatus),
                                                e.Value.Status),
                        Error = e.Value.Exception?.Message
                    }).ToList()
                });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            };
        }
    }
}
