using System.Linq.Expressions;

namespace AdvertisementsBoard.Infrastructure.DataAccess;

/// <summary>
///     Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Получить все элементы сущности <see cref="TEntity" />.
    /// </summary>
    /// <returns>Все элементы сущности <see cref="TEntity" /></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    ///     Получить все эелементы сущности <see cref="TEntity" /> по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <returns>Все эелементы сущности <see cref="TEntity" /> по фильтру.</returns>
    IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    ///     Получить элемент сущности <see cref="TEntity" /> по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавить экземпляр сущности <see cref="TEntity" />.
    /// </summary>
    /// <param name="model">Новый экземпляр сущности <see cref="TEntity" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Созданный экземпляр сущности <see cref="TEntity" />.</returns>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить элемент сущности <see cref="TEntity" />.
    /// </summary>
    /// <param name="model">Существующий экземпляр сущности <see cref="TEntity" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленный экземпляр сущности <see cref="TEntity" />.</returns>
    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить элемент сущности <see cref="TEntity" />.
    /// </summary>
    /// <param name="model">Существующая сущность <see cref="TEntity" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task DeleteAsync(TEntity model, CancellationToken cancellationToken);
}