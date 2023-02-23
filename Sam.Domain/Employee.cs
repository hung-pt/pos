using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sam.Domain;

public class Employee : Entity {
    [Key] public int EmployeeNumber { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Extension { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }

    public string? OfficeCode { get; set; }
    [JsonIgnore] public virtual Office? Office { get; set; }
    public int? ReportsToEmployeeNumber { get; set; }
    [JsonIgnore] public virtual Employee? ReportsTo { get; set; }
    [JsonIgnore] public virtual List<Employee>? Subordinates { get; set; }

    public Employee() { }
    public Employee(
        int employeeNumber, string? lastName, string? firstName, string? extension, string? email, string? officeCode,
        int? reportsTo, string? jobTitle
        ) {
        EmployeeNumber = employeeNumber;
        LastName = lastName;
        FirstName = firstName;
        Extension = extension;
        Email = email;
        OfficeCode = officeCode;
        ReportsToEmployeeNumber = reportsTo;
        JobTitle = jobTitle;
    }
}
