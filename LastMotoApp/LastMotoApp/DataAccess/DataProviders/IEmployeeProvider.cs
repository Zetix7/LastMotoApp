using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.DataAccess.DataProviders;

public interface IEmployeeProvider
{
    List<Employee> GenerateSampleEmployees();
}
