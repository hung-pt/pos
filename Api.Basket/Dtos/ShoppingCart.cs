namespace Api.Basket.Dtos; 

public record ShoppingCart(
    string CustomerName,
    ShoppingItem[] Items
);

public record ShoppingItem(
    string ProductCode,
    int QuantityOrdered,
    decimal PriceEach
);
