using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using LastMotoApp.Services;

namespace LastMotoApp;

public class App : IApp
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<BusinessPartner> _businessPartnerRepository;

    public App(IRepository<Employee> employeeRepository, IRepository<BusinessPartner> businessPartnerRepository)
    {
        _employeeRepository = employeeRepository;
        _businessPartnerRepository = businessPartnerRepository;
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
                case "0":
                    return;
                default:
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("INFO : Allow options 0 - 2");
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
            };
        } while (true);
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
}
