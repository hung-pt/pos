using Catalog.Application.Interfaces;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext : DbContext, ICatalogDbContext {
    public DbSet<ProductLine> ProductLines { get; set; }
    public DbSet<Product> Products { get; set; }

    public CatalogDbContext() { }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) {

    }

    public IQueryable<object> GetDbSet(Type entityType) {
        MethodInfo setMethod = GetType().GetMethod(
            nameof(Set),
            BindingFlags.Public | BindingFlags.Instance,
            Array.Empty<Type>()
        )!;
        setMethod = setMethod.MakeGenericMethod(entityType);
        var res = (IQueryable<object>)setMethod.Invoke(this, null)!;
        return res;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ProductLine>().ToTable("ProductLines");
        modelBuilder.Entity<ProductLine>().HasMany(line => line.Products).WithOne(product => product.ProductLine);
        modelBuilder.Entity<ProductLine>().HasData(Seed.ProductLines);

        //modelBuilder.Entity<Product>().HasKey(product => product.ProductCode);
        modelBuilder.Entity<Product>().HasOne(product => product.ProductLine).WithMany(line => line.Products).HasForeignKey(product => product.ProductLineCode);
        modelBuilder.Entity<Product>().Property(product => product.QuantityInStock).IsConcurrencyToken();
        modelBuilder.Entity<Product>().Property(product => product.BuyPrice).IsConcurrencyToken().HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(product => product.MSRP).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().HasData(Seed.Products);

        base.OnModelCreating(modelBuilder);
    }
}
