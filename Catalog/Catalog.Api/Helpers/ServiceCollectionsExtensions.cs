using StackExchange.Redis;

namespace Catalog.Api.Helpers;

public static class ServiceCollectionsExtensions {
    public static IServiceCollection AddRedis(this IServiceCollection services) {
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = "localhost";
            options.InstanceName = "SampleInstance";
        });
        return services;
    }

}
