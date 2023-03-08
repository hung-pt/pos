using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Customers;

public record GetCustomersQuery(int PageNumber, int PageCount, Type? ResponseType) : IRequest<IEnumerable<object?>>;

file class H : HandlerBase, IRequestHandler<GetCustomersQuery, IEnumerable<object?>> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<object?>> Handle(GetCustomersQuery r, CancellationToken ct) {
        var res = await _context.Customers
            .Skip((r.PageNumber - 1) * r.PageCount)
            .Take(r.PageCount)
            .ToListAsync(ct);
        return res.Select(e => Mapper.Map(e, r.ResponseType));
    }
}
