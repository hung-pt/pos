using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Ddd.Application.ProductLines;

public record struct GetProductByIdQuery(string ProductCode, Type? ResponseType) : IQuery<object?>;

file class Handler : HandlerBase, IQueryHandler<GetProductByIdQuery, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(GetProductByIdQuery r, CancellationToken ct) {
        var res = await _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FirstOrDefaultAsync(ct);
        return Mapper.Map(res, r.ResponseType);
    }
}
