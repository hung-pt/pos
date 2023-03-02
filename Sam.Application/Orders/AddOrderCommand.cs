using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using MediatR;

namespace Ddd.Application.Products;

public record OrderCreateDto(
    string CustomerName,
    ShoppingItem[] Items
);

public record ShoppingItem(
    string ProductCode,
    int QuantityOrdered,
    decimal PriceEach
);

public record AddOrderCommand(OrderCreateDto NewIOrder, Type? ResponseType) : IRequest<object?>;

file class H : HandlerBase, IRequestHandler<AddOrderCommand, object?> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(AddOrderCommand request, CancellationToken ct) {
        var order = new Order(EntityCodeGenerator.GetNewOrderNumber()) {

        };
        order.ValidateAndThrow();

        Product newProduct = Mapper.Map<Product>(request.NewIOrder);
        newProduct.ProductCode = EntityCodeGenerator.GetNewProductCode();
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(ct);
        return Mapper.Map(newProduct, request.ResponseType);
    }
}
