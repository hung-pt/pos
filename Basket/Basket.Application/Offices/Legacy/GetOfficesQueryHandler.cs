using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Offices.Legacy;

public record struct GetOfficesQuery() : IQuery<IList<Office>>;

public class GetOfficesQueryHandler : HandlerBase, IQueryHandler<GetOfficesQuery, IList<Office>> {
    public GetOfficesQueryHandler(IApplicationDbContext context) : base(context) { }

    public async Task<IList<Office>> Handle(GetOfficesQuery request, CancellationToken cancellationToken) {
        return await _context.Offices.ToListAsync(cancellationToken);
    }
}
