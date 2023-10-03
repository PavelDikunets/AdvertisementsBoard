using AdvertisementsBoard.Contracts.Categories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;

/// <summary>
///     Сервис работы с объявлениями.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    ///     Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель категории <see cref="CategoryInfoDto" />.</returns>
    Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив категорий <see cref="CategoryInfoDto" />.</returns>
    Task<CategoryDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории <see cref="CategoryCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории <see cref="Guid" />.</returns>
    Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель обновления категории. <see cref="CategoryUpdateDto" /></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель категории <see cref="CategoryInfoDto" />.</returns>
    Task<CategoryUpdateDto> UpdateByIdAsync(Guid id, CategoryUpdateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}