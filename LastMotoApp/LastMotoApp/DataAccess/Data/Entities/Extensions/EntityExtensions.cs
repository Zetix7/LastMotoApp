using LastMotoApp.DataAccess.Data.Entities;
using System.Text.Json;

namespace LastMotoApp.DataAccess.Data.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T item) where T : class, IEntity
    {
        var json = JsonSerializer.Serialize(item);
        return JsonSerializer.Deserialize<T>(json);
    }
}
