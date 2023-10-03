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
    /// <returns>Модель подкатегории <see cref="SubCategoryDto" />.</returns>
    Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив подкатегорий <see cref="SubCategoryInfoDto" />.</returns>
    Task<SubCategoryDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="dto">Модель создания подкатегории <see cref="SubCategoryCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории <see cref="Guid" />.</returns>
    Task<Guid> CreateByCategoryIdAsync(Guid categoryId, SubCategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="dto">Модель обновления подкатегории. <see cref="SubCategoryUpdateDto" /></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель обновления подкатегории <see cref="SubCategoryUpdateDto" />.</returns>
    Task<SubCategoryUpdateDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}