using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using MediatR;

namespace Ddd.Application.Products;

public record AddProductCommand(dynamic NewItem, Type? ResponseType) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<AddOrderCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(AddOrderCommand request, CancellationToken ct) {
        Product newProduct = Mapper.Map<Product>(request.NewItem);
        newProduct.ProductCode = EntityCodeGenerator.GetNewProductCode();
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(ct);
        return Mapper.Map(newProduct, request.ResponseType);
    }
}
