using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace multi_threadings.HealthCheck {
    public class BasicHealthCheck : IHealthCheck {
        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default) {
            var isHealthy = true;

            // ...

            if (isHealthy) {
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "An unhealthy result."));
        }

    }







    public class Animal {
        public virtual void MakeSound() {
            Console.WriteLine("The animal makes a sound.");
        }
    }

    public class Dog : Animal {
        public override void MakeSound() {
            Console.WriteLine("The dog barks.");
        }
    }

    public class Cat : Animal {
        public override void MakeSound() {
            Console.WriteLine("The cat meows.");
        }
    }
}
