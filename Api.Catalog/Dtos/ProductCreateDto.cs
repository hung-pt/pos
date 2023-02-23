namespace Api.Catalog.Dtos;

public record ProductCreateDto(
    string ProductName,
    string? ProductScale,
    string? ProductVendor,
    string? ProductDescription,
    int? QuantityInStock,
    decimal? BuyPrice,
    decimal? MSRP,
    string ProductLineCode
);
