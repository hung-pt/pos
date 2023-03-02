using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Ddd.Application.Products;

public record struct GetProductByIdWithCacheQuery(string ProductCode, Type? ResponseType) : IRequest<Product?>;

file class Handler : HandlerBase, IRequestHandler<GetProductByIdWithCacheQuery, Product?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<Product?> Handle(GetProductByIdWithCacheQuery r, CancellationToken ct) {
        return _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FromCache() // sd cau query lam key cua cache
            .FirstOrDefault();
    }
}
