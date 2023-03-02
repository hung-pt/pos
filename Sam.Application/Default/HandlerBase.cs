using Ddd.Application.Interfaces;

namespace Ddd.Application.Default;

public class HandlerBase {
    protected readonly IApplicationDbContext _context;
    public HandlerBase(IApplicationDbContext context) => _context = context;
}
