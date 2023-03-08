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
    // emp

    internal static Customer CreateCustomer(int customerNumber,
        string? customerName,
        string? contactLastName,
        string? contactFirstName,
        string? phone,
        string? addressLine1,
        string? addressLine2,
        string? city,
        string? state,
        string? postalCode,
        string? country,
        int? salesRepEmployeeNumber,
        decimal creditLimit) => new(customerNumber) {
            CustomerNumber = customerNumber,
            CustomerName = customerName,
            ContactLastName = contactLastName,
            ContactFirstName = contactFirstName,
            Phone = phone,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            City = city,
            State = state,
            PostalCode = postalCode,
            Country = country,
            SalesRepEmployeeNumber = salesRepEmployeeNumber,
            CreditLimit = creditLimit
        };

    internal static ProductLine CreateProductLine(
        string productLineCode,
        string productLineName,
        string? textDescription,
        string? htmlDescription,
        byte[]? image) => new(productLineCode) {
            ProductLineName = productLineName,
            TextDescription = textDescription,
            HtmlDescription = htmlDescription,
            Image = image,
        };


    internal static Order CreateOrder(
        int orderNumber,
        DateTime orderDate,
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

    internal static OrderDetail CreateOrderDetail(
        int orderNumber,
        int lineNumber,
        string productCode,
        int quantityOrdered,
        decimal priceEach) => new(orderNumber, lineNumber) {
            ProductCode = productCode,
            QuantityOrdered = quantityOrdered,
            PriceEach = priceEach
        };
}
