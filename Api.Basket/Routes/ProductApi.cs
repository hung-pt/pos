using Api.Catalog.Dtos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Ddd.Application.Offices;
using Ddd.Application.Products;
using System.ComponentModel;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Api.Basket.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Catalog.Routes;


internal static class BasketApi {
    internal static RouteGroupBuilder MapBasketApi(this RouteGroupBuilder group) {
        static string GetBasketKey(string customerName) => $"basket_{customerName}";

        group.MapGet("/{customerName}",
            async (IDistributedCache cache, string customerName) => {
                var cached = await cache.GetStringAsync(GetBasketKey(customerName));
                if (string.IsNullOrEmpty(cached)) return null;
                return JsonSerializer.Deserialize<ShoppingCart>(cached);
            });

        group.MapPost("/",
            async (IDistributedCache cache, [FromBody] ShoppingCart basket) => {
                var toCache = JsonSerializer.Serialize(basket);
                await cache.SetStringAsync(GetBasketKey(basket.CustomerName), toCache);
                return Results.Ok(basket);
            });

        group.MapPost("/{customerName}/placeOrder",
            async (IDistributedCache cache, IMediator md, string customerName) => {
                var cached = await cache.GetStringAsync(GetBasketKey(customerName));

                var basket = md.Send(new );
                return Results.Ok(basket);
            });

        group.MapPut("/{customerName}",
            async (IDistributedCache cache, [FromBody] ShoppingCart basket, string customerName) => {
                var toCache = JsonSerializer.Serialize(basket);
                await cache.SetStringAsync(GetBasketKey(customerName), toCache);
                return Results.Ok(basket);
            });

        group.MapDelete("/{customerName}",
            async (IDistributedCache cache, string customerName) =>
                await cache.RemoveAsync(customerName));

        return group;
    }
}
