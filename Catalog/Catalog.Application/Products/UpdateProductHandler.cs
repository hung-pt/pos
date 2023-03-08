using Catalog.Application.Interfaces;

namespace Catalog.Application.Products;

public record UpdateProductCommand(Type? ResponseType) : ICommand<object?>;

file class Handler : HandlerBase, ICommandHandler<UpdateProductCommand, object?> {
    public Handler(ICatalogDbContext context) : base(context) { }

    public Task<object?> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
