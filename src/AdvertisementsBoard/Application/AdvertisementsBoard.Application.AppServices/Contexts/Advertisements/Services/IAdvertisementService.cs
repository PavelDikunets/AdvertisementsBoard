using AdvertisementsBoard.Contracts.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <summary>
///     Сервис работы с объявлениями.
/// </summary>
public interface IAdvertisementService
{
    /// <summary>
    ///     Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" />.</returns>
    Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <returns>Массив объявлений <see cref="AdvertisementShortInfoDto" />.</returns>
    Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="subCategoryId">Идентификатор подкатегории.</param>
    /// <param name="dto">Модель создания объявления <see cref="AdvertisementCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления <see cref="Guid" />.</returns>
    Task<Guid> CreateAsync(Guid categoryId, Guid subCategoryId, AdvertisementCreateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" />.</returns>
    Task<AdvertisementUpdateDto> UpdateByIdAsync(Guid id, AdvertisementUpdateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}