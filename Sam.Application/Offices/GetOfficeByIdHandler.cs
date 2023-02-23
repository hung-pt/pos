using MediatR;
using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record struct GetOfficeByIdQuery(string OfficeCode) : IRequest<Office?>;

public class GetOfficeByIdHandler : RequestHandlerBase, IRequestHandler<GetOfficeByIdQuery, Office?> {
    public GetOfficeByIdHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken) {
        return await _context.Offices
            .Where(o => o.OfficeCode == request.OfficeCode)
            .Include(o => o.Staff)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
