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
    /// <returns>Модель с информацией о категории.</returns>
    Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий с кратким описанием.</returns>
    Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории.</returns>
    Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="editDto">Модель обновления категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель обновленной категорией.</returns>
    Task<CategoryInfoDto> UpdateByIdAsync(Guid id, CategoryEditDto editDto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}