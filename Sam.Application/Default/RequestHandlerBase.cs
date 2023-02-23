using Sam.Application.Interfaces;

namespace Sam.Application.Default; 

public class RequestHandlerBase  {
    protected readonly IApplicationDbContext _context;
    public RequestHandlerBase(IApplicationDbContext context) => _context = context;
}
