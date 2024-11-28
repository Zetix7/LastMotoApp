using LastMotoApp;
using LastMotoApp.Components;
using LastMotoApp.Components.CsvReader;
using LastMotoApp.Components.DataProviders;
using LastMotoApp.Components.Menu;
using LastMotoApp.Components.XmlReader;
using LastMotoApp.Data;
using LastMotoApp.Data.Entities;
using LastMotoApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IMenu<Employee>, Menu<Employee>>();
services.AddSingleton<IMenu<BusinessPartner>, Menu<BusinessPartner>>();
services.AddSingleton<IMenu<Car>, Menu<Car>>();
services.AddSingleton<IMenu<Manufacturer>, Menu<Manufacturer>>();
services.AddSingleton<IEmployeeMenu, EmployeeMenu>();
services.AddSingleton<IBusinessPartnerMenu, BusinessPartnerMenu>();
services.AddSingleton<ICarMenu, CarMenu>();
services.AddSingleton<IManufacturerMenu, ManufacturerMenu>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<BusinessPartner>, SqlRepository<BusinessPartner>>();
services.AddSingleton<IRepository<Car>, SqlRepository<Car>>();
services.AddSingleton<IRepository<Manufacturer>, SqlRepository<Manufacturer>>();
services.AddDbContext<MotoAppDbContext>();
services.AddSingleton<IFileCreator<Employee>, FileCreator<Employee>>();
services.AddSingleton<IFileCreator<BusinessPartner>, FileCreator<BusinessPartner>>();
services.AddSingleton<IFileCreator<Car>, FileCreator<Car>>();
services.AddSingleton<IFileCreator<Manufacturer>, FileCreator<Manufacturer>>();
services.AddSingleton<IEmployeeProvider, EmployeeProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlReader, XmlReader>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

// Data Source=.\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True;Encrypt=True;Trust Server Certificate=True