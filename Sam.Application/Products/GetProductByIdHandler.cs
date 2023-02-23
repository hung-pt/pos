using MediatR;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record struct GetProductByIdQuery(string OfficeCode) : IRequest<Office?>;

public class GetProductByIdHandler : RequestHandlerBase, IRequestHandler<GetProductByIdQuery, Office?> {
    public GetProductByIdHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(GetProductByIdQuery r, CancellationToken ct) {
        var query = _context.Products.Where(p => p.BuyPrice > 100);
        query = query.Cacheable();
        var result = await query.ToListAsync();


        // result-set cache
        return await _context.Offices
            .Where(o => o.OfficeCode == r.OfficeCode)
            //.Include(o => o.Staff)
            .Cacheable()
            .FirstOrDefaultAsync(ct);
    }
}
