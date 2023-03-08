using Ddd.Application.Default;
using Ddd.Application.Interfaces;

namespace Ddd.Application.Offices.Legacy;

public record struct RemoveOfficeCommand(string OfficeCode) : ICommand<int>;

public class RemoveOfficeCommandHandler : HandlerBase, ICommandHandler<RemoveOfficeCommand, int> {
    public RemoveOfficeCommandHandler(IApplicationDbContext context) : base(context) { }

    public Task<int> Handle(RemoveOfficeCommand request, CancellationToken cancellationToken) {
        var office = _context.Offices.Find(request.OfficeCode);
        if (office == null) {
            return Task.FromResult(0);
        }
        else {
            _context.Offices.Remove(office);
            return Task.FromResult(_context.SaveChanges());
        }
    }
}
