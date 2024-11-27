using LastMotoApp.Data.Entities;

namespace LastMotoApp.Data.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    event EventHandler<T>? ItemAdded;
    event EventHandler<T>? ItemRemoved;
}
