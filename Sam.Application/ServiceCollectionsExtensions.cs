using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ddd.Application;

public static class ServiceCollectionsExtensions {
    public static IServiceCollection AddCqrs(this IServiceCollection services) {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
        return services;
    }

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
