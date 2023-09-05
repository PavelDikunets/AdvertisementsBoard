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
}