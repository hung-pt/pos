namespace Sam.Application.DTOs;

public record struct OfficeCreateDto(
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
