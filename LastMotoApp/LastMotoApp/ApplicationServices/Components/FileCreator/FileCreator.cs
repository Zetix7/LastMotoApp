using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;
using System.Text.Json;

namespace LastMotoApp.ApplicationServices.Components.FileCreator;

public class FileCreator<T> : IFileCreator<T> where T : class, IEntity
{
    private readonly string _auditFile = "audit.txt";
    private readonly string _filePath = ".txt";

    public FileCreator()
    {
        _auditFile = $"{IComponentHelper.DefaultPath}{typeof(T).Name.ToLower()}s_{_auditFile}";
        _filePath = $"{IComponentHelper.DefaultPath}{typeof(T).Name.ToLower()}s{_filePath}";
    }

    public void SaveToFile(IRepository<T> entities, T entity, string action)
    {
        if (typeof(T).Name.Equals("Employee") || typeof(T).Name.Equals("BusinessPartner"))
        {
            var list = new List<T>();
            foreach (var item in entities.GetAll())
            {
                list.Add(item);
            }
            using (var writer = File.CreateText(_filePath))
            {
                var jsonItem = JsonSerializer.Serialize(list);
                writer.WriteLine(jsonItem);
            }
        }

        if (!File.Exists(_auditFile))
        {
            Console.WriteLine($"\tINFO : {_auditFile} file created");
        }

        using (var writera = File.AppendText(_auditFile))
        {
            writera.WriteLine($"[{DateTime.Now}]-{typeof(T).Name}{action}-[{entity}]");
        }
    }

    public List<T> ReadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"\tINFO : Creating {_filePath} file");
            return [];
        }

        if (new FileInfo(_filePath).Length < 3)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"\tERROR : File {_filePath} is empty");
            return [];
        }

        List<T> items = [];
        using (var reader = File.OpenText(_filePath))
        {
            var text = reader.ReadToEnd();
            items = JsonSerializer.Deserialize<List<T>>(text)!;
        }

        return items;
    }
}
