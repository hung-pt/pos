using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;

namespace Ddd.Application.Default;

public record struct RemoveCommand(string Id) : IRequest<Office?>;

public class RemoveHandler : HandlerBase, IRequestHandler<RemoveCommand, Office?> {
    public RemoveHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(RemoveCommand req, CancellationToken ct) {
        throw new NotImplementedException();
        //var office = await _context.Offices.SingleOrDefaultAsync(o => o.OfficeCode == req.OfficeCode, ct);
        //if (office == null) {
        //    return default;
        //}
        //else {
        //    _context.Offices.Remove(office);
        //    await _context.SaveChangesAsync(ct);
        //    return office;
        //}
    }
}
