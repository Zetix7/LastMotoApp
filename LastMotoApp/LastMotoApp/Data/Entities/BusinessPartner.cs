namespace LastMotoApp.Data.Entities;

public class BusinessPartner : EntityBase
{
    public string? Name { get; set; }

    public override string ToString() => $"Name: {Name}";
}
