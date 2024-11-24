using LastMotoApp.Data;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using LastMotoApp.Repositories.Extensions;

var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext());

AddEmployees(employeeRepository);
Display(employeeRepository);

static void AddEmployees(IRepository<Employee> repository)
{
    var employees = new[]
    {
        new Employee { FirstName = "Elizabeth", LastName = "Olsen" },
        new Employee { FirstName = "Scarlett", LastName = "Johansson" },
        new Employee { FirstName = "Ana", LastName = "de Armas" }
    };

    repository.AddBatch(employees);
}

static void Display(IReadRepository<IEntity> repository)
{
    foreach (var item in repository.GetAll())
    {
        Console.WriteLine(item);
    }
}
