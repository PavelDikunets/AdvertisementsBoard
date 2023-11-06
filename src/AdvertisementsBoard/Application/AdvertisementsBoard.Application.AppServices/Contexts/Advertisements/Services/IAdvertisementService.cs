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
    /// <returns>Модель с информацией об объявлении <see cref="AdvertisementInfoDto" />.</returns>
    Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <returns>Список объявлений с краткой информацией <see cref="AdvertisementShortInfoDto" />.</returns>
    Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель создания объявления <see cref="AdvertisementCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления.</returns>
    Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="dto">Модель обновления объявления <see cref="AdvertisementUpdateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным объявлением <see cref="AdvertisementUpdatedDto" />.</returns>
    Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, AdvertisementUpdateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверка объявления на существование по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task EnsureAdvertisementExistsByIdAsync(Guid id, CancellationToken cancellationToken);
}