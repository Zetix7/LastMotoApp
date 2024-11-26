using LastMotoApp.Components;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using LastMotoApp.Services;
using System.Drawing;

namespace LastMotoApp;

public class App : IApp
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<BusinessPartner> _businessPartnerRepository;
    private readonly IRepository<Car> _carRepository;
    private readonly ICarsProvider _carProvider;

    public App(
        IRepository<Employee> employeeRepository,
        IRepository<BusinessPartner> businessPartnerRepository,
        IRepository<Car> carRepository,
        ICarsProvider carProvider)
    {
        _employeeRepository = employeeRepository;
        _businessPartnerRepository = businessPartnerRepository;
        _carRepository = carRepository;
        _carProvider = carProvider;
    }

    public void Run()
    {
        _employeeRepository.ItemAdded += OnItemAdded;
        _employeeRepository.ItemRemoved += OnItemRemoved;
        _businessPartnerRepository.ItemAdded += OnItemAdded;
        _businessPartnerRepository.ItemRemoved += OnItemRemoved;

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
                    EmployeeMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    Console.WriteLine("-------------------------------------------------------------------");
                    BusinessPartnerMenu();
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
        foreach(var car in cars300000)
        {
            Console.WriteLine($"\t{car}");
        }
        Console.WriteLine("-------------------------------------------------------------------");

        // UniqueColors
        var uniqueColors = _carProvider.UniqueColors();

        Console.WriteLine("\tINFO: Unique colors");
        foreach(var color in uniqueColors)
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
        foreach(var car in specificColumns)
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

    private void BusinessPartnerMenu()
    {
        do
        {
            Console.WriteLine("What do you want do?");
            Console.WriteLine("\t1 - Add business partner");
            Console.WriteLine("\t2 - Remove business partner");
            Console.WriteLine("\t3 - Display business partner list");
            Console.WriteLine("\t0 - Return");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    AddBusinesPartnerMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    RemoveMenu(_businessPartnerRepository);
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "3":
                    DisplayBusinesPartnerList();
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

    private void AddBusinesPartnerMenu()
    {
        var fileCreator = new FileCreator<BusinessPartner>();
        LoadBusinessPartnerListFromFile(_businessPartnerRepository, fileCreator);

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("\tAdding new business partner: ");
        Console.Write("\t\tEnter name: ");
        var name = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Name can not be empty");
            return;
        }

        var businessPartner = new BusinessPartner { Name = name };
        if (_businessPartnerRepository.GetAll().Where(x => x.Name == businessPartner.Name).Any())
        {
            return;
        }
        _businessPartnerRepository.Add(businessPartner);
        _businessPartnerRepository.Save();
        fileCreator.SaveToFile(_businessPartnerRepository, businessPartner, "Added");
    }

    private static void LoadBusinessPartnerListFromFile(IRepository<BusinessPartner> businessPartnerRepository, FileCreator<BusinessPartner> fileCreator)
    {
        var businessPartnerList = fileCreator.ReadFromFile();
        if (businessPartnerList.Count > 0)
        {
            foreach (var businessPartner in businessPartnerList)
            {
                if (businessPartnerRepository.GetAll().Where(x => x.Name == businessPartner.Name).Any())
                {
                    continue;
                }
                businessPartnerRepository.Add(new BusinessPartner { Name = businessPartner.Name });
            }
            businessPartnerRepository.Save();
        }
    }

    private void EmployeeMenu()
    {
        do
        {
            Console.WriteLine("What do you want do?");
            Console.WriteLine("\t1 - Add employee");
            Console.WriteLine("\t2 - Remove employee");
            Console.WriteLine("\t3 - Display employee list");
            Console.WriteLine("\t0 - Return");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    AddEmployeeMenu();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    RemoveMenu(_employeeRepository);
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "3":
                    DisplayEmployeeList();
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

    private void RemoveMenu<T>(IRepository<T> repository) where T : class, IEntity
    {

        if (!repository.GetAll().Any())
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tINFO : File not load or File is empty");
            return;
        }

        Console.WriteLine($"Removing {typeof(T).Name}:");
        Console.WriteLine($"\t\tChoose Id of {typeof(T).Name}");

        if (typeof(T).Name.Equals("Employee"))
        {
            DisplayEmployeeList();
        }
        else
        {
            DisplayBusinesPartnerList();
        }

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
        else if (!repository.GetAll().Where(x => x.Id == id).Any())
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Id not exists on list");
            return;
        }

        if (typeof(T).Name.Equals("Employee"))
        {
            var employee = _employeeRepository.GetById(id);
            _employeeRepository.Remove(employee!);
            _employeeRepository.Save();
            var fileCreator = new FileCreator<Employee>();
            fileCreator.SaveToFile(_employeeRepository, employee!, "Removed");
        }
        else
        {
            var businessPartner = _businessPartnerRepository.GetById(id);
            _businessPartnerRepository.Remove(businessPartner!);
            _businessPartnerRepository.Save();
            var fileCreator = new FileCreator<BusinessPartner>();
            fileCreator.SaveToFile(_businessPartnerRepository, businessPartner!, "Removed");
        }
    }

    private void DisplayBusinesPartnerList()
    {
        var fileCreator = new FileCreator<BusinessPartner>();
        LoadBusinessPartnerListFromFile(_businessPartnerRepository, fileCreator);

        if (!_businessPartnerRepository.GetAll().Any())
        {
            Console.WriteLine("\tINFO : File is empty");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("Business partner list in file");
        foreach (var businessPartner in _businessPartnerRepository.GetAll())
        {
            Console.WriteLine($"\t{businessPartner}");
        }
    }

    private void DisplayEmployeeList()
    {
        var fileCreator = new FileCreator<Employee>();
        LoadEmployeeListFromFile(_employeeRepository, fileCreator);

        if (!_employeeRepository.GetAll().Any())
        {
            Console.WriteLine("\tINFO : File is empty");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("Employee list in file");
        foreach (var employee in _employeeRepository.GetAll())
        {
            Console.WriteLine($"\t{employee}");
        }
    }

    private void AddEmployeeMenu()
    {
        var fileCreator = new FileCreator<Employee>();
        LoadEmployeeListFromFile(_employeeRepository, fileCreator);

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("\tAdding new employee: ");
        Console.Write("\t\tEnter first name: ");
        var firstName = Console.ReadLine()!.Trim();
        Console.Write("\t\tEnter last name: ");
        var lastName = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(firstName))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : First name can not be empty");
            return;
        }
        else if (string.IsNullOrEmpty(lastName))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("\tERROR : Last name can not be empty");
            return;
        }

        var employee = new Employee { FirstName = firstName, LastName = lastName };
        if (_employeeRepository.GetAll().Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Any())
        {
            return;
        }
        _employeeRepository.Add(employee);
        _employeeRepository.Save();
        fileCreator.SaveToFile(_employeeRepository, employee, "Added");
    }

    private static void LoadEmployeeListFromFile(IRepository<Employee> employeeRepository, FileCreator<Employee> fileCreator)
    {
        var employeeList = fileCreator.ReadFromFile();
        if (employeeList.Count > 0)
        {
            foreach (var employee in employeeList)
            {
                if (employeeRepository.GetAll().Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Any())
                {
                    continue;
                }
                employeeRepository.Add(new Employee { FirstName = employee.FirstName, LastName = employee.LastName });
            }
            employeeRepository.Save();
        }
    }

    private static void OnItemAdded<T>(object? sender, T item)
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : New {item} added");
    }

    private static void OnItemRemoved<T>(object? sender, T item)
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"\tINFO : {item} removed");
    }

    public List<Car> GenerateSampleCars()
    {
        return new List<Car>(){
            new Car { Id = 100, Name = "Audi A1", Color="White", Price=100000, Type="Compact" },
            new Car { Id = 200, Name = "Audi A2", Color="Blue", Price=150000, Type="Compact" },
            new Car { Id = 300, Name = "Audi A3", Color="Black", Price=200000, Type="Sportback" },
            new Car { Id = 400, Name = "Audi A4", Color="Silver", Price=250000, Type="Avant" },
            new Car { Id = 500, Name = "Audi A5", Color="Red", Price=300000, Type="Liftback" },
            new Car { Id = 600, Name = "Audi A6", Color="Green", Price=400000, Type="Limusine" },
            new Car { Id = 700, Name = "Audi A7", Color="Pink", Price=500000, Type="Liftback" },
            new Car { Id = 800, Name = "Audi A8", Color="Orange", Price=650000, Type="Limusine" },
            new Car { Id = 900, Name = "Audi TT", Color="Silver", Price=230000, Type="Coupe" },

            new Car { Id = 101, Name = "Audi S1", Color="Blue", Price=120000, Type="Compact" },
            new Car { Id = 201, Name = "Audi S2", Color="Green", Price=180000, Type="Compact" },
            new Car { Id = 301, Name = "Audi S3", Color="Pink", Price=250000, Type="Limusine" },
            new Car { Id = 401, Name = "Audi S4", Color="Yellow", Price=300000, Type="Avant" },
            new Car { Id = 501, Name = "Audi S5", Color="Red", Price=350000, Type="Liftback" },
            new Car { Id = 601, Name = "Audi S6", Color="Black", Price=450000, Type="Avant" },
            new Car { Id = 701, Name = "Audi S7", Color="Green", Price=550000, Type="Liftback" },
            new Car { Id = 801, Name = "Audi S8", Color="Silver", Price=690000, Type="Limusine" },
            new Car { Id = 901, Name = "Audi TTS", Color="Red", Price=280000, Type="Coupe" },

            new Car { Id = 302, Name = "Audi RS3", Color="Pink", Price=300000, Type="Limusine" },
            new Car { Id = 402, Name = "Audi RS4", Color="Yellow", Price=400000, Type="Avant" },
            new Car { Id = 502, Name = "Audi RS5", Color="Red", Price=500000, Type="Liftback" },
            new Car { Id = 602, Name = "Audi RS6", Color="Black", Price=620000, Type="Avant" },
            new Car { Id = 702, Name = "Audi RS7", Color="Green", Price=740000, Type="Liftback" },
            new Car { Id = 802, Name = "Audi R8", Color="Silver", Price=870000, Type="Coupe" },
            new Car { Id = 902, Name = "Audi TTRS", Color="Black", Price=330000, Type="Coupe" },
        };
    }
}
