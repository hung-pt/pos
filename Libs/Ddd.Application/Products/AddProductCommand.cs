using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using MediatR;

namespace Ddd.Application.Products;

public record AddProductCommand(dynamic NewItem) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<AddProductCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(AddProductCommand request, CancellationToken ct) {
        // should perform some business logic
        Product newProduct = Mapper.Map<Product>(request.NewItem);
        newProduct.ProductCode = EntityCodeGenerator.GetNewProductCode();
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(ct);
        return newProduct;
    }
}
