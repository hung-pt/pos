using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using Catalog.Application.ProductLines;
using Catalog.Domain;

namespace Catalog.Api.Routes;

public static class ProductLineApi {
    internal static RouteGroupBuilder MapProductLineApi(this RouteGroupBuilder group) {

        group.MapGet("/", [OutputCache(Duration = 5)] async (
                IMediator md,
                [DefaultValue(1)] int? page,
                [DefaultValue(10)] int? pageSize
                ) => await md.Send(new GetProductLineQuery(page ?? 1, pageSize ?? 10)));

        group.MapGet("/{productLineCode}", async (
                IMediator md,
                IMemoryCache cache,
                string productLineCode
                ) => {
                    if (cache.TryGetValue($"productLine_{productLineCode}", out ProductLine cached)) {
                        return Results.Ok(cached);
                    }
                    else {
                        var response = await md.Send(new GetProductLineByIdQuery(productLineCode));
                        if (response is null) return Results.NotFound();

                        cache.Set($"productLine_{productLineCode}", response, DateTimeOffset.Now.AddSeconds(5));
                        return Results.Ok(response);
                    }
                });

        return group;
    }
}
