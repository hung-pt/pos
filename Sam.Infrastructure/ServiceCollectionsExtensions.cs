using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sam.Application.Interfaces;
using Sam.Infrastructure.Data;

namespace Sam.Infrastructure {
    public static class ServiceCollectionsExtensions {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, string? connectionString) {
            serviceCollection.AddSqlServer<EcomShopContext>(
                connectionString,
                options => options
                    .MigrationsAssembly("Sam.Infrastructure")
            );
            serviceCollection.AddDbContext<EcomShopContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, o => o.EnableRetryOnFailure(3))
                    .EnableSensitiveDataLogging()
            );
            serviceCollection.AddScoped<IApplicationDbContext, EcomShopContext>();

            return serviceCollection;
        }
    }
}
