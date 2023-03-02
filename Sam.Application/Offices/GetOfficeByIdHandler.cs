using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Offices;

public record struct GetOfficeByIdQuery(string OfficeCode) : IRequest<Office?>;

file class H : HandlerBase, IRequestHandler<GetOfficeByIdQuery, Office?> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken) {
        return await _context.Offices
            .Where(o => o.OfficeCode == request.OfficeCode)
            .Include(o => o.Staff)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
