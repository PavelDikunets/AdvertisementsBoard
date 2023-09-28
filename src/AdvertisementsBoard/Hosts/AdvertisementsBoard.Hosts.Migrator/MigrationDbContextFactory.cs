using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AdvertisementsBoard.Hosts.Migrator;

/// <summary>
///     Фабрика контекста БД для мигратора.
/// </summary>
public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
{
    /// <inheritdoc />
    public MigrationDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("PostgresDb");
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString);
        return new MigrationDbContext(dbContextOptionsBuilder.Options);
    }
}