using LastMotoApp.Entities;

namespace LastMotoApp.Components;

public interface ICarsProvider
{
    List<Car> PriceGreaterThan(decimal price);
    List<string> UniqueColors();
    decimal CheapestPrice();
    List<Car> GetSpecificColumns();
    string AnonymousClass();
    List<Car> OrderByName();
    List<Car> OrderByNameDescending();
    List<Car> OrderByColorAndName();
    List<Car> OrderByColorAndNameDescending();
    List<Car> WhereStartsWith(string prefix);
    List<Car> WhereStartsWithAndPriceIsGreaterThan(string prefix, decimal price);
    List<Car> WhereColorIs(string color);
    Car? FirstByColor(string color);
    Car? FirstOrDefaultByColor(string color);
    Car? FirstOrDefaultByColorWithDefault(string color);
    Car? LastByColor(string color);
    Car? SingleById(int id);
    Car? SingleOrDefaultById(int id);
    List<Car> TakeCars(int howMany);
    List<Car> TakeCars(Range range);
    List<Car> TakeCarsWhileNameStartsWith(string prefix);
    List<Car> SkipCars(int howMany);
    List<Car> SkipCarsWhileNameStartsWith(string prefix);
    List<string> DistinctAllColors();
    List<Car> DistinctByColors();
    List<Car[]> ChunkCars(int size);
}
