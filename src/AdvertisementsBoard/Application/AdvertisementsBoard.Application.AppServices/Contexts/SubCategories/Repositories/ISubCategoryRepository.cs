using System.Linq.Expressions;
using AdvertisementsBoard.Domain.SubCategories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;

/// <summary>
///     Репозиторий для работы с подкатегориями.
/// </summary>
public interface ISubCategoryRepository
{
    /// <summary>
    ///     Получить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории.</returns>
    Task<SubCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории в категории.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список подкатегорий в указанной категории.</returns>
    Task<List<SubCategory>> GetAllAsync(Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="entity">Сущность подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    Task<Guid> CreateAsync(SubCategory entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="entity">Сущность подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность подкатегории.</returns>
    Task<SubCategory> UpdateAsync(SubCategory entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли подкатегория с указанным фильтром.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если подкатегория существует, и false в противном случае.</returns>
    Task<bool> DoesSubCategoryExistWhereAsync(Expression<Func<SubCategory, bool>> filter,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить подкатегорию по фильтру.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность подкатегории.</returns>
    Task<SubCategory> FindWhereAsync(Expression<Func<SubCategory, bool>> filter, CancellationToken cancellationToken);
}