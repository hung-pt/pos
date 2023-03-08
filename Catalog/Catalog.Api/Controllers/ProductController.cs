using Catalog.Api.Dtos;
using Catalog.Application;
using Catalog.Application.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel;
using System.Text.Json;

namespace multi_threadings.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase {
    private readonly ILogger<ProductController> _logger;

    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public ProductController(
        ILogger<ProductController> logger,
        IMediator mediator,
        IDistributedCache cache
        ) {
        _logger = logger;
        _mediator = mediator;
        _cache = cache;
        _cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
    }

    [HttpGet()]
    public async Task<IActionResult> Get([DefaultValue(1)] int? page, [DefaultValue(10)] int? pageSize) {
        var query = new GetProductsQuery(page ?? 1, pageSize ?? 10);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{productCode}")]
    public async Task<IActionResult> GetById(string productCode) {
        var cacheKey = $"product_{productCode}";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        ProductDto? dto;
        if (cachedData == null) {
            var query = new GetProductByIdQuery(productCode);
            var product = await _mediator.Send(query);
            if (product is null)
                return NotFound();
            dto = Mapper.Map<ProductDto>(product);

            cachedData = JsonSerializer.Serialize(dto);

            await _cache.SetStringAsync(cacheKey, cachedData, _cacheOptions);
        }
        else {
            dto = JsonSerializer.Deserialize<ProductDto>(cachedData);
        }

        return Ok(dto);
    }

    [HttpPost()]
    public async Task<IActionResult> Post(RegisterProductDto newProduct) {
        var command = new AddProductCommand(newProduct);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{productCode}")]
    public async Task<IActionResult> Delete(string productCode) {
        var command = new RemoveProductCommand(productCode);
        await _mediator.Send(command);
        return NoContent();
    }
}
