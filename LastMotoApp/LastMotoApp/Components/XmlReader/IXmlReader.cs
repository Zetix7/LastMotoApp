using LastMotoApp.Components.CsvReader.Models;

namespace LastMotoApp.Components.XmlReader;

public interface IXmlReader
{
    void CreateCarsXmlFileFromCsvFile(string fileName);
    void CreateManufacturersXmlFileFromCsvFile(string fileName);
    List<Car> ReadCarsXmlFile(string fileName);
    List<Manufacturer> ReadManufacturersXmlFile(string fileName);
    void CreateSpecificManufacturersCarsXmlFile();
    void CreateCarsXmlFileFromDatabase();
    void CreateManufacturersXmlFileFromDatabase();
}
