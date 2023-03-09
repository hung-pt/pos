using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Ddd.Application.ProductLines;

public record struct GetProductLineQuery(
    int? Page,
    int? PageSize,
    bool? UseCache = true
) : IRequest<ProductLine>;

file class H : HandlerBase, IRequestHandler<GetProductByIdQuery, object?> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(GetProductByIdQuery r, CancellationToken ct) {
        // validations

        //
        var res = await _context.Products
            .Where(e => e.ProductCode == r.ProductCode)
            .Include(e => e.ProductLine)
            .FirstOrDefaultAsync(ct);
        return Mapper.Map(res, r.ResponseType);
    }
}
