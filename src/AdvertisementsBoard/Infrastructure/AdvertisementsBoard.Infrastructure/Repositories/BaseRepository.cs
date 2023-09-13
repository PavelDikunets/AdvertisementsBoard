using System.Linq.Expressions;
using AdvertisementsBoard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.Repositories;

/// <summary>
///     Базовый репозиторий.
/// </summary>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Инициализирует экземпляр <see cref="BaseRepository{TEntity}" />.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных.</param>
    /// <param name="dbSet">Таблица базы данных.</param>
    public BaseRepository(DbContext dbContext, DbSet<TEntity> dbSet)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return DbSet.Where(predicate);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        DbSet.Remove(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}