using Consul;
using Ddd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace multi_threadings;

public static partial class Utilities {
    //
    public static async Task EnsureDbAsync(IServiceProvider sp) {
        //await using var context = sp.CreateScope().ServiceProvider.GetRequiredService<EcomShopContext>();

        //await context.Database.EnsureDeletedAsync();
        //await context.Database.MigrateAsync();
    }

    //
    public static async Task RegisterToConsul(IServiceProvider sp) {
        using var scope = sp.CreateScope();
        var consulClient = scope.ServiceProvider.GetRequiredService<IConsulClient>();

        var serviceName = Assembly.GetExecutingAssembly().GetName().Name;
        string consulServiceID = $"{serviceName}";

        await consulClient.Agent.ServiceRegister(new AgentServiceRegistration {
            Name = serviceName,
            ID = consulServiceID,
            Address = "localhost",
            Port = 7226
        });
    }
}
