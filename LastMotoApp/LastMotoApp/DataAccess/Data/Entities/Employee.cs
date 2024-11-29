namespace LastMotoApp.DataAccess.Data.Entities;

public class Employee : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public override string ToString() => $"Fullname: {FirstName} {LastName}";
}
