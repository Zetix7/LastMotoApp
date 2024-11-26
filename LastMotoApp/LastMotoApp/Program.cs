using LastMotoApp;
using LastMotoApp.Data;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<BusinessPartner>, SqlRepository<BusinessPartner>>();
services.AddDbContext<MotoAppDbContext>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
