using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation.Results;

namespace Ddd.Domain;

public class Payment : Entity {
    public int CustomerNumber { get; set; }
    [JsonIgnore] public virtual Customer? Customer { get; set; }
    public string? CheckNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }

    public Payment() { }
    public Payment(int customerNumber, string? checkNumber, DateTime paymentDate, decimal amount) {
        CustomerNumber = customerNumber;
        CheckNumber = checkNumber;
        PaymentDate = paymentDate;
        Amount = amount;
    }

    public override ValidationResult Validate() {
        throw new NotImplementedException();
    }
}
