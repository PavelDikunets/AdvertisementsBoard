using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts;

/// <summary>
///     Конфигурация контекста БД.
/// </summary>
public class BaseDbContextConfiguration : IDbContextOptionsConfigurator<BaseDbContext>
{
    private const string PostqresConnectionStringName = "PostqresDb";
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Инициализация экземпляра <see cref="BaseDbContextConfiguration" />
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="loggerFactory">Фабрика логирования.</param>
    public BaseDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        var configuration = builder.Build();
        var connectionString = _configuration.GetConnectionString(PostqresConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Строка с именем \"{PostqresConnectionStringName}\" не найдена");

        options.UseNpgsql(connectionString);
    }
}