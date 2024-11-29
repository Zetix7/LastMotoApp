using LastMotoApp.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LastMotoApp.DataAccess.Data;

public class MotoAppDbContext : DbContext
{
    public MotoAppDbContext(DbContextOptions<MotoAppDbContext> options) : base(options)
    {
    }

    public DbSet<Car>? Cars { get; set; }
    public DbSet<Manufacturer>? Manufacturers { get; set; }
    public DbSet<Employee>? Employees { get; set; }
    public DbSet<BusinessPartner>? BusinessPartners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MotoAppStorage;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
    }
}
