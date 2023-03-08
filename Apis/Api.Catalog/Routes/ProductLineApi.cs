using Api.Catalog.Dtos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Ddd.Application.Products;
using System.ComponentModel;
using System.Text.Json;
using Ddd.Application.Default;
using Ddd.Domain;

namespace Api.Catalog.Routes;

public static class ProductLineApi {
    internal static RouteGroupBuilder MapProductLineApi(this RouteGroupBuilder group) {
        group.MapGet("/", async (
                IMediator md,
                [DefaultValue(1)] int? page,
                [DefaultValue(10)] int? pageSize
                ) => {
                    var res = await md.Send(new GetQuery(
                        typeof(ProductLine),
                        null,
                        page ?? 1,
                        pageSize ?? 10
                        ));
                    return res;
                });

        group.MapGet("/{productLineCode}", async (
                IMediator md,
                IDistributedCache cache,
                string productLineCode
                ) => {
                    var cacheKey = $"productLine_{productLineCode}";
                    var cached = await cache.GetStringAsync(cacheKey);

                    ProductDto? dto;
                    if (cached == null) {
                        dto = (ProductDto?)await md.Send(new GetProductByIdQuery(productCode, typeof(ProductDto)));
                        cached = JsonSerializer.Serialize(dto);

                        await cache.SetStringAsync(cacheKey, cached,
                            new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                    }
                    else {
                        dto = JsonSerializer.Deserialize<ProductDto>(cached);
                    }

                    return dto;
                });

        return group;
    }



}
