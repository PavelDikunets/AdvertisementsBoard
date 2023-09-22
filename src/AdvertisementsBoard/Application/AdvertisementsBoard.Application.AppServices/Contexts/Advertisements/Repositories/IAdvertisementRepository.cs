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
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" /></returns>
    Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив моделей объявлений <see cref="AdvertisementShortInfoDto" />.</returns>
    public Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

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
    /// <returns>Возвращает true, если удаление произошло успешно, false - в противном случае.</returns>
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}