using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;

namespace Ddd.Application.Offices.Legacy;

public record struct AddOfficeCommand(Office NewOffice) : ICommand;

public class AddOfficeCommandHandler : HandlerBase, ICommandHandler<AddOfficeCommand> {
    public AddOfficeCommandHandler(IApplicationDbContext context) : base(context) { }

    public Task Handle(AddOfficeCommand request, CancellationToken cancellationToken) {
        _context.Offices.Add(request.NewOffice);
        return Task.FromResult(_context.SaveChanges());
    }
}