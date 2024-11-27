using LastMotoApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.Data;

public class MotoAppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();
    public DbSet<Car> Cars => Set<Car>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("MotoAppStorrage");
    }
}
