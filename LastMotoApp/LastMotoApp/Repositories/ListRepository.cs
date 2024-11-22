using LastMotoApp.Entities;

namespace LastMotoApp.Repositories;

public class ListRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly List<T> _items;

    public ListRepository()
    {
        _items = [];
    }

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public T GetById(int id)
    {
        return _items.Single(item => item.Id == id);
    }

    public void Add(T items)
    {
        items.Id = _items.Count + 1;
        _items.Add(items);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public void Save()
    {
        foreach (var item in _items)
        {
            Console.WriteLine(item);
        }
    }
}
