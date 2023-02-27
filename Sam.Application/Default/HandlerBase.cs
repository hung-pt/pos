using Sam.Application.Interfaces;

namespace Sam.Application.Default;

public class HandlerBase {
    protected readonly IApplicationDbContext _context;
    public HandlerBase(IApplicationDbContext context) => _context = context;
}
