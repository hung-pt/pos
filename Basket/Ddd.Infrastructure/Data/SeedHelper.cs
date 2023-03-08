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
        decimal creditLimit) => new() {
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

    internal static Order CreateOrder(
        int orderNumber,
        DateTime orderDate,
        DateTime? requiredDate,
        DateTime? shippedDate,
        string? status,
        string? comments,
        int customerNumber) => new() {
            OrderNumber = orderNumber,
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
    internal static Payment CreatePayment(
        int customerNumber,
        string? checkNumber,
        DateTime paymentDate,
        decimal amount) => new() {
            CustomerNumber = customerNumber,
            CheckNumber = checkNumber,
            PaymentDate = paymentDate,
            Amount = amount
        };

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
