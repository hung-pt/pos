using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;

namespace Ddd.Application.Offices;

public record struct UpdateOfficeCommand(string OfficeCode) : IRequest<Office?>;

file class H : HandlerBase, IRequestHandler<RemoveOfficeCommand, Office?> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<Office?> Handle(RemoveOfficeCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
