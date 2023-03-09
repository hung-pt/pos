namespace Ddd.Application.Dtos;

public record OfficeDto(
    string OfficeCode,
    string? City,
    string? Phone,
    string? AddressLine1,
    string? AddressLine2,
    string? State,
    string? Country,
    string? PostalCode,
    string? Territory
);
