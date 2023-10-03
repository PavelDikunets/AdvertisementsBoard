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
    /// <returns>Сущность подкатегории <see cref="SubCategory" />.</returns>
    Task<SubCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Перечеслитель сущностей подкатегорий <see cref="SubCategory" />.</returns>
    Task<IEnumerable<SubCategory>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="entity">Сущность подкатегории <see cref="SubCategory" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории <see cref="Guid" />.</returns>
    Task<Guid> CreateAsync(SubCategory entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(SubCategory updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить подкатегорию на существование по имени.
    /// </summary>
    /// <param name="name">Наименование подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken);
}