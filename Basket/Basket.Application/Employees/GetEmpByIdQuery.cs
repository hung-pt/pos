using Ddd.Application;
using Ddd.Application.Default;
using Ddd.Application.Interfaces;
using MediatR;

namespace Ddd.Application.Employees {
    public record GetEmpByIdQuery(int EmpNo, Type ReturnType) : IRequest<object?>;

    file class Handler : HandlerBase, IRequestHandler<GetEmpByIdQuery, object?> {
        public Handler(IApplicationDbContext context) : base(context) { }

        public async Task<object?> Handle(GetEmpByIdQuery r, CancellationToken ct) {
            var res = await _context.Employees.FindAsync(r.EmpNo);

            return Mapper.Map(res, r.ReturnType);
        }
    }
}
