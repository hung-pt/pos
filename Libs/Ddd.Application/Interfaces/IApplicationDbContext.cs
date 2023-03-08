using Ddd.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Application.Interfaces;

public interface IApplicationDbContext {
    DbSet<Office> Offices { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<ProductLine> ProductLines { get; set; }
    DbSet<Product> Products { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    IQueryable<object> GetDbSet(Type entityType);
}
