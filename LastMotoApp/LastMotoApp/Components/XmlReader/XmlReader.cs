using LastMotoApp.Components.CsvReader;
using LastMotoApp.Components.CsvReader.Models;
using LastMotoApp.Data.Repositories;
using System.Globalization;
using System.Xml.Linq;

namespace LastMotoApp.Components.XmlReader;

public class XmlReader : IXmlReader
{
    private readonly ICsvReader _csvReader;
    private readonly IRepository<Data.Entities.Car> _carRepository;
    private readonly IRepository<Data.Entities.Manufacturer> _manufacturerRepository;

    public XmlReader(ICsvReader csvReader, IRepository<Data.Entities.Car> carRepository, IRepository<Data.Entities.Manufacturer> manufacturerRepository)
    {
        _csvReader = csvReader;
        _carRepository = carRepository;
        _manufacturerRepository = manufacturerRepository;
    }

    public void CreateCarsXmlFileFromCsvFile(string fileName)
    {
        var cars = _csvReader.ProcessCars(fileName);

        var xmlCars = new XElement("Cars", cars.Select(x =>
            new XElement("Car",
                new XAttribute("Year", x.Year),
                new XAttribute("Manufacturer", x.Manufacturer!),
                new XAttribute("Name", x.Name!),
                new XAttribute("Engine", x.Engine),
                new XAttribute("Cylinders", x.Cylinders),
                new XAttribute("City", x.City),
                new XAttribute("Highway", x.Highway),
                new XAttribute("Combined", x.Combined)
                )));

        var xml = new XDocument();
        xml.Add(xmlCars);
        xml.Save(@"Resources\Files\cars.xml");

        Console.WriteLine("\tINFO : Created cars.xml file");
    }

    public void CreateManufacturersXmlFileFromCsvFile(string fileName)
    {
        var manufacturers = _csvReader.ProcessManufacturers(fileName);

        var xmlManufacturers = new XElement("Manufacturers", manufacturers.Select(x =>
            new XElement("Manufacturer",
                new XAttribute("Name", x.Name!),
                new XAttribute("Country", x.Country!),
                new XAttribute("Year", x.Year)
                )));

        var xml = new XDocument(xmlManufacturers);
        xml.Save(@"Resources\Files\manufacturers.xml");

        Console.WriteLine("\tINFO : Created manufacturers.xml file");
    }

    public List<Car> ReadCarsXmlFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"\tERROR : {fileName} file not exist");
            return [];
        }

        var carsXml = XDocument.Load(fileName);
        var cars = carsXml.Element("Cars")!.Elements("Car").Select(x => new Car
        {
            Year = int.Parse(x.Attribute("Year")!.Value),
            Manufacturer = x.Attribute("Manufacturer")!.Value,
            Name = x.Attribute("Name")?.Value,
            Engine = double.Parse(x.Attribute("Engine")!.Value, CultureInfo.InvariantCulture),
            Cylinders = int.Parse(x.Attribute("Cylinders")!.Value),
            City = int.Parse(x.Attribute("City")!.Value),
            Highway = int.Parse(x.Attribute("Highway")!.Value),
            Combined = int.Parse(x.Attribute("Combined")!.Value)
        });

        return cars.ToList();
    }

    public List<Manufacturer> ReadManufacturersXmlFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"\tERROR : {fileName} file not exist");
            return [];
        }

        var manufacturersXml = XDocument.Load(fileName);

        var manufacturers = manufacturersXml.Element("Manufacturers")?.Elements("Manufacturer")?.Select(x => new Manufacturer
        {
            Name = x.Attribute("Name")?.Value,
            Country = x.Attribute("Country")?.Value,
            Year = int.Parse(x.Attribute("Year")!.Value)
        });

        return manufacturers!.ToList();
    }

    public void CreateSpecificManufacturersCarsXmlFile()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources/Files/manufacturers.csv");
        var cars = _csvReader.ProcessCars("Resources/Files/fuel.csv");

        var groupJoin = manufacturers.GroupJoin(cars,
            m => new { Manufacturer = m.Name, m.Year },
            c => new { c.Manufacturer, c.Year },
            (manufacturer, group) => new
            {
                Manufacturer = manufacturer,
                Cars = group
            }).ToList();

        var xml = new XElement("Manufacturers", groupJoin.Select(x =>
            new XElement("Manufacturer",
                new XAttribute("Name", x.Manufacturer.Name!),
                new XAttribute("Country", x.Manufacturer.Country!),
                new XElement("Cars", x.Cars.Select(c =>
                    new XElement("Car",
                        new XAttribute("Model", c.Name!),
                        new XAttribute("Combined", c.Combined))),
                    new XAttribute("Country", x.Manufacturer.Country!),
                    new XAttribute("CombinedSum", x.Cars.Sum(s => s.Combined))
                    ))));

        var document = new XDocument(xml);
        document.Save("Resources/Files/specificManufacturersCars.xml");

        Console.WriteLine("\tINFO : Created specificManufacturersCars.xml file");
    }

    public void CreateCarsXmlFileFromDatabase()
    {
        var cars = _carRepository.GetAll().ToList();

        var xmlCars = new XElement("Cars", cars.Select(x =>
            new XElement("Car",
                new XAttribute("Year", x.Year),
                new XAttribute("Manufacturer", x.Manufacturer!),
                new XAttribute("Name", x.Name!),
                new XAttribute("Engine", x.Engine),
                new XAttribute("Cylinders", x.Cylinders),
                new XAttribute("City", x.City),
                new XAttribute("Highway", x.Highway),
                new XAttribute("Combined", x.Combined)
                )));

        var xml = new XDocument();
        xml.Add(xmlCars);
        xml.Save(@"Resources\Files\cars.xml");

        Console.WriteLine("\tINFO : Created cars.xml file");
    }

    public void CreateManufacturersXmlFileFromDatabase()
    {
        var manufacturers = _manufacturerRepository.GetAll().ToList();

        var xmlManufacturers = new XElement("Manufacturers", manufacturers.Select(x =>
            new XElement("Manufacturer",
                new XAttribute("Name", x.Name!),
                new XAttribute("Country", x.Country!),
                new XAttribute("Year", x.Year)
                )));

        var xml = new XDocument(xmlManufacturers);
        xml.Save(@"Resources\Files\manufacturers.xml");

        Console.WriteLine("\tINFO : Created manufacturers.xml file");
    }
}
