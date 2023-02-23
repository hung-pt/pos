using Api.Catalog.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Domain;
using Sam.Infrastructure.Data;
using Sam.Infrastructure.Others;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace Api.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase {
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(
        ILogger<ProductController> logger,
        IMediator mediator
        ) {
        _logger = logger;
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IEnumerable<object>> GetAsync(int? pageIndex, int? pageSize) {
        return await _mediator.Send(new GetQuery(
            typeof(Product),
            typeof(ProductDto),
            pageIndex ?? 1,
            pageSize ?? 10
        ));
    }

    //[HttpGet("{productCode}")]
    //public async Task<ActionResult<Product>> Get(string productCode) {
    //    Product? res = await _mediator.Products.FindAsync(productCode);
    //    if (res == null)
    //        return NotFound();
    //    else
    //        return res;
    //}

    //[HttpPut]
    //public async Task<ActionResult<Product>> Put(Product product) {
    //    Product? _findRes = await _context.Products
    //        .FirstOrDefaultAsync(p => p.ProductCode == product.ProductCode);
    //    if (_findRes != null) {
    //        _findRes.ProductName = product.ProductName;
    //        _findRes.ProductScale = product.ProductScale;
    //        _findRes.ProductVendor = product.ProductVendor;
    //        _findRes.ProductDescription = product.ProductDescription;

    //        _context.Products.Update(_findRes);
    //        await _context.SaveChangesAsync();
    //        return Ok();
    //    }
    //    else {
    //        await _context.Products.AddAsync(product);
    //        await _context.SaveChangesAsync();
    //        return Created("", product);
    //    }
    //}

    //[HttpDelete("{productCode}")]
    //public async Task<ActionResult<Product>> Delete(string productCode) {
    //    Product? _findRes = await _context.Products
    //        .FirstOrDefaultAsync(p => p.ProductCode == productCode);
    //    if (_findRes != null) {
    //        _context.Products.Remove(_findRes);
    //        await _context.SaveChangesAsync();
    //        return Ok("Deleted");
    //    }
    //    else {
    //        return NotFound("Not found");
    //    }
    //}
}
