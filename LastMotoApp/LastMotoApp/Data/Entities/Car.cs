namespace LastMotoApp.Data.Entities;

public class Car : EntityBase
{
    public int Year { get; set; }
    public string? Manufacturer { get; set; }
    public string? Name { get; set; }
    public double Engine { get; set; }
    public int Cylinders { get; set; }
    public int City { get; set; }
    public int Highway { get; set; }
    public int Combined { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Year}, {Manufacturer}, {Name}, {Engine}, {Cylinders}, {City}, {Highway}, {Combined}";
    }
}
