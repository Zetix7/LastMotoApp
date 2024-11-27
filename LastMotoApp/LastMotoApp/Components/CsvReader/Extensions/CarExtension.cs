using LastMotoApp.Components.CsvReader.Models;
using System.Globalization;
using System.Xml.Linq;

namespace LastMotoApp.Components.CsvReader.Extensions;

public static class CarExtension
{
    public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var column = line.Split(',');

            yield return new Car()
            {
                Year = int.Parse(column[0]),
                Manufacturer = column[1],
                Name = column[2],
                Engine = double.Parse(column[3], CultureInfo.InvariantCulture),
                Cylinders = int.Parse(column[4]),
                City = int.Parse(column[5]),
                Highway = int.Parse(column[6]),
                Combined = int.Parse(column[7])
            };
        }
    }

    public static IEnumerable<Manufacturer> ToManufacturer(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var column = line.Split(",");

            yield return new Manufacturer()
            {
                Name = column[0],
                Country = column[2],
                Year = int.Parse(column[2])
            };
        }
    }
}
