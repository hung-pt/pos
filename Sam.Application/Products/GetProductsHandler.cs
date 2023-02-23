using MediatR;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record GetProductsQuery(int PageNumber = 1, int PageCount = 10) : IRequest<IList<Product>>;

public class GetProductsHandler : RequestHandlerBase, IRequestHandler<GetProductsQuery, IList<Product>> {
    public GetProductsHandler(IApplicationDbContext context) : base(context) { }

    public async Task<IList<Product>> Handle(GetProductsQuery r, CancellationToken ct) {
        return await _context.Products
            .Skip((r.PageNumber - 1) * r.PageCount)
            .Take(r.PageCount)
            .Include(e => e.ProductLine)
            .ToListAsync(ct);
    }
}
