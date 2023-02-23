using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace multi_threadings.HealthCheck {
    public class NonCriticalHealthCheck : IHealthCheck {
        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default) {
            var isHealthy = true;

            // ...

            //if (isHealthy)
            //    return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));


            return Task.FromResult(HealthCheckResult.Degraded("An unhealthy result."));
        }

    }
}
