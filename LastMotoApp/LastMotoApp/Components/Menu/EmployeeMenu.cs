using LastMotoApp.Components.DataProviders;
using LastMotoApp.Data.Entities;
using LastMotoApp.Data.Repositories;

namespace LastMotoApp.Components.Menu;

public class EmployeeMenu : Menu<Employee>, IEmployeeMenu
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IFileCreator<Employee> _fileCreator;
    private readonly IMenu<Employee> _menu;
    private readonly IEmployeeProvider _employeeProvider;

    public EmployeeMenu(
        IRepository<Employee> employeeRepository,
        IFileCreator<Employee> fileCreator,
        IMenu<Employee> menu,
        IEmployeeProvider employeeProvider) : base(employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _fileCreator = fileCreator;
        _menu = menu;
        _employeeProvider = employeeProvider;
    }

    public void RunEmployeeMenu()
    {
        do
        {
            Console.WriteLine("What do you want do?");
            Console.WriteLine("\t1 - Add employee");
            Console.WriteLine("\t2 - Remove employee");
            Console.WriteLine("\t3 - Display employee list");
            Console.WriteLine("\t4 - Add sample employees to file");
            Console.WriteLine("\t0 - Return");
            Console.Write("\t\tYour choise: ");

            var input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                    AddEmployee();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "2":
                    _menu.RunRemoveItem();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "3":
                    DisplayItemList();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "4":
                    Console.WriteLine("-------------------------------------------------------------------");
                    AddSampleEmployees();
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

    private void AddSampleEmployees()
    {
        var employees = _employeeProvider.GenerateSampleEmployees();
        foreach (var employee in employees)
        {
            if (_employeeRepository.GetAll().Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Any())
            {
                continue;
            }
            _employeeRepository.Add(new Employee { FirstName = employee.FirstName, LastName = employee.LastName });
            _employeeRepository.Save();
            _fileCreator.SaveToFile(_employeeRepository, employee, "Added");
        }
    }

    public override void DisplayItemList()
    {
        LoadEmployeeListFromFile();
        base.DisplayItemList();
    }

    private void AddEmployee()
    {
        LoadEmployeeListFromFile();

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
        _fileCreator.SaveToFile(_employeeRepository, employee, "Added");
    }

    private void LoadEmployeeListFromFile()
    {
        var employeeList = _fileCreator.ReadFromFile();
        if (employeeList.Count > 0)
        {
            foreach (var employee in employeeList)
            {
                if (_employeeRepository.GetAll().Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Any())
                {
                    continue;
                }
                _employeeRepository.Add(new Employee { FirstName = employee.FirstName, LastName = employee.LastName });
            }
            _employeeRepository.Save();
        }
    }
}
