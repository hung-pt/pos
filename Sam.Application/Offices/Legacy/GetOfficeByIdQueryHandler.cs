using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Offices.Legacy;

public record struct GetOfficeByIdQuery(string OfficeCode) : IQuery<Office?>;

public class GetOfficeByIdQueryHandler : HandlerBase, IQueryHandler<GetOfficeByIdQuery, Office?> {
    public GetOfficeByIdQueryHandler(IApplicationDbContext context) : base(context) { }

    public Office? Handle(GetOfficeByIdQuery query) {
        return _context.Offices
            .Where(o => o.OfficeCode == query.OfficeCode)
            .Include(o => o.Staff)
            .FirstOrDefault();
    }
}
