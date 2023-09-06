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
    /// <returns>Модель объявления <see cref="AdvertisementDto" /></returns>
    Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция объявлений <see cref="AdvertisementDto" /></returns>
    public Task<AdvertisementDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор <see cref="AdvertisementDto" /></returns>
    public Task<Guid> CreateAsync(CreateAdvertisementDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AdvertisementDto> UpdateAsync(UpdateAdvertisementDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AdvertisementDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}