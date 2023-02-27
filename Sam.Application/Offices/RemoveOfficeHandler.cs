using MediatR;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record struct RemoveOfficeCommand(string OfficeCode) : IRequest<Office?>;

public class RemoveOfficeHandler : HandlerBase, IRequestHandler<RemoveOfficeCommand, Office?> {
    public RemoveOfficeHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(RemoveOfficeCommand request, CancellationToken cancellationToken) {
        var office2 = _context.Offices.Find(request.OfficeCode);
        var office = _context.Offices.FirstOrDefault(o => o.OfficeCode == request.OfficeCode);
        if (office == null) {
            return default;
        }
        else {
            _context.Offices.Remove(office);
            await _context.SaveChangesAsync(cancellationToken);
            return office;
        }
    }
}
