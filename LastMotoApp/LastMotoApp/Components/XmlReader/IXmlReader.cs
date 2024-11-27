using LastMotoApp.Components.CsvReader.Models;

namespace LastMotoApp.Components.XmlReader;

public interface IXmlReader
{
    void CreateCarsXmlFile(string fileName);
    void CreateManufacturersXmlFile(string fileName);
    List<Car> ReadCarsXmlFile(string fileName);
    List<Manufacturer> ReadManufacturersXmlFile(string fileName);
    void CreateSpecificManufacturersCarsXmlFile();
}
