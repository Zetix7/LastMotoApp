using LastMotoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.Data;

public class MotoAppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("MotoAppStorrage");
    }
}
