using LastMotoApp.ApplicationServices.Components.CsvReader;
using LastMotoApp.ApplicationServices.Components.FileCreator;
using LastMotoApp.ApplicationServices.Components.XmlReader;
using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;

namespace LastMotoApp.UI.Menu;

public class ManufacturerMenu : Menu<Manufacturer>, IManufacturerMenu
{
    private readonly IRepository<Manufacturer> _manufacturerRepository;
    private readonly IMenu<Manufacturer> _menu;
    private readonly ICsvReader _csvReader;
    private readonly IXmlReader _xmlReader;
    private readonly IFileCreator<Manufacturer> _fileCreator;

    public ManufacturerMenu(IRepository<Manufacturer> manufacturerRepository,
        IMenu<Manufacturer> menu,
        ICsvReader csvReader,
        IXmlReader xmlReader,
        IFileCreator<Manufacturer> fileCreator
        ) : base(manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
        _menu = menu;
        _csvReader = csvReader;
        _xmlReader = xmlReader;
        _fileCreator = fileCreator;
    }

    public void RunManufacturerMenu()
    {
        do
        {
            Console.WriteLine("What do you want do?");
            Console.WriteLine("\t1 - Add manufacturers to database from manufacturers.csv file");
            Console.WriteLine("\t2 - Create manufacturers.xml file from database");
            Console.WriteLine("\t3 - Remove manufacturer from database");
            Console.WriteLine("\t4 - Display manufacturer list from database");
            Console.WriteLine("\t0 - Return");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    AddManufacturersFromCsvFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    CreateManufacturersXmlFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "3":
                    _menu.RunRemoveItem();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "4":
                    DisplayItemList();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("INFO : Allow options 0 - 4");
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
            };
        } while (true);
    }

    private void AddManufacturersFromCsvFile()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        var manufacturers = _csvReader.ProcessManufacturers($"{_filePath}manufacturers.csv");
        var counter = 0;
        foreach (var manufacturer in manufacturers)
        {
            if (_manufacturerRepository.GetAll().Where(x => x.Name == manufacturer.Name).Any())
            {
                continue;
            }

            var entityManufacturer = new Manufacturer
            {
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Year = manufacturer.Year
            };

            _manufacturerRepository.Add(entityManufacturer);
            _manufacturerRepository.Save();
            _fileCreator.SaveToFile(_manufacturerRepository, entityManufacturer, "Added");
            counter++;
        }
        Console.WriteLine($"\tINFO : {counter} manufacturer(s) added to database");
    }

    private void CreateManufacturersXmlFile()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        _xmlReader.CreateManufacturersXmlFileFromDatabase();
    }
}
