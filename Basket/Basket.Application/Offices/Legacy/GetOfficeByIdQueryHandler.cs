using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Offices.Legacy;

public record struct GetOfficeByIdQuery(string OfficeCode) : IQuery<Office?>;

public class GetOfficeByIdQueryHandler : HandlerBase, IQueryHandler<GetOfficeByIdQuery, Office?> {
    public GetOfficeByIdQueryHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken) {
        return await _context.Offices
            .Where(o => o.OfficeCode == request.OfficeCode)
            .Include(o => o.Staff)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
