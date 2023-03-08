namespace Api.Catalog.Dtos; 

public class ProductDto {
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? ProductScale { get; set; }
    public string? ProductVendor { get; set; }
    public string? ProductDescription { get; set; }
    public int? QuantityInStock { get; set; }
    public decimal? BuyPrice { get; set; }
    public decimal? MSRP { get; set; }

    public string? ProductLineCode { get; set; }
}
