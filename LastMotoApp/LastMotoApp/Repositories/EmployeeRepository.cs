using LastMotoApp.Entities;

namespace LastMotoApp.Repositories;

public class EmployeeRepository
{
    private List<Employee> _employees;

    public EmployeeRepository()
    {
        _employees = new List<Employee>();
    }

    public Employee GetById(int id)
    {
        return _employees.Single(item => item.Id == id);
    }

    public void Add(Employee employee)
    {
        employee.Id = _employees.Count + 1;
        _employees.Add(employee);
    }

    public void Save()
    {
        foreach (var employee in _employees)
        {
            Console.WriteLine(employee);
        }
}
