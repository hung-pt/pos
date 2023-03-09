using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Offices;

public record GetOfficesQuery(int PageNumber = 1, int PageCount = 10) : IRequest<IList<Office>>;

file class H : HandlerBase, IRequestHandler<GetOfficesQuery, IList<Office>> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<IList<Office>> Handle(GetOfficesQuery request, CancellationToken cancellationToken) {
        return await _context.Offices
            .Skip((request.PageNumber - 1) * request.PageCount)
            .Take(request.PageCount)
            .ToListAsync(cancellationToken);
    }
}
