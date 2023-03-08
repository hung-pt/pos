using Ddd.Application.Interfaces;
using Ddd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ddd.Infrastructure {
    public static class ServiceCollectionsExtensions {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, string? connectionString) {
            serviceCollection.AddSqlServer<EcomShopContext>(
                connectionString,
                options => options
                    .MigrationsAssembly("Api.Playground")
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
