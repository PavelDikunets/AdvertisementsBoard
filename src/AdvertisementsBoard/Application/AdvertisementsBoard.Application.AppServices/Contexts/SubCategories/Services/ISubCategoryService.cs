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
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив моделей подкатегорий с краткой информацией.</returns>
    Task<SubCategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="dto">Модель создания подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    Task<Guid> CreateAsync(SubCategoryCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="updateDto">Модель обновления подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной подкатегорией.</returns>
    Task<SubCategoryUpdateDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto updateDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли подкатегория с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если подкатегория существует, и false в противном случае.</returns>
    Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken);
}