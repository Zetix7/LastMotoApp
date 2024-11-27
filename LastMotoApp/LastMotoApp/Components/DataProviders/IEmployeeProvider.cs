using LastMotoApp.Data.Entities;

namespace LastMotoApp.Components.DataProviders;

public interface IEmployeeProvider
{
    List<Employee> GenerateSampleEmployees();
}
