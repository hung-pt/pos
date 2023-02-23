using Consul;
using MediatR;
using Newtonsoft.Json;
using RestSharp;
using Sam.Application.Default;
using Sam.Domain;
using System.ComponentModel;

namespace multi_threadings.Routes;


public static class ConsulApi {
    public static RouteGroupBuilder MapConsulApi(this RouteGroupBuilder group) {
        //group.MapGet("/service2d", async (IMediator md) => {
        //    var res = await md.Send(new GetEntitiesQuery(typeof(Product), typeof(Product)));
        //    return res;
        //});
        group.MapGet("/service2d", async (IMediator md) => {
            var res = await md.Send(new GetByIdQuery(
                typeof(Product),
                "S10_1678",
                typeof(Product)
            ));
            return res;
        });

        group.MapGet("/service", async (IConsulClient consulClient, [DefaultValue("multi threadings")] string serviceName) =>
            await GetAgents(consulClient, serviceName));

        group.MapGet("/service/selfcall-a-dummy", async (IConsulClient consulClient) => {
            var agents = await GetAgents(consulClient, "multi threadings");
            var agent = agents?[0];
            if (agents == null) throw new Exception($"Consul agent: was not found.");

            var uri = ResolveUri(agents[0], $"/d/Joe");
            var response = await new RestClient().GetAsync(new RestRequest(uri));
            return response.Content;
        });

        return group;
    }



    static async Task<List<AgentService>?> GetAgents(IConsulClient consulclient, string serviceName) =>
        (await consulclient.Agent.Services()).Response?
            .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Value)
            .ToList();

    static string ResolveUri(AgentService service, string requestUri) =>
        new UriBuilder() {
            Scheme = "https",
            Host = service.Address,
            Port = service.Port,
            Path = requestUri
        }.Uri.AbsoluteUri;

}
