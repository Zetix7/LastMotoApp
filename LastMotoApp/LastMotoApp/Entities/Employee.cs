namespace LastMotoApp.Entities;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public override string ToString() => $"Id: {Id}, Fullname: {FirstName} {LastName}";
}
