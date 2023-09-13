using AdvertisementsBoard.Infrastructure.DataAccess;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Hosts.Migrator;

/// <summary>
///     Контекст базы данных для мигратора.
/// </summary>
public class MigrationDbContext : BaseDbContext
{
    public MigrationDbContext(DbContextOptions options) : base(options)
    {
    }
}