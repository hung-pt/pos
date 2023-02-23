using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sam.Infrastructure.Others;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IOrderDetailRepository OrderDetailRepository { get; }
    IProductRepository ProductRepository { get; }

    void CreateTransaction();
    void Commit();
    void Rollback();
    void Save();
}

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private DbContext _context { get; }

    public IOrderRepository OrderRepository { get; }
    public IOrderDetailRepository OrderDetailRepository { get; }
    public IProductRepository ProductRepository { get; }
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(DbContext context)
    {
        _context = context;
        OrderRepository = new OrderRepository(context);
        OrderDetailRepository = new OrderDetailRepository(context);
        ProductRepository = new ProductRepository(context);
    }

    // co can ko
    public void CreateTransaction() => _transaction = _context.Database.BeginTransaction();
    public void Commit() => _transaction?.Commit();
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }
    public void Save() => _context.SaveChanges();
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();
        _disposed = true;
    }
}
