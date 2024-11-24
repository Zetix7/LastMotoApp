using LastMotoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.Repositories;

public delegate void ItemAdded(object item);

public class SqlRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;
    private readonly ItemAdded? _callbackItemAdded;

    public SqlRepository(DbContext dbContext, ItemAdded callbackItemAdded = null!)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _callbackItemAdded = callbackItemAdded;
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Single(item => item.Id == id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        _callbackItemAdded?.Invoke(item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
