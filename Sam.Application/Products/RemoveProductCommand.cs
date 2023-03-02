using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using MediatR;

namespace Ddd.Application.Products;

public record RemoveProductCommand(string ProductCode) : IRequest;

file class Handler : HandlerBase, IRequestHandler<RemoveProductCommand> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task Handle(RemoveProductCommand r, CancellationToken ct) {
        var prod = await _context.Products.FindAsync(new object?[] { r.ProductCode }, ct);
        if (prod != null) _context.Products.Remove(prod);
        await _context.SaveChangesAsync(ct);
    }
}
