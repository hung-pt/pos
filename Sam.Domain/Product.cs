using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sam.Domain;

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

    public Product() { }
    public Product(string? productCode, string? productName, string? productLineCode, string? productScale, string? productVendor,
        string? productDescription, int quantityInStock, decimal buyPrice, decimal mSRP
        ) {
        ProductCode = productCode;
        ProductName = productName;
        ProductLineCode = productLineCode;
        ProductScale = productScale;
        ProductVendor = productVendor;
        ProductDescription = productDescription;
        QuantityInStock = quantityInStock;
        BuyPrice = buyPrice;
        MSRP = mSRP;
    }
}
