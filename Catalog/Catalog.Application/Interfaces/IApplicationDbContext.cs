using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Interfaces;

public interface ICatalogDbContext {
    DbSet<ProductLine> ProductLines { get; set; }
    DbSet<Product> Products { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
