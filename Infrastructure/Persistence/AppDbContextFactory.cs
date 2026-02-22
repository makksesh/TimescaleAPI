using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Строка подключения хардкодится для миграции
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=timescaledb;Username=postgres;Password=postgres");

        return new AppDbContext(optionsBuilder.Options);
    }
}
