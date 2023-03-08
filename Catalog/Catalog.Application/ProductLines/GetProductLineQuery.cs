using Catalog.Application.Interfaces;
using Catalog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.ProductLines;

public record struct GetProductLineQuery(
    int? Page,
    int? PageSize,
    bool? UseCache = true
) : IRequest<IEnumerable<ProductLine?>>;

file class Handler : HandlerBase, IRequestHandler<GetProductLineQuery, IEnumerable<ProductLine?>> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<IEnumerable<ProductLine?>> Handle(GetProductLineQuery request, CancellationToken ct) {
        // validations

        //
        var res = await _context.ProductLines
            .Take(20).ToListAsync(ct);
        //.Where(e => e.ProductLineCode == r.ProductCode)
        //.Include(e => e.ProductLine)
        //.FirstOrDefaultAsync(ct);
        return res;
    }
}
