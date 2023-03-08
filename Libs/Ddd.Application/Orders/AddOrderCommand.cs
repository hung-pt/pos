using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using MediatR;

namespace Ddd.Application.Orders;

public record OrderCreateDto(
    int CustomerNumber,
    LineItem[] Items
);

public record LineItem(
    string ProductCode,
    int Quantity,
    decimal Price
);

public record AddOrderCommand(OrderCreateDto NewOrder) : IRequest<Order>;

file class H : HandlerBase, IRequestHandler<AddOrderCommand, Order> {
    public H(IApplicationDbContext context) : base(context) { }

    public async Task<Order> Handle(AddOrderCommand r, CancellationToken ct) {
        var newOrderNo = EntityCodeGenerator.GetNewOrderNumber();
        var newOrder = new Order(newOrderNo) {
            RequiredDate = DateTime.Now.AddDays(7),
            ShippedDate = DateTime.Now.AddDays(7),
            Status = "In Process",
            Comments = "",
            CustomerNumber = r.NewOrder.CustomerNumber,
            OrderDetails = r.NewOrder.Items.Select((e, i) =>
                new OrderDetail(newOrderNo, i) {
                    ProductCode = e.ProductCode,
                    QuantityOrdered = e.Quantity,
                    PriceEach = e.Price,
                }).ToList(),
        };
        newOrder.ValidateAndThrow();
        _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync(ct);
        return newOrder;
    }
}
