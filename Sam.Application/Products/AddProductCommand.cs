using MediatR;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Products;

public record AddProductCommand(dynamic NewItem, Type? ResponseType) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<AddProductCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(AddProductCommand request, CancellationToken ct) {
        Product newProduct = Mapper.Map<Product>(request.NewItem);
        newProduct.ProductCode = EntityCodeGenerator.GetNewProductCode();
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(ct);
        return Mapper.Map(newProduct, request.ResponseType);
    }
}
