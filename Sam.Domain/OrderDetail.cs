using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation.Results;

namespace Ddd.Domain;

public class OrderDetail : Entity {
    public int OrderNumber { get; set; }
    [JsonIgnore] public virtual Order? Order { get; set; }
    public string? ProductCode { get; set; }
    [JsonIgnore] public virtual Product? Product { get; set; }
    public int QuantityOrdered { get; set; }
    public decimal PriceEach { get; set; }
    public int OrderLineNumber { get; set; }

    public OrderDetail() { }
    public OrderDetail(int orderNumber, string productCode, int quantityOrdered, decimal priceEach, int orderLineNumber) {
        OrderNumber = orderNumber;
        ProductCode = productCode;
        QuantityOrdered = quantityOrdered;
        PriceEach = priceEach;
        OrderLineNumber = orderLineNumber;
    }

    public override ValidationResult Validate() {
        throw new NotImplementedException();
    }
}
