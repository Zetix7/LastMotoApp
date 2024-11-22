namespace LastMotoApp.Entities;

public class Supervisor : Employee
{
    public override string ToString() => $"{base.ToString()} (SUPERVISOR)";
}
