using Catalog.Application.ProductLines;
using Catalog.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductLineController : ControllerBase {
    private readonly ILogger<ProductLineController> _logger;
    private readonly IMediator _mediator;
    private readonly IMemoryCache _cache;

    public ProductLineController(
        ILogger<ProductLineController> logger,
        IMediator mediator,
        IMemoryCache cache
        ) {
        _logger = logger;
        _mediator = mediator;
        _cache = cache;
    }

    [HttpGet()]
    public async Task<IResult> Get([DefaultValue(1)] int? page, [DefaultValue(10)] int? pageSize) {
        var query = new GetProductLineQuery(page ?? 1, pageSize ?? 10);
        return Results.Ok(await _mediator.Send(query));
    }

    [HttpGet("{productLineCode}")]
    public async Task<IResult> BetById(string productLineCode) {
        if (_cache.TryGetValue($"productLine_{productLineCode}", out ProductLine cached)) {
            return Results.Ok(cached);
        }
        else {
            var response = await _mediator.Send(new GetProductLineByIdQuery(productLineCode));
            if (response is null) return Results.NotFound();

            _cache.Set($"productLine_{productLineCode}", response, DateTimeOffset.Now.AddSeconds(5));
            return Results.Ok(response);
        }
    }
}
