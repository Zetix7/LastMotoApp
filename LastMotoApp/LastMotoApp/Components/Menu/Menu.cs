using LastMotoApp.Data.Entities;
using LastMotoApp.Data.Repositories;

namespace LastMotoApp.Components.Menu;

public class Menu<T> : IMenu<T> where T : class, IEntity
{
    private readonly IRepository<T> _repository;

    public Menu(IRepository<T> repository)
    {
        _repository = repository;
        _repository.ItemAdded -= OnItemAdded;
        _repository.ItemRemoved -= OnItemRemoved;
        _repository.ItemAdded += OnItemAdded;
        _repository.ItemRemoved += OnItemRemoved;
    }

    public void RunRemoveItem()
    {

        if (!_repository.GetAll().Any())
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tINFO : File not load or File is empty");
            return;
        }

        Console.WriteLine($"Removing {typeof(T).Name}:");
        Console.WriteLine($"\t\tChoose Id of {typeof(T).Name}");

        DisplayItemList();

        Console.Write("\t\tYour choise: ");
        var input = Console.ReadLine()!.Trim();
        int id = -1;

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id can not be empty");
            return;
        }
        else if (!int.TryParse(input, out id))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id must be a number");
            return;
        }
        else if (!_repository.GetAll().Where(x => x.Id == id).Any())
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id not exists on list");
            return;
        }

        var item = _repository.GetById(id);
        _repository.Remove(item!);
        _repository.Save();
        var fileCreator = new FileCreator<T>();
        fileCreator.SaveToFile(_repository, item!, "Removed");
    }

    public virtual void DisplayItemList()
    {
        if (!_repository.GetAll().Any())
        {
            Console.WriteLine("\tINFO : File is empty");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"{typeof(T).Name} list in file");
        foreach (var item in _repository.GetAll().OrderBy(x => x.Id).ToList())
        {
            Console.WriteLine($"\t{item}");
        }
    }

    private static void OnItemAdded(object? sender, T item)
    {
        //Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : New {item} added");
    }

    private static void OnItemRemoved(object? sender, T item)
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : {item} removed");
    }
}
