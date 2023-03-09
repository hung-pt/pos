using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Ddd.Application.ProductLines;

public record struct GetProductLineByIdQuery(string ProductLineCode) : IQuery<ProductLine?>;

file class Handler : HandlerBase, IQueryHandler<GetProductLineByIdQuery, ProductLine?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<ProductLine?> Handle(GetProductLineByIdQuery r, CancellationToken ct) {
        var res = await _context.ProductLines
            .Where(e => e.ProductLineCode == r.ProductLineCode)
            .FirstOrDefaultAsync(ct);
        return res;
    }
}
