using Api.Catalog.Dtos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Ddd.Application.Products;
using System.ComponentModel;
using System.Text.Json;

namespace Api.Catalog.Routes;

public static class ProductApi {
    internal static RouteGroupBuilder MapProductApi(this RouteGroupBuilder group) {
        group.MapGet("/", async (
                IMediator md,
                [DefaultValue(1)] int? pageIndex,
                [DefaultValue(10)] int? pageSize
                ) => {
                    await md.Send(new GetProductsQuery(pageIndex ?? 1, pageSize ?? 10, typeof(ProductDto)));
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

        group.MapPost("/", async (
            IMediator md,
            RegisterProductDto newProduct
            ) => {
                await md.Send(new AddProductCommand(newProduct, typeof(ProductDto)));
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
