using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.DataAccess.Data.Repositories;

public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
}
