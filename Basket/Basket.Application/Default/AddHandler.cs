using Ddd.Application.Interfaces;
using MediatR;

namespace Ddd.Application.Default;

public record AddEntityCommand(
    Type EntityType,
    dynamic EntityToAdd,
    Type ResponseType
) : IRequest<object>;

public class AddHandler : HandlerBase, IRequestHandler<AddEntityCommand, object> {
    public AddHandler(IApplicationDbContext context) : base(context) { }

    public async Task<object> Handle(AddEntityCommand request, CancellationToken ccToken) {
        var newEntity = Mapper.Map(request.EntityToAdd, request.EntityType);

        dynamic dbSet = _context.GetDbSet(request.EntityType);
        dbSet.Add(newEntity);

        await _context.SaveChangesAsync(ccToken);
        return Mapper.Map(newEntity, request.ResponseType);
    }
}
