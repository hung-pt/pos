using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices.Legacy;

public record struct GetOfficesQuery() : IQuery<IList<Office>>;

public class GetOfficesQueryHandler : RequestHandlerBase, IQueryHandler<GetOfficesQuery, IList<Office>> {
    public GetOfficesQueryHandler(IApplicationDbContext context) : base(context) { }

    public IList<Office> Handle(GetOfficesQuery request) {
        return _context.Offices.Take(10).ToList();
    }
}
