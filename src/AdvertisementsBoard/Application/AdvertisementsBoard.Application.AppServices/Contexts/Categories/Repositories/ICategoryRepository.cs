using System.Linq.Expressions;
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
    /// <returns>Модель категории <see cref="CategoryDto" />.</returns>
    Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий с краткой информацией <see cref="CategoryShortInfoDto" />.</returns>
    Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории <see cref="CategoryCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной категории.</returns>
    Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию.
    /// </summary>
    /// <param name="dto">Модель категории <see cref="CategoryDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной категорией <see cref="CategoryUpdatedDto" />.</returns>
    Task<CategoryUpdatedDto> UpdateAsync(CategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли категория с указанным фильтром.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если категория существует, и false в противном случае.</returns>
    Task<bool> DoesCategoryExistWhereAsync(Expression<Func<Category, bool>> filter,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить категорию по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель категории <see cref="CategoryDto" />.</returns>
    Task<CategoryDto> GetWhereAsync(Expression<Func<Category, bool>> filter, CancellationToken cancellationToken);
}