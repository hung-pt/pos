using Ddd.Application;
using Ddd.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Ddd.Application.Default;

public record GetByIdQuery(
    Type EntityType,
    object Id,
    Type ResponseType,
    int PageIndex = 1,
    int PageSize = 10
) : IRequest<object?>;

public class GetByIdHandler : HandlerBase, IRequestHandler<GetByIdQuery, object?> {
    public GetByIdHandler(IApplicationDbContext context) : base(context) { }

    public async Task<object?> Handle(GetByIdQuery request, CancellationToken ccToken) {
        throw new NotImplementedException();

        var keyProperty = request.EntityType.GetProperties()
            .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0);
        if (keyProperty == null) {
            throw new ArgumentException($"Type {request.EntityType.Name} does not have a property marked with the KeyAttribute.");
        }

        var dbSet = _context.GetDbSet(request.EntityType);

        // The LINQ expression 'DbSet<Product>()
        //   .Where(p => __keyProperty_0.GetValue(p) == __p_1)' could not be translated.
        var entity = dbSet.SingleOrDefault(e => keyProperty.GetValue(e) == request.Id);
        return Mapper.Map(entity, request.ResponseType);
    }
}
