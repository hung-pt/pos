using MediatR;
using Sam.Application.Interfaces;

namespace Sam.Application.Default;

public record AddEntityCommand(
    Type EntityType,
    dynamic EntityToAdd,
    Type ResponseType,
    int PageIndex = 1,
    int PageSize = 10
) : IRequest<object>;

public class AddHandler : RequestHandlerBase, IRequestHandler<AddEntityCommand, object> {
    public AddHandler(IApplicationDbContext context) : base(context) { }

    public async Task<object> Handle(AddEntityCommand request, CancellationToken ccToken) {
        var newEntity = Mapper.Map(request.EntityToAdd, request.EntityType);

        dynamic dbSet = _context.GetDbSet(request.EntityType);
        dbSet.Add(newEntity);

        await _context.SaveChangesAsync(ccToken);
        return Mapper.Map(newEntity, request.ResponseType);
    }
}
