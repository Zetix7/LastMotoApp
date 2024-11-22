using LastMotoApp.Data;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;

//var employeeRepository = new GenericRepository<Employee>();
//employeeRepository.Add(new Employee { FirstName = "Elizabeth", LastName = "Olsen" });
//employeeRepository.Add(new Employee { FirstName = "Scarlett", LastName = "Johansson" });
//employeeRepository.Add(new Employee { FirstName = "Ana", LastName = "de Armas" });
//employeeRepository.Save();

var sqlRepository = new SqlRepository(new MotoAppDbContext());
sqlRepository.Add(new Employee { FirstName = "Elizabeth", LastName = "Olsen" });
sqlRepository.Add(new Employee { FirstName = "Scarlett", LastName = "Johansson" });
sqlRepository.Add(new Employee { FirstName = "Ana", LastName = "de Armas" });
sqlRepository.Save();
var e1 = sqlRepository.GetById(1);
var e2 = sqlRepository.GetById(2);
var e3 = sqlRepository.GetById(3);

Console.WriteLine(e1);
Console.WriteLine(e2);
Console.WriteLine(e3);