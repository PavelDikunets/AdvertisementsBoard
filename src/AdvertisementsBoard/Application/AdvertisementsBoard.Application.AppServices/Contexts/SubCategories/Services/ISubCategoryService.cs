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
    /// <returns>Модель с информацией о подкатегории <see cref="SubCategoryInfoDto" />.</returns>
    Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список подкатегорий с краткой информацией <see cref="SubCategoryShortInfoDto" />.</returns>
    Task<List<SubCategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="dto">Модель создания подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    Task<Guid> CreateAsync(SubCategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="dto">Модель обновления подкатегории <see cref="SubCategoryUpdateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной подкатегорией <see cref="SubCategoryUpdatedDto" />.</returns>
    Task<SubCategoryUpdatedDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto dto,
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
    public Task EnsureSubCategoryExistsByIdAsync(Guid id, CancellationToken cancellationToken);
}