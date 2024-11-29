using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;

namespace LastMotoApp.ApplicationServices.Components.FileCreator;

public interface IFileCreator<T> where T : class, IEntity
{
    void SaveToFile(IRepository<T> entities, T entity, string action);
    List<T> ReadFromFile();
}
