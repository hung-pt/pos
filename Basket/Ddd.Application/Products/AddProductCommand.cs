using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;

namespace Ddd.Application.ProductLines;

public record AddProductCommand(dynamic NewItem) : ICommand<object?>;

file class Handler : HandlerBase, ICommandHandler<AddProductCommand, object?> {
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
