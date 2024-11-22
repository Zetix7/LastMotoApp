using LastMotoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.Repositories;

public class SqlRepository
{
    private readonly DbSet<Employee> _dbSet;
    private readonly DbContext _dbContext;

    public SqlRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Employee>();
    }

    public Employee GetById(int id)
    {
        return _dbSet.Single(item => item.Id == id);
    }

    public void Add(Employee item)
    {
        _dbSet.Add(item);
    }

    public void Remove(Employee item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
