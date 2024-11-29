using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.UI.Menu;

public interface IMenu<T> where T : class, IEntity
{
    void RunRemoveItem();
}
