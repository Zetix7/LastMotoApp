using LastMotoApp.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.DataAccess.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;

    public SqlRepository(MotoAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.SingleOrDefault(item => item.Id == id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
