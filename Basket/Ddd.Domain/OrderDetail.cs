using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation.Results;

namespace Ddd.Domain;

public class OrderDetail : Entity {
    public int OrderNumber { get; set; }
    [JsonIgnore] public virtual Order? Order { get; set; }
    public int LineNumber { get; set; }
    public string? ProductCode { get; set; }
    [JsonIgnore] public virtual Product? Product { get; set; }
    public int QuantityOrdered { get; set; }
    public decimal PriceEach { get; set; }

    public OrderDetail() : base() { }

    public OrderDetail(int orderNumber, int lineNumber) : base() {
        OrderNumber = orderNumber;
        LineNumber = lineNumber;
    }
}
