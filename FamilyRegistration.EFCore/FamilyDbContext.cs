using Microsoft.EntityFrameworkCore;

namespace FamilyRegistration.EFCore;

public class FamilyDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }

    public FamilyDbContext(DbContextOptions options) : base(options) { }

    public FamilyDbContext()
    {
        Database.SetConnectionString("Host=localhost;Username=postgres;Password=pgsql;Database=FamilyRegistration;Pooling=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.UseIdentityByDefaultColumns();
}
