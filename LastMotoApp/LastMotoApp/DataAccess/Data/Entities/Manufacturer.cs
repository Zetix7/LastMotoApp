namespace LastMotoApp.DataAccess.Data.Entities;

public class Manufacturer : EntityBase
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Country}, {Year}";
    }
}