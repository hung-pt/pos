using System.Text.Json.Serialization;

namespace Sam.Domain;

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
    public Customer(int customerNumber, string? customerName, string? contactLastName, string? contactFirstName, string? phone,
        string? addressLine1, string? addressLine2, string? city, string? state, string? postalCode, string? country, int? salesRepEmployeeNumber,
        decimal creditLimit
        ) {
        CustomerNumber = customerNumber;
        CustomerName = customerName;
        ContactLastName = contactLastName;
        ContactFirstName = contactFirstName;
        Phone = phone;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        SalesRepEmployeeNumber = salesRepEmployeeNumber;
        CreditLimit = creditLimit;
    }
}
