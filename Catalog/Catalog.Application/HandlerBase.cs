using Catalog.Application.Interfaces;

namespace Catalog.Application;

public class HandlerBase
{
    protected readonly ICatalogDbContext _context;
    public HandlerBase(ICatalogDbContext context) => _context = context;
}
