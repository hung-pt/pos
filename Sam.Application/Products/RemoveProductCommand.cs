using MediatR;
using Sam.Application.Default;
using Sam.Application.Interfaces;

namespace Sam.Application.Products;

public record RemoveProductCommand(string ProductCode) : IRequest;

file class Handler : HandlerBase, IRequestHandler<RemoveProductCommand> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public async Task Handle(RemoveProductCommand r, CancellationToken ct) {
        var prod = await _context.Products.FindAsync(new object?[] { r.ProductCode }, ct);
        if (prod != null) _context.Products.Remove(prod);
        await _context.SaveChangesAsync(ct);
    }
}
