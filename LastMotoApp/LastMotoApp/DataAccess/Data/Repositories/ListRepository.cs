using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.DataAccess.Data.Repositories;

public class ListRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly List<T> _items;

    public ListRepository()
    {
        _items = [];
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public T? GetById(int id)
    {
        return _items.Single(item => item.Id == id);
    }

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public void Save()
    {
        foreach (var item in _items)
        {
            Console.WriteLine(item);
        }
    }
}
