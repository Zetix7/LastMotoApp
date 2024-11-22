using LastMotoApp.Entities;
using LastMotoApp.Repositories;

var employeeRepository = new GenericRepository<Employee>();
employeeRepository.Add(new Employee { FirstName = "Elizabeth", LastName = "Olsen" });
employeeRepository.Add(new Employee { FirstName = "Scarlett", LastName = "Johansson" });
employeeRepository.Add(new Employee { FirstName = "Ana", LastName = "de Armas" });

employeeRepository.Save();