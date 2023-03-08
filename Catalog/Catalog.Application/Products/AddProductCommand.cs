using Catalog.Application.Interfaces;
using Catalog.Domain;
using Catalog.Domain.Common;

namespace Catalog.Application.Products;

public record AddProductCommand(dynamic NewItem) : ICommand<Product?>;

file class Handler : HandlerBase, ICommandHandler<AddProductCommand, Product?> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task<Product?> Handle(AddProductCommand request, CancellationToken ct) {
        // should perform some business logic
        Product newProduct = Mapper.Map<Product>(request.NewItem);
        newProduct.ProductCode = EntityCodeGenerator.GetNewProductCode();
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(ct);
        return newProduct;
    }
}
