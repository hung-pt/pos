using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Products;

public record ListProductsQuery(int PageIndex, int PageSize, Type? ResponseType) : IRequest<IEnumerable<object?>>;

file class Handler : HandlerBase, IRequestHandler<ListProductsQuery, IEnumerable<object?>> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<object?>> Handle(ListProductsQuery r, CancellationToken ct) {
        var res = await _context.Products
            .Skip((r.PageIndex - 1) * r.PageSize)
            .Take(r.PageSize)
            .Include(e => e.ProductLine)
            .ToListAsync(ct);
        return res.Select(e => Mapper.Map(e, r.ResponseType));
    }
}
