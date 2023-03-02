using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ddd.Domain.Common;

namespace Ddd.Domain;


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

    public override FluentValidation.Results.ValidationResult Validate() {
        throw new NotImplementedException();
    }
}
