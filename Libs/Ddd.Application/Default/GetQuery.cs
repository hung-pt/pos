using Ddd.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Default;

public record GetQuery(
    Type EntityType,
    Type? ResponseType,
    int PageIndex = 1,
    int PageSize = 10
) : IRequest<IEnumerable<object>>;

public class GetHandler : HandlerBase, IRequestHandler<GetQuery, IEnumerable<object>> {
    public GetHandler(IApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<object>> Handle(GetQuery req, CancellationToken ct) {
        var dbSet = _context.GetDbSet(req.EntityType) ?? throw new Exception($"No DbSet of type {req.EntityType.Name}.");
        var res = await dbSet
            .Skip((req.PageIndex - 1) * req.PageSize)
            .Take(req.PageSize)
            .ToListAsync(ct);
        return res.Select(e => Mapper.Map(e, req.ResponseType)!);
    }
}
