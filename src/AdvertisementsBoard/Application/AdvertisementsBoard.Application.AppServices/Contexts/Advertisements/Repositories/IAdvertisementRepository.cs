using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий для работы с объявлениями.
/// </summary>
public interface IAdvertisementRepository
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
    /// <param name="model">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> CreateAsync(Advertisement model, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="model">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AdvertisementDto> UpdateAsync(Advertisement model, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AdvertisementDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}