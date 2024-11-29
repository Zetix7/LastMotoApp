using LastMotoApp.ApplicationServices.Components.FileCreator;
using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;

namespace LastMotoApp.UI.Menu;

public class Menu<T> : IMenu<T> where T : class, IEntity
{
    private readonly IRepository<T> _repository;
    protected const string _filePath = "DataAccess/Resources/Files/";

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
            Console.WriteLine("\tINFO : Database not load or is empty");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : Removing {typeof(T).Name}...");

        DisplayItemList();

        Console.Write($"\t\tChoose # of {typeof(T).Name}: ");
        var input = Console.ReadLine()!.Trim();
        var id = CheckId(input);
        if(id == -1)
        {
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
            Console.WriteLine("\tINFO : Database is empty");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"{typeof(T).Name} list in database");

        int counter = 0;
        foreach (var item in _repository.GetAll().OrderBy(x => x.Id).ToList())
        {
            Console.WriteLine($"\t#{item.Id} - {item}");
            counter++;
        }
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : {counter} {typeof(T).Name.ToLower()}(s) in database");
    }

    private int CheckId(string input)
    {
        int id = -1;

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id can not be empty");
        }
        else if (!int.TryParse(input, out id))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id must be a number");
        }
        else if (!_repository.GetAll().Where(x => x.Id == id).Any())
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id not exists on list");
        }
        return id;
    }

    private static void OnItemAdded(object? sender, T item)
    {
        Console.WriteLine($"\tINFO : New {item} added");
    }

    private static void OnItemRemoved(object? sender, T item)
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : {item} removed");
    }
}
