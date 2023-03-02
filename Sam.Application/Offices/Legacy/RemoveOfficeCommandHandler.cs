using Ddd.Application.Default;
using Ddd.Application.Interfaces;

namespace Ddd.Application.Offices.Legacy;

public record struct RemoveOfficeCommand(string OfficeCode) : ICommand;

public class RemoveOfficeCommandHandler : HandlerBase, ICommandHandler<RemoveOfficeCommand> {
    public RemoveOfficeCommandHandler(IApplicationDbContext context) : base(context) { }

    public int Handle(RemoveOfficeCommand request) {
        var office = _context.Offices.Find(request.OfficeCode);
        if (office == null) {
            return 0;
        }
        else {
            _context.Offices.Remove(office);
            return _context.SaveChanges();
        }
    }
}
