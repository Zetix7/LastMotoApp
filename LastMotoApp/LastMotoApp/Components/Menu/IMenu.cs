using LastMotoApp.Data.Entities;

namespace LastMotoApp.Components.Menu;

public interface IMenu<T> where T : class, IEntity
{
    void RunRemoveItem();
}
