using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel;
using System.Text.Json;
using Catalog.Api.Dtos;
using Catalog.Application.Products;
using Catalog.Application;

namespace Catalog.Api.Routes;

public static class ProductApi {
    internal static RouteGroupBuilder MapProductApi(this RouteGroupBuilder group) {
        group.MapGet("/", async (
                IMediator md,
                [DefaultValue(1)] int? page,
                [DefaultValue(10)] int? pageSize
                ) => {
                    await md.Send(new GetProductsQuery(page ?? 1, pageSize ?? 10));
                });

        group.MapGet("/{productCode}", async (
                IMediator md,
                IDistributedCache cache,
                string productCode
                ) => {
                    // ** query-level cache & result-set cache
                    // query-level cache: vd như EFCore Plus
                    // SELECT * FROM products
                    //     => cache key = "SELECT * FROM products" (nguyên câu query)
                    // 
                    // query-level cache: vd như Redis
                    // SELECT * FROM products
                    //     => cache key = "product_123" (tự ra quy tắc)

                    var cacheKey = $"product_{productCode}";
                    var cached = await cache.GetStringAsync(cacheKey);

                    ProductDto? dto;
                    if (cached == null) {
                        var queryResult = await md.Send(new GetProductByIdQuery(productCode));
                        if (queryResult is null)
                            return Results.NotFound();
                        dto = Mapper.Map<ProductDto>(queryResult);

                        cached = JsonSerializer.Serialize(dto);

                        await cache.SetStringAsync(cacheKey, cached,
                            new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                    }
                    else {
                        dto = JsonSerializer.Deserialize<ProductDto>(cached);
                    }

                    return Results.Ok(dto);
                });

        group.MapPost("/", async (
            IMediator md,
            RegisterProductDto newProduct
            ) => {
                await md.Send(new AddProductCommand(newProduct));
            });
        //group.MapPut("/{productCode}",
        //    async (IMediator md, string productCode) => 
        //        (ProductDto?)await md.Send(new UpdateProductCommand(newProduct, typeof(ProductDto))));
        group.MapDelete("/{productCode}", async (
            IMediator md,
            string productCode
            ) => {
                await md.Send(new RemoveProductCommand(productCode));
            });

        return group;
    }



}
