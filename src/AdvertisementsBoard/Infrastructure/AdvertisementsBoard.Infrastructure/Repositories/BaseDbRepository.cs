using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.Repositories;

/// <summary>
///     Базовый репозиторий.
/// </summary>
public class BaseDbRepository<TEntity> : IBaseDbRepository<TEntity> where TEntity : class
{
    protected DbContext DbContext;
    protected DbSet<TEntity> DbSet;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="BaseDbRepository{TEntity}" />.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных.</param>
    public BaseDbRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }


    public IQueryable<TEntity> GetAll()
    {
        if (DbSet == null) throw new ArgumentNullException(nameof(DbSet));

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
        var result = await DbSet.FindAsync(id, cancellationToken);
        return result;
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

    public async Task<bool> FindAnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        return await DbSet.AnyAsync(filter, cancellationToken);
    }
}