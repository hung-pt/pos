using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation.Results;

namespace Ddd.Domain;

public class Customer : Entity {
    public int CustomerNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? ContactLastName { get; set; }
    public string? ContactFirstName { get; set; }
    public string? Phone { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public decimal CreditLimit { get; set; }

    public int? SalesRepEmployeeNumber { get; set; }
    [JsonIgnore] public virtual Employee? SalesRep { get; set; }
    [JsonIgnore] public virtual List<Order>? Orders { get; set; }
    [JsonIgnore] public virtual List<Payment>? Payments { get; set; }

    public Customer() { }

    public Customer(int customerNumber) {
        CustomerNumber = customerNumber;
    }

    public override ValidationResult Validate() {
        throw new NotImplementedException();
    }
}

internal static partial class Vtors {

}
