using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;

namespace Ddd.Application.Offices.Legacy;

public record struct GetOfficesQuery() : IQuery<IList<Office>>;

public class GetOfficesQueryHandler : HandlerBase, IQueryHandler<GetOfficesQuery, IList<Office>> {
    public GetOfficesQueryHandler(IApplicationDbContext context) : base(context) { }

    public IList<Office> Handle(GetOfficesQuery request) {
        return _context.Offices.ToList();
    }
}
