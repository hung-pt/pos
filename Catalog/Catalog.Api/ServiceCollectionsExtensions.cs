using Catalog.Infrastructure.Data;
using Catalog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Catalog.Application.Products;

namespace Catalog.Infrastructure {
    public static class ServiceCollectionsExtensions {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString) {
            // 
            //services.AddSqlServer<CatalogDbContext>(
            //    connectionString,
            //    options => options
            //        .MigrationsAssembly("Catalog.Api")
            //);

            // 
            services.AddDbContext<ICatalogDbContext, CatalogDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            return services;
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection services) {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));
            return services;
        }

        // code raw
        //public static IServiceCollection AddApplication(this IServiceCollection services) {
        //    AddQueries(services);
        //    AddCommands(services);
        //    return services;
        //}

        //private static void AddQueries(IServiceCollection serviceCollection) {
        //    serviceCollection.AddScoped<IQueryHandler<GetOfficeByIdQuery, Office?>, GetOfficeByIdQueryHandler>();
        //    serviceCollection.AddScoped<IQueryHandler<GetOfficesQuery, IList<Office>>, GetOfficesQueryHandler>();
        //}

        //private static void AddCommands(IServiceCollection serviceCollection) {
        //    serviceCollection.AddScoped<ICommandHandler<AddOfficeCommand>, AddOfficeCommandHandler>();
        //    serviceCollection.AddScoped<ICommandHandler<RemoveOfficeCommand>, RemoveOfficeCommandHandler>();
        //}

    }
}
