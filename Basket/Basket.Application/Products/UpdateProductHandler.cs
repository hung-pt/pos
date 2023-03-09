using Ddd.Application.Default;
using Ddd.Application.Interfaces;

namespace Ddd.Application.ProductLines;

public record UpdateProductCommand(Type? ResponseType) : ICommand<object?>;

file class Handler : HandlerBase, ICommandHandler<UpdateProductCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public Task<object?> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
