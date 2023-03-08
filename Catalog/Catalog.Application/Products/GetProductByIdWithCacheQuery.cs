using Catalog.Application.Interfaces;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Catalog.Application.Products;

public record struct GetProductByIdWithCacheQuery(string ProductCode, Type? ResponseType) : IQuery<Product?>;

file class Handler : HandlerBase, IQueryHandler<GetProductByIdWithCacheQuery, Product?> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<Product?> Handle(GetProductByIdWithCacheQuery r, CancellationToken ct) {
        return _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FromCache() // sd cau query lam key cua cache
            .FirstOrDefault();
    }
}
