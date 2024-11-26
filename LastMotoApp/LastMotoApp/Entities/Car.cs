namespace LastMotoApp.Entities;

public class Car : EntityBase
{
    public string? Name { get; set; }
    public string? Color { get; set; }
    public decimal Price { get; set; }
    public string? Type { get; set; }

    public override string ToString()
    {
        return $"\t#{Id} - Name: {Name}, Color: {Color}, Price: {Price:c}, Type: {Type}";
    }
}
