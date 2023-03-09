using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using Ddd.Domain.Validators;
using FluentValidation;
using MediatR;

namespace Ddd.Application.Customers;

public record NewCustomerDto(
    string CustomerName,
    string? ContactLastName,
    string? ContactFirstName,
    string? Phone,
    string? AddressLine1,
    string? AddressLine2,
    string? City,
    string? State,
    string? PostalCode,
    string? Country,
    decimal? CreditLimit
);

public record AddCustomerCommand(NewCustomerDto NewCustomer) : IRequest<Customer>;

file class Handler : HandlerBase, IRequestHandler<AddCustomerCommand, Customer> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<Customer> Handle(AddCustomerCommand r, CancellationToken ct) {
        Customer newCustomer = new();

        //
        EntityCodeGenerator.GetNewCustomerNumber();
        newCustomer = Mapper.Map(r.NewCustomer, newCustomer)!;

        //
        new CustomerValidator().ValidateAndThrow(newCustomer);

        //
        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync(ct);
        return newCustomer;
    }
}
