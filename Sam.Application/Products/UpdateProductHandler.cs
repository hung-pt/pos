using MediatR;
using Sam.Application.Default;
using Sam.Application.Interfaces;

namespace Sam.Application.Products;

public record UpdateProductCommand(Type? ResponseType) : IRequest<object?>;

file class Handler : HandlerBase, IRequestHandler<UpdateProductCommand, object?> {
    public Handler(IApplicationDbContext context) : base(context) { }

    public Task<object?> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
