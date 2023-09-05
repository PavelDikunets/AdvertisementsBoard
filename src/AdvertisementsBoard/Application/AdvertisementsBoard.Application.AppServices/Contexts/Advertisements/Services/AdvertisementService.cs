using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementService" />
    /// </summary>
    /// <param name="advertisementRepository">Репозиторий для работы с объявлениями. </param>
    public AdvertisementService(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    /// <inheritdoc />
    public Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertisementRepository.GetByIdAsync(id, cancellationToken);
    }
}