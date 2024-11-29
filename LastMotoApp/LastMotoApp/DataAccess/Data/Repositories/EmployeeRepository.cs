using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.DataAccess.Data.Repositories;

public class EmployeeRepository : EntityBase, IRepository<Employee>
{
    private readonly List<Employee> _employees;

    public EmployeeRepository()
    {
        _employees = [];
    }

    public event EventHandler<Employee>? ItemAdded;
    public event EventHandler<Employee>? ItemRemoved;

    public IEnumerable<Employee> GetAll()
    {
        return _employees;
    }

    public Employee? GetById(int id)
    {
        return _employees.Single(item => item.Id == id);
    }

    public void Add(Employee employee)
    {
        employee.Id = _employees.Count + 1;
        _employees.Add(employee);
        ItemAdded?.Invoke(this, employee);
    }

    public void Remove(Employee employee)
    {
        _employees.Remove(employee);
        ItemRemoved?.Invoke(this, employee);
    }

    public void Save()
    {
        foreach (var employee in _employees)
        {
            Console.WriteLine(employee);
        }
    }
}
