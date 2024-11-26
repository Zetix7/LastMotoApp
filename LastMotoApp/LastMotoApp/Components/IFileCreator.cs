using LastMotoApp.Entities;
using LastMotoApp.Repositories;

namespace LastMotoApp.Components;

public interface IFileCreator<T> where T : class, IEntity
{
    void SaveToFile(IRepository<T> entities, T entity, string action);
    List<T> ReadFromFile();
}
