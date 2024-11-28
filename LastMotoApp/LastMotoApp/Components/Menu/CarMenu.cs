using LastMotoApp.Components.CsvReader;
using LastMotoApp.Components.XmlReader;
using LastMotoApp.Data.Entities;
using LastMotoApp.Data.Repositories;

namespace LastMotoApp.Components.Menu;

public class CarMenu : Menu<Car>, ICarMenu
{
    private readonly IRepository<Car> _carRepository;
    private readonly IMenu<Car> _menu;
    private readonly ICsvReader _csvReader;
    private readonly IXmlReader _xmlReader;

    public CarMenu(IRepository<Car> carRepository,
        IMenu<Car> menu,
        ICsvReader csvReader,
        IXmlReader xmlReader
        ) : base(carRepository)
    {
        _carRepository = carRepository;
        _menu = menu;
        _csvReader = csvReader;
        _xmlReader = xmlReader;
    }

    public void RunCarMenu()
    {
        do
        {
            Console.WriteLine("What do you want do?");
            Console.WriteLine("\t1 - Add cars to database from cars.csv file");
            Console.WriteLine("\t2 - Create cars.xml file from database");
            Console.WriteLine("\t3 - Remove car from database");
            Console.WriteLine("\t4 - Display car list from database");
            Console.WriteLine("\t0 - Return");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    AddCarsFromCsvFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    CreateCarsXmlFile();
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

    private void CreateCarsXmlFile()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        _xmlReader.CreateCarsXmlFileFromDatabase();
    }

    private void AddCarsFromCsvFile()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        var cars = _csvReader.ProcessCars("Resources/Files/fuel.csv");
        var counter = 0;
        foreach (var car in cars)
        {
            if (_carRepository.GetAll().Where(x => x.Name == car.Name
                && x.City == car.City
                && x.Highway == car.Highway
                && x.Combined == car.Combined).Any())
            {
                continue;
            }
            _carRepository.Add(new Car
            {
                Year = car.Year,
                Manufacturer = car.Manufacturer,
                Name = car.Name,
                Engine = car.Engine,
                Cylinders = car.Cylinders,
                City = car.City,
                Highway = car.Highway,
                Combined = car.Combined
            });
            _carRepository.Save();
            counter++;
        }
        Console.WriteLine($"\tINFO : {counter} car(s) added to database");
    }
}
