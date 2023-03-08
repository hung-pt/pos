using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation;
using FluentValidation.Results;

namespace Ddd.Domain;

public class Order : Entity {
    public int OrderNumber { get; }
    public DateTime OrderDate { get; set; }

    /// <summary> When the customer needs the products. </summary>
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    /// <summary>
    /// Shipped, On Hold, Resolved, Cancelled, In Process,...
    /// </summary>
    public string? Status { get; set; }
    public string? Comments { get; set; }
    public int? CustomerNumber { get; set; }
    [JsonIgnore] public virtual Customer? Customer { get; set; }
    [JsonIgnore] public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

    public Order(int orderNumber) {
        OrderNumber = orderNumber;
        OrderDate = DateTime.Now;
    }

    public override ValidationResult Validate() {
        return Vtors.OrderValidator.Validate(this);
    }

    public void ValidateAndThrow() {
        Vtors.OrderValidator.ValidateAndThrow(this);
    }
}

internal class OrderValidator : AbstractValidator<Order> {
    internal OrderValidator() {
        RuleFor(e => e.OrderDetails).NotEmpty();
        RuleFor(e => e.OrderDate).GreaterThanOrEqualTo(DateTime.Now);
        RuleFor(e => e.RequiredDate).GreaterThanOrEqualTo(DateTime.Now).When(e => e.RequiredDate != null);
        RuleFor(e => e.ShippedDate).GreaterThanOrEqualTo(DateTime.Now).When(e => e.ShippedDate != null);
        //RuleFor(e => e.Status).NotNull();
        RuleFor(e => e.CustomerNumber).NotNull();
    }
}

internal static partial class Vtors {
    internal static OrderValidator OrderValidator { get; } = new();
}
