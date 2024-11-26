using LastMotoApp.Entities;
using LastMotoApp.Repositories;

namespace LastMotoApp.Components.Menu;

public interface IMenu<T> where T : class, IEntity
{
    void RunRemoveItem();
}
