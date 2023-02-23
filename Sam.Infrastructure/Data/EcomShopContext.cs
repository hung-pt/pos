using Microsoft.EntityFrameworkCore;
using Sam.Application.Interfaces;
using Sam.Domain;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Sam.Infrastructure.Data;

public class EcomShopContext : DbContext, IApplicationDbContext {
    public DbSet<Office> Offices { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ProductLine> ProductLines { get; set; }
    public DbSet<Product> Products { get; set; }

    public EcomShopContext() { }

    public EcomShopContext(DbContextOptions<EcomShopContext> options) : base(options) {

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
        //modelBuilder.Entity<Office>().HasKey(office => office.OfficeCode);
        modelBuilder.Entity<Office>().HasMany(office => office.Staff).WithOne(emp => emp.Office);
        modelBuilder.Entity<Office>().HasData(Seed.Offices);

        //modelBuilder.Entity<Employee>().HasKey(emp => emp.EmployeeNumber);
        modelBuilder.Entity<Employee>().HasOne(emp => emp.Office).WithMany(office => office.Staff).HasForeignKey(emp => emp.OfficeCode);
        modelBuilder.Entity<Employee>().HasOne(emp => emp.ReportsTo).WithMany(emp => emp.Subordinates).HasForeignKey(emp => emp.ReportsToEmployeeNumber);
        modelBuilder.Entity<Employee>().HasData(Seed.Employees);

        modelBuilder.Entity<Customer>().HasKey(cus => cus.CustomerNumber);
        modelBuilder.Entity<Customer>().HasOne(cus => cus.SalesRep).WithMany().HasForeignKey(cus => cus.SalesRepEmployeeNumber);
        modelBuilder.Entity<Customer>().HasMany(cus => cus.Orders).WithOne(order => order.Customer);
        modelBuilder.Entity<Customer>().HasMany(cus => cus.Payments).WithOne(payment => payment.Customer);
        modelBuilder.Entity<Customer>().Property(cus => cus.CreditLimit).HasPrecision(18, 2);
        modelBuilder.Entity<Customer>().HasData(Seed.Customers);

        modelBuilder.Entity<Order>().HasKey(order => order.OrderNumber);
        modelBuilder.Entity<Order>().Property(order => order.OrderNumber).ValueGeneratedNever();
        modelBuilder.Entity<Order>().HasOne(order => order.Customer).WithMany(cus => cus.Orders).HasForeignKey(order => order.CustomerNumber);
        modelBuilder.Entity<Order>().HasData(Seed.Orders);

        modelBuilder.Entity<OrderDetail>().HasKey(detail => new { detail.OrderNumber, detail.ProductCode });
        modelBuilder.Entity<OrderDetail>().HasOne(detail => detail.Order).WithMany(order => order.OrderDetails).HasForeignKey(detail => detail.OrderNumber);
        modelBuilder.Entity<OrderDetail>().HasOne(detail => detail.Product).WithMany(product => product.OrderDetails).HasForeignKey(detail => detail.ProductCode);
        modelBuilder.Entity<OrderDetail>().Property(detail => detail.PriceEach).HasPrecision(18, 2);
        modelBuilder.Entity<OrderDetail>().HasData(Seed.OrderDetails);

        modelBuilder.Entity<Payment>().HasKey(pay => new { pay.CustomerNumber, pay.CheckNumber });
        modelBuilder.Entity<Payment>().HasOne(pay => pay.Customer).WithMany(cus => cus.Payments).HasForeignKey(pay => pay.CustomerNumber);
        modelBuilder.Entity<Payment>().Property(pay => pay.Amount).HasPrecision(18, 2);
        modelBuilder.Entity<Payment>().HasData(Seed.Payments);

        modelBuilder.Entity<ProductLine>().ToTable("ProductLines")
            //.HasKey(line => line.ProductLineCode)
            ;
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
