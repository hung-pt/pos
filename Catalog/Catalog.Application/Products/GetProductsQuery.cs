using Catalog.Application.Interfaces;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Products;

public record GetProductsQuery(int Page, int PageSize) : IQuery<IEnumerable<Product?>>;

file class Handler : HandlerBase, IQueryHandler<GetProductsQuery, IEnumerable<Product?>> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<IEnumerable<Product?>> Handle(GetProductsQuery r, CancellationToken ct) {
        return await _context.Products
            .Skip((r.Page - 1) * r.PageSize)
            .Take(r.PageSize)
            .ToListAsync(ct);
    }
}
