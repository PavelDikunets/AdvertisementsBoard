using AdvertisementsBoard.Domain.Categories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;

/// <summary>
///     Репозиторий для работы с категориями.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    ///     Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность категории <see cref="Category" />.</returns>
    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Перечеслитель сущностей категорий <see cref="Category" />.</returns>
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="entity">Сущность категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории <see cref="Guid" />.</returns>
    Task<Guid> CreateAsync(Category entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(Category updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить категорию на существование по имени.
    /// </summary>
    /// <param name="name">Наименование категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken);
}