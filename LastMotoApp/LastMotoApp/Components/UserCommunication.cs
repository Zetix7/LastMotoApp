using LastMotoApp.Components.Menu;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;

namespace LastMotoApp.Components;

public class UserCommunication : IUserCommunication
{
    private readonly IRepository<Car> _carRepository;
    private readonly ICarsProvider _carProvider;
    private readonly IEmployeeMenu _employeeMenu;
    private readonly IBusinessPartnerMenu _businessPartnerMenu;

    public UserCommunication(IEmployeeMenu employeeMenu, IBusinessPartnerMenu businessPartnerMenu,
        IRepository<Car> carRepository,
        ICarsProvider carProvider)
    {
        _employeeMenu = employeeMenu;
        _businessPartnerMenu = businessPartnerMenu;
        _carRepository = carRepository;
        _carProvider = carProvider;
    }

    public void Run()
    {
        do
        {
            Console.WriteLine("Choose one of allow resources?");
            Console.WriteLine("\t1 - Employee");
            Console.WriteLine("\t2 - Business partner");
            Console.WriteLine("\t3 - Car");
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
                    CarMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("INFO : Allow options 0 - 3");
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
            };
        } while (true);
    }

    private void CarMenu()
    {
        if (!_carRepository.GetAll().Any())
        {
            var cars = GenerateSampleCars();
            foreach (var car in cars)
            {
                _carRepository.Add(car);
            }
            _carRepository.Save();
        }

        // chepestPrice
        var chepestPrice = _carProvider.CheapestPrice();
        Console.WriteLine($"\tINFO : Cheapest price: {chepestPrice}");
        Console.WriteLine("-------------------------------------------------------------------");

        // PriceGreaterThan 300.000
        var cars300000 = _carProvider.PriceGreaterThan(300000);

        Console.WriteLine("\tINFO : Cars greater price than 300000");
        foreach (var car in cars300000)
        {
            Console.WriteLine($"\t{car}");
        }
        Console.WriteLine("-------------------------------------------------------------------");

        // UniqueColors
        var uniqueColors = _carProvider.UniqueColors();

        Console.WriteLine("\tINFO: Unique colors");
        foreach (var color in uniqueColors)
        {
            Console.WriteLine($"\t\t{color}");
        }
        Console.WriteLine("-------------------------------------------------------------------");

        // AnonymousClass
        var anonymousClass = _carProvider.AnonymousClass();

        Console.WriteLine("\tINFO : Anonymous class");
        Console.WriteLine(anonymousClass);
        Console.WriteLine("-------------------------------------------------------------------");

        // GetSpecificColumns
        var specificColumns = _carProvider.GetSpecificColumns();

        Console.WriteLine("\tINFO : Specific columns");
        foreach (var car in specificColumns)
        {
            Console.WriteLine($"\t{car}");
        }
        Console.WriteLine("-------------------------------------------------------------------");

        // TakeCars(range)
        var takeCarsRange = _carProvider.TakeCars(2..9);

        Console.WriteLine("\tINFO : TakeCars(range)");
        foreach (var car in takeCarsRange)
        {
            Console.WriteLine($"\t{car}");
        }
        Console.WriteLine("-------------------------------------------------------------------");

        // ChunkCars
        Console.WriteLine("\tINFO : ChunkCars");
        var chunkCars = _carProvider.ChunkCars(6);
        foreach (var cars in chunkCars)
        {
            Console.WriteLine();
            foreach (var car in cars)
            {
                Console.WriteLine($"\t{car}");
            }
        }
    }

    public List<Car> GenerateSampleCars()
    {
        return new List<Car>(){
            new() { Id = 100, Name = "Audi A1", Color="White", Price=100000, Type="Compact" },
            new() { Id = 200, Name = "Audi A2", Color="Blue", Price=150000, Type="Compact" },
            new() { Id = 300, Name = "Audi A3", Color="Black", Price=200000, Type="Sportback" },
            new() { Id = 400, Name = "Audi A4", Color="Silver", Price=250000, Type="Avant" },
            new() { Id = 500, Name = "Audi A5", Color="Red", Price=300000, Type="Liftback" },
            new() { Id = 600, Name = "Audi A6", Color="Green", Price=400000, Type="Limusine" },
            new() { Id = 700, Name = "Audi A7", Color="Pink", Price=500000, Type="Liftback" },
            new() { Id = 800, Name = "Audi A8", Color="Orange", Price=650000, Type="Limusine" },
            new() { Id = 900, Name = "Audi TT", Color="Silver", Price=230000, Type="Coupe" },

            new() { Id = 101, Name = "Audi S1", Color="Blue", Price=120000, Type="Compact" },
            new() { Id = 201, Name = "Audi S2", Color="Green", Price=180000, Type="Compact" },
            new() { Id = 301, Name = "Audi S3", Color="Pink", Price=250000, Type="Limusine" },
            new() { Id = 401, Name = "Audi S4", Color="Yellow", Price=300000, Type="Avant" },
            new() { Id = 501, Name = "Audi S5", Color="Red", Price=350000, Type="Liftback" },
            new() { Id = 601, Name = "Audi S6", Color="Black", Price=450000, Type="Avant" },
            new() { Id = 701, Name = "Audi S7", Color="Green", Price=550000, Type="Liftback" },
            new() { Id = 801, Name = "Audi S8", Color="Silver", Price=690000, Type="Limusine" },
            new() { Id = 901, Name = "Audi TTS", Color="Red", Price=280000, Type="Coupe" },

            new() { Id = 302, Name = "Audi RS3", Color="Pink", Price=300000, Type="Limusine" },
            new() { Id = 402, Name = "Audi RS4", Color="Yellow", Price=400000, Type="Avant" },
            new() { Id = 502, Name = "Audi RS5", Color="Red", Price=500000, Type="Liftback" },
            new() { Id = 602, Name = "Audi RS6", Color="Black", Price=620000, Type="Avant" },
            new() { Id = 702, Name = "Audi RS7", Color="Green", Price=740000, Type="Liftback" },
            new() { Id = 802, Name = "Audi R8", Color="Silver", Price=870000, Type="Coupe" },
            new() { Id = 902, Name = "Audi TTRS", Color="Black", Price=330000, Type="Coupe" },
        };
    }
}
