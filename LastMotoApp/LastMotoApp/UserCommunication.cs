using LastMotoApp.Components.CsvReader;
using LastMotoApp.Components.DataProviders;
using LastMotoApp.Components.Menu;
using LastMotoApp.Data.Entities;
using LastMotoApp.Data.Repositories;

namespace LastMotoApp;

public class UserCommunication : IUserCommunication
{
    private readonly ICsvReader _csvReader;
    private readonly IEmployeeMenu _employeeMenu;
    private readonly IBusinessPartnerMenu _businessPartnerMenu;

    public UserCommunication(IEmployeeMenu employeeMenu,
        IBusinessPartnerMenu businessPartnerMenu,
        ICsvReader csvReader)
    {
        _employeeMenu = employeeMenu;
        _businessPartnerMenu = businessPartnerMenu;
        _csvReader = csvReader;
    }

    public void Run()
    {
        do
        {
            Console.WriteLine("Choose one of allow resources?");
            Console.WriteLine("\t1 - Employee");
            Console.WriteLine("\t2 - Business partner");
            Console.WriteLine("\t3 - Car");
            Console.WriteLine("\t4 - Display Cars from file");
            Console.WriteLine("\t5 - Display Manufacturers from file");
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
                    DisplayCarsFromFile();
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                case "4":
                    Console.WriteLine("-------------------------------------------------------------------");
                    DisplayManufacturersFromFile();
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

    private void DisplayManufacturersFromFile()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources/Files/manufacturers.csv");
        foreach (var manufacturer in manufacturers)
        {
            Console.WriteLine(manufacturer);
        }
    }

    private void DisplayCarsFromFile()
    {
        var cars = _csvReader.ProcessCars("Resources/Files/fuel.csv");
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
