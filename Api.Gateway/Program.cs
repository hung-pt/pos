using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot().AddConsul().AddCacheManager(x => x.WithDictionaryHandle());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.UseAuthorization();

app.MapControllers();

app.Run();




//static void Main(string[] args) {
//    new WebHostBuilder()
//    .UseKestrel()
//    .UseContentRoot(Directory.GetCurrentDirectory())
//    .ConfigureAppConfiguration((hostingContext, config) => {
//        config
//            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//            .AddJsonFile("appsettings.json", true, true)
//            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//            .AddJsonFile("ocelot.json")
//            .AddEnvironmentVariables();
//    })
//    .ConfigureServices(s => {
//        s.AddOcelot();
//    })
//    .ConfigureLogging((hostingContext, logging) => {
//        //add your logging
//    })
//    .UseIISIntegration()
//    .Configure(app => {
//        app.UseOcelot().Wait();
//    })
//    .Build()
//    .Run();
//}
