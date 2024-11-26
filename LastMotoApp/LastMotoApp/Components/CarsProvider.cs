using LastMotoApp.Entities;
using LastMotoApp.Repositories;
using System.Text;

namespace LastMotoApp.Components;

public class CarsProvider : ICarsProvider
{
    private readonly IRepository<Car> _carRepository;

    public CarsProvider(IRepository<Car> carRepository)
    {
        _carRepository = carRepository;
    }

    public string AnonymousClass()
    {
        var cars = _carRepository.GetAll();
        var list = cars.Select(x => new
        {
            Name = x.Name,
            Price = x.Price
        });

        var sb = new StringBuilder(2048);
        foreach (var car in list)
        {
            sb.Append($"\t\tName: {car.Name,-10}- price: {car.Price:c}\n");
        }

        return sb.ToString();
    }

    public decimal CheapestPrice()
    {
        var cars = _carRepository.GetAll();
        return cars.Select(x => x.Price).Min();
    }

    public List<Car[]> ChunkCars(int size)
    {
        var cars = _carRepository.GetAll();
        return cars.Chunk(size).ToList();
    }

    public List<string> DistinctAllColors()
    {
        var cars = _carRepository.GetAll();
        return cars.Select(x => x.Color).Distinct().ToList()!;
    }

    public List<Car> DistinctByColors()
    {
        var cars = _carRepository.GetAll();
        return cars.DistinctBy(x => x.Color).ToList();
    }

    public Car? FirstByColor(string color)
    {
        var cars = _carRepository.GetAll();
        return cars.First(x => x.Color == color);
    }

    public Car? FirstOrDefaultByColor(string color)
    {
        var cars = _carRepository.GetAll();
        return cars.FirstOrDefault(x => x.Color == color);
    }

    public Car? FirstOrDefaultByColorWithDefault(string color)
    {
        var cars = _carRepository.GetAll();
        return cars.FirstOrDefault(x => x.Color == color, new Car { Id = -1, Name = "NOT FOUND" });
    }

    public List<Car> GetSpecificColumns()
    {
        var cars = _carRepository.GetAll();
        var list = cars.Select(x => new Car
        {
            Name = x.Name,
            Price = x.Price
        }).ToList();

        return list;
    }

    public Car? LastByColor(string color)
    {
        var cars = _carRepository.GetAll();
        return cars.Last(x => x.Color == color);
    }

    public List<Car> OrderByColorAndName()
    {
        var cars = _carRepository.GetAll();
        return cars.OrderBy(x => x.Name).ToList();
    }

    public List<Car> OrderByColorAndNameDescending()
    {
        var cars = _carRepository.GetAll();
        return cars.OrderByDescending(x => x.Name).ToList();
    }

    public List<Car> OrderByName()
    {
        var cars = _carRepository.GetAll();
        return cars
            .OrderBy(x => x.Color)
            .ThenBy(x => x.Name)
            .ToList();
    }

    public List<Car> OrderByNameDescending()
    {
        var cars = _carRepository.GetAll();
        return cars
            .OrderByDescending(x => x.Color)
            .ThenByDescending(x => x.Name)
            .ToList();
    }

    public List<Car> PriceGreaterThan(decimal price)
    {
        var cars = _carRepository.GetAll();
        return cars.Where(x => x.Price > price).ToList();
    }

    public Car? SingleById(int id)
    {
        var cars = _carRepository.GetAll();
        return cars.Single(x => x.Id == id);
    }

    public Car? SingleOrDefaultById(int id)
    {
        var cars = _carRepository.GetAll();
        return cars.SingleOrDefault(x => x.Id == id);
    }

    public List<Car> SkipCars(int howMany)
    {
        var cars = _carRepository.GetAll();
        return cars.Skip(howMany).ToList();
    }

    public List<Car> SkipCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carRepository.GetAll();
        return cars.SkipWhile(x => x.Name!.StartsWith(prefix)).ToList();
    }

    public List<Car> TakeCars(int howMany)
    {
        var cars = _carRepository.GetAll();
        return cars.Take(howMany).ToList();
    }

    public List<Car> TakeCars(Range range)
    {
        var cars = _carRepository.GetAll();
        return cars.Take(range).ToList();
    }

    public List<Car> TakeCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carRepository.GetAll();
        return cars.TakeWhile(x => x.Name!.StartsWith(prefix)).ToList();
    }

    public List<string> UniqueColors()
    {
        var cars = _carRepository.GetAll();
        return cars.Select(c => c.Color).Distinct().ToList()!;
    }

    public List<Car> WhereColorIs(string color)
    {
        var cars = _carRepository.GetAll();
        return cars.Where(x => x.Color == color).ToList();
    }

    public List<Car> WhereStartsWith(string prefix)
    {
        var cars = _carRepository.GetAll();
        return cars.Where(x => x.Name!.StartsWith(prefix)).ToList();
    }

    public List<Car> WhereStartsWithAndPriceIsGreaterThan(string prefix, decimal price)
    {
        var cars = _carRepository.GetAll();
        return cars.Where(x => x.Name!.StartsWith(prefix) && x.Price > price).ToList();
    }
}
