using LastMotoApp.Data;
using LastMotoApp.Entities;
using LastMotoApp.Repositories;

var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext());

AddEmployees(employeeRepository);
AddSupervisors(employeeRepository);
Display(employeeRepository);

static void AddEmployees(IRepository<Employee> repository){
    repository.Add(new Employee { FirstName = "Elizabeth", LastName = "Olsen" });
    repository.Add(new Employee { FirstName = "Scarlett", LastName = "Johansson" });
    repository.Add(new Employee { FirstName = "Ana", LastName = "de Armas" });
    repository.Save();
}

static void AddSupervisors(IWriteRepository<Supervisor> repository)
{
    repository.Add(new Supervisor { FirstName = "Gregory", LastName = "White" });
    repository.Add(new Supervisor { FirstName = "Chris", LastName = "Evans" });
    repository.Add(new Supervisor { FirstName = "Gal", LastName = "Gadot" });
    repository.Save();
}

static void Display(IReadRepository<IEntity> repository)
{
    foreach (var employee in repository.GetAll())
    {
        Console.WriteLine(employee);
    }
}
