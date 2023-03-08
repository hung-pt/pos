using Catalog.Application.Interfaces;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Catalog.Application.ProductLines;

public record struct GetProductLineByIdQuery(string ProductLineCode) : IQuery<ProductLine?>;

file class Handler : HandlerBase, IQueryHandler<GetProductLineByIdQuery, ProductLine?> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<ProductLine?> Handle(GetProductLineByIdQuery r, CancellationToken ct) {
        var res = await _context.ProductLines
            .Where(e => e.ProductLineCode == r.ProductLineCode)
            .FirstOrDefaultAsync(ct);
        return res;
    }
}
