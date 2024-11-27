using LastMotoApp.Components.CsvReader.Models;
using LastMotoApp.Components.CsvReader.Extensions;

namespace LastMotoApp.Components.CsvReader;

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
            return new List<Manufacturer>();
        }
        else if (new FileInfo(filePath).Length == 0)
        {
            Console.WriteLine($"\tERROR: {filePath} is empty");
            return new List<Manufacturer>();
        }

        var manufacturers = File.ReadAllLines(filePath)
            .Where(x => x.Length > 1)
            .Select(x => new Manufacturer
            {
                Name = x.Split(',')[0],
                Country = x.Split(',')[1],
                Year = int.Parse(x.Split(',')[2])
            })
            .ToList();

        return manufacturers;
    }
}
