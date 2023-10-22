using AdvertisementsBoard.Contracts.Categories;
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
    /// <returns>Модель категории.</returns>
    Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив моделей категорий с кратким описанием.</returns>
    Task<CategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="entity">Сущность категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории.</returns>
    Task<Guid> CreateAsync(Category entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной категорией.</returns>
    Task<CategoryUpdateDto> UpdateAsync(Category updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли категория с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если категория существует, и false в противном случае.</returns>
    Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли категория с указанным именем.
    /// </summary>
    /// <param name="name">Наименование категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если наименование категории существует, и false в противном случае.</returns>
    Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken);
}