using LastMotoApp.ApplicationServices.Components.CsvReader.Models;

namespace LastMotoApp.ApplicationServices.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCars(string filepath);
    List<Manufacturer> ProcessManufacturers(string filepath);
}
