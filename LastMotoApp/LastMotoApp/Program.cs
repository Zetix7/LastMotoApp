using LastMotoApp;
using LastMotoApp.Components;
using LastMotoApp.Data;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<BusinessPartner>, SqlRepository<BusinessPartner>>();
services.AddDbContext<MotoAppDbContext>();
services.AddSingleton<IRepository<Car>, SqlRepository<Car>>();
services.AddSingleton<ICarsProvider, CarsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
