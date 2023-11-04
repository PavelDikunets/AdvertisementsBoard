using AdvertisementsBoard.Contracts.Categories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;

/// <summary>
///     Сервис для работы с категориями.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    ///     Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией о категории <see cref="CategoryInfoDto" />.</returns>
    Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий с кратким описанием <see cref="CategoryShortInfoDto" />.</returns>
    Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории <see cref="CategoryCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории.</returns>
    Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель обновления категории <see cref="CategoryUpdateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель обновленной категорией <see cref="CategoryUpdatedDto" />.</returns>
    Task<CategoryUpdatedDto> UpdateByIdAsync(Guid id, CategoryUpdateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверка категории на существование по идентификатору.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task EnsureCategoryExistsByIdAsync(Guid categoryId, CancellationToken cancellationToken);
}