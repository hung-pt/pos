using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sam.Domain;


//public record struct OfficeCode {
//    public string OfficeCode { get; set; }
//}


public class Office : Entity {
    [Key] public string OfficeCode { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? Territory { get; set; }
    [JsonIgnore] public virtual List<Employee>? Staff { get; set; }

    public Office() { } // for mapper

    public Office(string officeCode) {
        OfficeCode = officeCode;
    }

    public Office(
        string officeCode, string? city, string? phone, string? addressLine1, string? addressLine2, string? state, string? country,
        string? postalCode, string? territory
        ) : this(officeCode) {
        OfficeCode = officeCode;
        City = city;
        Phone = phone;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        State = state;
        Country = country;
        PostalCode = postalCode;
        Territory = territory;
    }
}
