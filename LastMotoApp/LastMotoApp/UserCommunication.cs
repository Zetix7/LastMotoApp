using LastMotoApp.Components.CsvReader;
using LastMotoApp.Components.Menu;
using LastMotoApp.Components.XmlReader;

namespace LastMotoApp;

public class UserCommunication : IUserCommunication
{
    private readonly ICsvReader _csvReader;
    private readonly IXmlReader _xmlReader;
    private readonly IEmployeeMenu _employeeMenu;
    private readonly IBusinessPartnerMenu _businessPartnerMenu;
    private readonly ICarMenu _carMenu;
    private readonly IManufacturerMenu _manufacturerMenu;

    public UserCommunication(IEmployeeMenu employeeMenu,
        IBusinessPartnerMenu businessPartnerMenu,
        ICarMenu carMenu,
        IManufacturerMenu manufacturerMenu,
        ICsvReader csvReader,
        IXmlReader xmlReader)
    {
        _employeeMenu = employeeMenu;
        _businessPartnerMenu = businessPartnerMenu;
        _carMenu = carMenu;
        _manufacturerMenu = manufacturerMenu;
        _csvReader = csvReader;
        _xmlReader = xmlReader;
    }

    public void Run()
    {
        do
        {
            Console.WriteLine("Choose one of allow resources?");
            Console.WriteLine("\t1 - Employee");
            Console.WriteLine("\t2 - Business partner");
            Console.WriteLine("\t3 - Car");
            Console.WriteLine("\t4 - Manufacturer");
            Console.WriteLine("\t5 - Display Cars from csv file (GroupBy, Join)");
            Console.WriteLine("\t6 - Display Manufacturers from csv file (GroupJoin)");
            Console.WriteLine("\t7 - Create specificManufacturersCars.xml file");
            Console.WriteLine("\t0 - Exit");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    Console.WriteLine("-------------------------------------------------------------------");
                    _employeeMenu.RunEmployeeMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    Console.WriteLine("-------------------------------------------------------------------");
                    _businessPartnerMenu.RunBusinessPartnerMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "3":
                    Console.WriteLine("-------------------------------------------------------------------");
                    _carMenu.RunCarMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "4":
                    Console.WriteLine("-------------------------------------------------------------------");
                    _manufacturerMenu.RunManufacturerMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "5":
                    Console.WriteLine("-------------------------------------------------------------------");
                    DisplayCarsFromFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "6":
                    Console.WriteLine("-------------------------------------------------------------------");
                    DisplayManufacturersFromFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "7":
                    Console.WriteLine("-------------------------------------------------------------------");
                    CreateSpecificManufacturersCarsXmlFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("INFO : Allow options 0 - 9");
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
            };
        } while (true);
    }

    private void CreateSpecificManufacturersCarsXmlFile()
    {
        _xmlReader.CreateSpecificManufacturersCarsXmlFile();
    }

    private void DisplayCarsFromFile()
    {
        var cars = _csvReader.ProcessCars("Resources/Files/fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources/Files/manufacturers.csv");

        var groups = cars.GroupBy(x => x.Manufacturer).Select(g => new
        {
            Name = g.Key,
            Max = g.Max(x => x.Combined),
            Min = g.Min(x => x.Combined),
            Average = g.Average(x => x.Combined),
        });

        Console.WriteLine("----------------------GROUP-BY-------------------------------------");
        foreach (var car in groups)
        {
            Console.WriteLine($"Name: {car.Name}");
            Console.WriteLine($"\tMax: {car.Max}");
            Console.WriteLine($"\tMin: {car.Min}");
            Console.WriteLine($"\tAverage: {car.Average}");
        }

        var join = cars.Join(manufacturers,
            c => new { c.Manufacturer, c.Year },
            m => new { Manufacturer = m.Name, m.Year },
            (car, manufacturer) => new
            {
                manufacturer.Country,
                car.Name,
                car.Combined
            }).OrderBy(x => x.Country).ThenBy(x => x.Name);

        Console.WriteLine("----------------------JOIN-----------------------------------------");
        foreach (var car in join)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\tName: {car.Name}");
            Console.WriteLine($"\tCombined: {car.Combined}");
        }
    }

    private void DisplayManufacturersFromFile()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources/Files/manufacturers.csv");
        var cars = _csvReader.ProcessCars("Resources/Files/fuel.csv");

        var groupjoin = manufacturers.GroupJoin(cars,
            m => new { Manufacturer = m.Name, m.Year },
            c => new { c.Manufacturer, c.Year },
            (manufacturers, group) => new
            {
                Manufacturer = manufacturers,
                Cars = group
            });

        Console.WriteLine("----------------------GROUP-JOIN-----------------------------------");
        foreach (var group in groupjoin)
        {
            Console.WriteLine($"Manufacturer: {group.Manufacturer}");
            Console.WriteLine($"\tCars: {group.Cars.Count()}");
            Console.WriteLine($"\tMin: {group.Cars.Min(x => x.Combined)}");
            Console.WriteLine($"\tMax: {group.Cars.Max(x => x.Combined)}");
        }
    }
}
