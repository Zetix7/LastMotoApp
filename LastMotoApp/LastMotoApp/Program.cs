﻿using LastMotoApp.ApplicationServices.Components.CsvReader;
using LastMotoApp.ApplicationServices.Components.FileCreator;
using LastMotoApp.ApplicationServices.Components.XmlReader;
using LastMotoApp.DataAccess.Data;
using LastMotoApp.DataAccess.Data.Entities;
using LastMotoApp.DataAccess.Data.Repositories;
using LastMotoApp.DataAccess.DataProviders;
using LastMotoApp.UI;
using LastMotoApp.UI.Menu;
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
