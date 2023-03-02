using Ddd.Domain;

namespace Ddd.Infrastructure.Data;

internal static class SeedHelper {
    internal static Office CreateOffice(string officeCode,
        string? city,
        string? phone,
        string? addressLine1,
        string? addressLine2,
        string? state,
        string? country,
        string? postalCode,
        string? territory) => new(officeCode) {
            City = city,
            Phone = phone,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            State = state,
            Country = country,
            PostalCode = postalCode,
            Territory = territory,
        };

    internal static ProductLine CreateProductLine(
        string productLineCode,
        string? textDescription,
        string? htmlDescription,
        byte[]? image) => new(productLineCode) {
            TextDescription = textDescription,
            HtmlDescription = htmlDescription,
            Image = image,
        };


    internal static Order CreateOrder(
        int orderNumber,
        DateTime? orderDate,
        DateTime? requiredDate,
        DateTime? shippedDate,
        string? status,
        string? comments,
        int customerNumber) => new(orderNumber) {
            OrderDate = orderDate,
            RequiredDate = requiredDate,
            ShippedDate = shippedDate,
            Status = status,
            Comments = comments,
            CustomerNumber = customerNumber,
        };
}
