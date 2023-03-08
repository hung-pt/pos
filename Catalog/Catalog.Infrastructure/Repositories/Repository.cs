using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public interface IRepository<T> where T : class {
    T GetById(object id);
    IEnumerable<T> GetAll();
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public class BaseRepository<T> : IRepository<T> where T : class {
    protected readonly DbContext _context; // is dbSet only enough?
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T GetById(object id) => _dbSet.Find(id);
    public IEnumerable<T> GetAll() => _dbSet.ToList();
    public void Insert(T entity) => _dbSet.Add(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
}

public interface IProductRepository : IRepository<Product> { // method nghiep vu
}

public class ProductRepository : BaseRepository<Product>, IProductRepository {
    public ProductRepository(DbContext context) : base(context) { }
    // method nghiep vu
}
