using Ddd.Application.Interfaces;
using Ddd.Domain;
using Ddd.Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ddd.Application.Default;

public record GetByIdQuery(
    Type EntityType,
    object Id
) : IRequest<object?>;

public class GetByIdHandler : HandlerBase, IRequestHandler<GetByIdQuery, object?> {
    public GetByIdHandler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(GetByIdQuery request, CancellationToken ccToken) {
        var keyProperty = request.EntityType
            .GetProperties()
            .FirstOrDefault(p =>
                p.GetCustomAttributes(
                    typeof(KeyAttribute),
                    false
                ).Length > 0
            ) ?? throw new ArgumentException(
                $"Type {request.EntityType.Name} does not have a " +
                $"property marked with the KeyAttribute.");
        var dbSet = _context.GetDbSet(request.EntityType);
        return dbSet.SingleOrDefault(e => keyProperty.GetValue(e) == request.Id);
    }
}
