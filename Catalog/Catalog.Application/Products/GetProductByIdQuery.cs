using Catalog.Application.Interfaces;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Catalog.Application.Products;

public record struct GetProductByIdQuery(string ProductCode) : IQuery<Product?>;

file class Handler : HandlerBase, IQueryHandler<GetProductByIdQuery, Product?> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<Product?> Handle(GetProductByIdQuery r, CancellationToken ct) {
        return await _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FirstOrDefaultAsync(ct);
    }
}
