using LastMotoApp.Entities;

namespace LastMotoApp.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> 
    where T : class, IEntity
{
}
