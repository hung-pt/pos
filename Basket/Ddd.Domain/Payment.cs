using System.Text.Json.Serialization;
using Ddd.Domain.Common;

namespace Ddd.Domain;

public class Payment : Entity {
    public int CustomerNumber { get; set; }
    [JsonIgnore] public virtual Customer? Customer { get; set; }
    public string? CheckNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}
