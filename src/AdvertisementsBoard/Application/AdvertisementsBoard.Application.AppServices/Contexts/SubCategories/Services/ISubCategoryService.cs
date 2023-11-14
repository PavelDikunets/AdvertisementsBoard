using AdvertisementsBoard.Contracts.SubCategories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;

/// <summary>
///     Сервис для работы с подкатегориями.
/// </summary>
public interface ISubCategoryService
{
    /// <summary>
    ///     Получить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией о подкатегории.</returns>
    Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории в категории.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список подкатегорий с краткой информацией.</returns>
    Task<List<SubCategoryShortInfoDto>> GetAllAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="entity"></param>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    Task<SubCategoryShortInfoDto> CreateAsync(Guid categoryId, SubCategoryCreateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="categoryId">Идентификатор подкатегории.</param>
    /// <param name="editDto">Модель обновления подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной подкатегорией.</returns>
    Task<SubCategoryInfoDto> UpdateByIdAsync(Guid categoryId, SubCategoryEditDto editDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверка подкатегории на существование по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task DoesSubCategoryExistByIdAsync(Guid id, CancellationToken cancellationToken);
}