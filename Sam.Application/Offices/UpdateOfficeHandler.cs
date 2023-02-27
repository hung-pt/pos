using MediatR;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

public record struct UpdateOfficeCommand(string OfficeCode) : IRequest<Office?>;

public class UpdateOfficeHandler : HandlerBase, IRequestHandler<RemoveOfficeCommand, Office?> {
    public UpdateOfficeHandler(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(RemoveOfficeCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
