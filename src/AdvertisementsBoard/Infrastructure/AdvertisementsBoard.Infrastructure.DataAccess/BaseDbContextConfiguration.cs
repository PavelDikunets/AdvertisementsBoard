using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Infrastructure.DataAccess;

/// <summary>
///     Конфигурация контекста БД.
/// </summary>
public class BaseDbContextConfiguration : IDbContextOptionsConfigurator<BaseDbContext>
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    ///     Инициализация экземпляра <see cref="BaseDbContextConfiguration" />
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="loggerFactory">Фабрика логирования.</param>
    public BaseDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("PostgresDb");
        options.UseNpgsql(connectionString);
    }
}