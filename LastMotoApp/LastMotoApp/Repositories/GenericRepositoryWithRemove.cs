using LastMotoApp.Entities;

namespace LastMotoApp.Repositories;

public class GenericRepositoryWithRemove<T> : GenericRepository<T> where T : class, IEntity
{
    public void Remove(T item)
    {
        _items.Remove(item);
    }
}
