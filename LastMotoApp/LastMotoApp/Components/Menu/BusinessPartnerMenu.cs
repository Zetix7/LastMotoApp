using LastMotoApp.Entities;
using LastMotoApp.Repositories;

namespace LastMotoApp.Components.Menu;

public class BusinessPartnerMenu : Menu<BusinessPartner>, IBusinessPartnerMenu
{
    private readonly IRepository<BusinessPartner> _businessPartnerRepository;
    private readonly IFileCreator<BusinessPartner> _fileCreator;
    private readonly IMenu<BusinessPartner> _menu;

    public BusinessPartnerMenu(
        IRepository<BusinessPartner> businessPartnerRepository,
        IFileCreator<BusinessPartner> fileCreator,
        IMenu<BusinessPartner> menu) : base(businessPartnerRepository)
    {
        _businessPartnerRepository = businessPartnerRepository;
        _fileCreator = fileCreator;
        _menu = menu;
    }

    public void RunBusinessPartnerMenu()
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
                    AddBusinesPartner();
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

    public override void DisplayItemList()
    {
        LoadBusinessPartnerListFromFile();
        base.DisplayItemList();
    }

    private void AddBusinesPartner()
    {
        var fileCreator = new FileCreator<BusinessPartner>();
        LoadBusinessPartnerListFromFile();

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

    private void LoadBusinessPartnerListFromFile()
    {
        var businessPartnerList = _fileCreator.ReadFromFile();
        if (businessPartnerList.Count > 0)
        {
            foreach (var businessPartner in businessPartnerList)
            {
                if (_businessPartnerRepository.GetAll().Where(x => x.Name == businessPartner.Name).Any())
                {
                    continue;
                }
                _businessPartnerRepository.Add(new BusinessPartner { Name = businessPartner.Name });
            }
            _businessPartnerRepository.Save();
        }
    }
}
