using MediatR;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record GetOfficesQuery(int PageNumber = 1, int PageCount = 10) : IRequest<IList<Office>>;

public class GetOfficesHandler : RequestHandlerBase, IRequestHandler<GetOfficesQuery, IList<Office>> {
    public GetOfficesHandler(IApplicationDbContext context) : base(context) { }

    public async Task<IList<Office>> Handle(GetOfficesQuery request, CancellationToken cancellationToken) {
        return await _context.Offices
            .Skip((request.PageNumber - 1) * request.PageCount)
            .Take(request.PageCount)
            .ToListAsync(cancellationToken);
    }
}
