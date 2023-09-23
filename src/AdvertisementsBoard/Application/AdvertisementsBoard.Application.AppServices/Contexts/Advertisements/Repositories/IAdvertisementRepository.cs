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
    public Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Массив моделей объявлений <see cref="AdvertisementShortInfoDto" />.</returns>
    public Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken, int pageNumber, int pageSize);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="entity">Сущность объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AdvertisementInfoDto> UpdateAsync(Advertisement entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}