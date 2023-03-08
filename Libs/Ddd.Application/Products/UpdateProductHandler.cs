using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using MediatR;

namespace Ddd.Application.Products;

public record UpdateProductCommand(Type? ResponseType) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<UpdateProductCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public Task<object?> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
