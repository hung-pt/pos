using Ddd.Application.Default;
using Ddd.Application.Dtos;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using MediatR;

namespace Ddd.Application.Offices;

public record struct AddOfficeCommand(
    string OfficeCode,
    string? City = default,
    string? Phone = default,
    string? AddressLine1 = default,
    string? AddressLine2 = default,
    string? State = default,
    string? Country = default,
    string? PostalCode = default,
    string? Territory = default
) : IRequest<OfficeDto>;

file class H : HandlerBase, IRequestHandler<AddOfficeCommand, OfficeDto> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<OfficeDto> Handle(AddOfficeCommand request, CancellationToken cancellationToken) {
        var newOffice = Mapper.Map<Office>(request);

        _context.Offices.Add(newOffice);
        await _context.SaveChangesAsync(cancellationToken);
        return new OfficeDto(
            newOffice.OfficeCode, newOffice.City, newOffice.Phone, newOffice.AddressLine1, newOffice.AddressLine2, newOffice.State,
            newOffice.Country, newOffice.PostalCode, newOffice.Territory
        );
    }
}
