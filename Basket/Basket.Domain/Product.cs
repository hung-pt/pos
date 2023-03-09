using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ddd.Domain.Common;

namespace Ddd.Domain;

public class Product : Entity {
    [Key] public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? ProductScale { get; set; }
    public string? ProductVendor { get; set; }
    public string? ProductDescription { get; set; }
    public int QuantityInStock { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal MSRP { get; set; }

    public string? ProductLineCode { get; set; }
    [JsonIgnore] public virtual ProductLine? ProductLine { get; set; }
    [JsonIgnore] public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
