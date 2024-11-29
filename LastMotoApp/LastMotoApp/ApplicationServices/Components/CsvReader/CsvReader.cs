using LastMotoApp.ApplicationServices.Components.CsvReader.Extensions;
using LastMotoApp.ApplicationServices.Components.CsvReader.Models;

namespace LastMotoApp.ApplicationServices.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<Car> ProcessCars(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"\tERROR: {filePath} not exist");
            return [];
        }
        else if (new FileInfo(filePath).Length == 0)
        {
            Console.WriteLine($"\tERROR: {filePath} is empty");
            return [];
        }

        var cars = new List<string>();
        using (var reader = File.OpenText(filePath))
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                cars.Add(line);
                line = reader.ReadLine();
            }
        }

        return cars.Skip(1).Where(x => x.Length > 1).ToCar().ToList();
    }

    public List<Manufacturer> ProcessManufacturers(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"\tERROR: {filePath} not exist");
            return [];
        }
        else if (new FileInfo(filePath).Length == 0)
        {
            Console.WriteLine($"\tERROR: {filePath} is empty");
            return [];
        }

        return File.ReadAllLines(filePath).Where(x => x.Length > 1).ToManufacturer().ToList();
    }
}
