using Catalog.Application.Interfaces;

namespace Catalog.Application.Products;

public record RemoveProductCommand(string ProductCode) : ICommand;

file class Handler : HandlerBase, ICommandHandler<RemoveProductCommand> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public async Task Handle(RemoveProductCommand r, CancellationToken ct) {
        var prod = await _context.Products.FindAsync(new object?[] { r.ProductCode }, ct);
        if (prod != null) _context.Products.Remove(prod);
        await _context.SaveChangesAsync(ct);
    }
}
