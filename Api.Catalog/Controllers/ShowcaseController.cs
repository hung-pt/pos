using Api.Catalog.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sam.Domain;
using Sam.Infrastructure.Data;
using Sam.Infrastructure.Others;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace multi_threadings.Controllers;

[ApiController]
[Route("[controller]")]
public class ShowcaseController : ControllerBase {
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ShowcaseController(
        ILogger<ProductController> logger,
        IMediator mediator
        ) {
        _logger = logger;
        _mediator = mediator;
    }

    //// GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
    [HttpGet("query_level_cache")]
    public async Task query_level_cache() {

    }

    [HttpGet("result_set_cache")]
    public async Task result_set_cache() {

    }

}
