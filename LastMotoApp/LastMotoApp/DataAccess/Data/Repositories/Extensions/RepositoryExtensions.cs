using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;

namespace LastMotoApp.DataAccess.Data.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static void AddBatch<T>(this IRepository<T> repository, List<T> items) where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }
        repository.Save();
    }
}
