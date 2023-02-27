﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Z.EntityFramework.Plus;

namespace Sam.Application.Products;

public record struct GetProductByIdQuery(string ProductCode, Type? ResponseType) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<GetProductByIdQuery, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(GetProductByIdQuery r, CancellationToken ct) {
        var res = await _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FirstOrDefaultAsync(ct);
        return Mapper.Map(res, r.ResponseType);
    }
}
