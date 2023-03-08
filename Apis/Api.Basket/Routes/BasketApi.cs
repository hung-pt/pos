using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Ddd.Application.Orders;

namespace Api.Catalog.Routes;

internal static class BasketApi {
    internal static RouteGroupBuilder MapBasketApi(this RouteGroupBuilder group) {
        static string GetBasketKey(int CustomerNumber) => $"basket_{CustomerNumber}";

        group.MapGet("/{CustomerNumber}",
            async (IDistributedCache cache, int CustomerNumber) => {
                var cached = await cache.GetStringAsync(GetBasketKey(CustomerNumber));
                if (string.IsNullOrEmpty(cached)) return null;
                return JsonSerializer.Deserialize<OrderCreateDto>(cached);
            });

        group.MapPost("/",
            async (IDistributedCache cache, [FromBody] OrderCreateDto basket) => {
                var toCache = JsonSerializer.Serialize(basket);
                await cache.SetStringAsync(GetBasketKey(basket.CustomerNumber), toCache);
                return Results.Ok(basket);
            });

        group.MapPost("/{CustomerNumber}/placeOrder",
            async (IDistributedCache cache, IMediator md, int CustomerNumber) => {
                var cached = await cache.GetStringAsync(GetBasketKey(CustomerNumber));
                if (string.IsNullOrEmpty(cached)) return Results.NotFound("basket not found");

                var basket = JsonSerializer.Deserialize<OrderCreateDto>(cached);
                var newOrder = md.Send(new AddOrderCommand(basket!));
                return Results.Ok(newOrder);
            });

        group.MapPut("/{CustomerNumber}",
            async (IDistributedCache cache, [FromBody] OrderCreateDto basket, int CustomerNumber) => {
                var toCache = JsonSerializer.Serialize(basket);
                await cache.SetStringAsync(GetBasketKey(CustomerNumber), toCache);
                return Results.Ok(basket);
            });

        group.MapDelete("/{CustomerNumber}",
            async (IDistributedCache cache, string CustomerNumber) =>
                await cache.RemoveAsync(CustomerNumber));

        return group;
    }
}
