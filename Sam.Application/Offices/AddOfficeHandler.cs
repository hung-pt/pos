using MediatR;
using Sam.Application.Default;
using Sam.Application.DTOs;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices;

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

public class AddOfficeHandler : HandlerBase, IRequestHandler<AddOfficeCommand, OfficeDto> {
    public AddOfficeHandler(IApplicationDbContext context) : base(context) { }

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
