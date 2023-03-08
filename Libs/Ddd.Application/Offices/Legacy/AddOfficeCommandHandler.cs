using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;

namespace Ddd.Application.Offices.Legacy;

public record struct AddOfficeCommand(Office NewOffice) : ICommand;

public class AddOfficeCommandHandler : HandlerBase, ICommandHandler<AddOfficeCommand> {
    public AddOfficeCommandHandler(IApplicationDbContext context) : base(context) { }

    public int Handle(AddOfficeCommand request) {
        _context.Offices.Add(request.NewOffice);
        return _context.SaveChanges();
    }
}
