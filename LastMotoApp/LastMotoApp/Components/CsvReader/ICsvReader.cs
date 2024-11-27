
using LastMotoApp.Components.CsvReader.Models;

namespace LastMotoApp.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCars(string filepath);
    List<Manufacturer> ProcessManufacturers(string filepath);
}
