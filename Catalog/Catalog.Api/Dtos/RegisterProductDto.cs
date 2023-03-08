namespace Catalog.Api.Dtos;

public record RegisterProductDto(
    string ProductName,
    string? ProductScale,
    string? ProductVendor,
    string? ProductDescription,
    int? QuantityInStock,
    decimal? BuyPrice,
    decimal? MSRP,
    string ProductLineCode
);
