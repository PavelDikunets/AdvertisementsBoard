using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess;

/// <summary>
///     Контекст базы данных.
/// </summary>
public class BaseDbContext : DbContext
{
    /// <summary>
    ///     Инициализирует экземпляр <see cref="BaseDbContext" />
    /// </summary>
    /// <param name="options">Настройки контекста.</param>
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}