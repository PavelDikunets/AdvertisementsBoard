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
    /// <returns>Модель с информацией об объявлении.</returns>
    Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список объявлений пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией об объявлениях пользователя.</returns>
    Task<List<AdvertisementShortInfoDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <returns>Список объявлений с краткой информацией.</returns>
    Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель создания объявления.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления.</returns>
    Task<AdvertisementCreatedDto> CreateAsync(AdvertisementCreateDto dto, Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="userId"></param>
    /// <param name="dto">Модель обновления объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным объявлением.</returns>
    Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, Guid userId, AdvertisementEditDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить идентификатор пользователя из объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> GetUserIdAsync(Guid id, CancellationToken cancellationToken);
}