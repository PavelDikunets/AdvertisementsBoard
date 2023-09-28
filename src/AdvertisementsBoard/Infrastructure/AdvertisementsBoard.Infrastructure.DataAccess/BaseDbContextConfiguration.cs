using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdvertisementsBoard.Infrastructure.DataAccess;

/// <summary>
///     Конфигурация контекста БД.
/// </summary>
public class BaseDbContextConfiguration : IDbContextOptionsConfigurator<BaseDbContext>
{
    private const string PostgesConnectionStringName = "PostgresDb";
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Инициализация экземпляра <see cref="BaseDbContextConfiguration" />
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    public BaseDbContextConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
    {
        var connectionString = _configuration.GetConnectionString("PostgresDb");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Строка подключения с именем {PostgesConnectionStringName} не найдена!");
        options.UseNpgsql(connectionString);
    }
}