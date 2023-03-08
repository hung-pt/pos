using Catalog.Domain;

namespace Catalog.Infrastructure.Data;

internal static class SeedHelper {
    internal static ProductLine CreateProductLine(
        string productLineCode,
        string productLineName,
        string? textDescription,
        string? htmlDescription,
        byte[]? image) => new() {
            ProductLineCode = productLineCode,
            ProductLineName = productLineName,
            TextDescription = textDescription,
            HtmlDescription = htmlDescription,
            Image = image,
        };

    internal static Product CreateProduct(
        string? productCode,
        string? productName,
        string? productLineCode,
        string? productScale,
        string? productVendor,
        string? productDescription,
        int quantityInStock,
        decimal buyPrice,
        decimal mSRP) => new() {
            ProductCode = productCode,
            ProductName = productName,
            ProductLineCode = productLineCode,
            ProductScale = productScale,
            ProductVendor = productVendor,
            ProductDescription = productDescription,
            QuantityInStock = quantityInStock,
            BuyPrice = buyPrice,
            MSRP = mSRP
        };
}
