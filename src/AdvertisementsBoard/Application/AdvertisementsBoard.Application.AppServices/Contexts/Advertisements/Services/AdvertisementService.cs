using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;

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

    /// <inheritdoc />
    public Task<AdvertisementDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
    {
        return _advertisementRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(CreateAdvertisementDto dto, CancellationToken cancellationToken)
    {
        var advertisement = new Advertisement
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            CategoryId = dto.CategoryId
        };

        return _advertisementRepository.CreateAsync(advertisement, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AdvertisementDto> UpdateAsync(UpdateAdvertisementDto dto, CancellationToken cancellationToken)
    {
        var advertisement = new Advertisement
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            CategoryId = dto.CategoryId
        };

        return _advertisementRepository.UpdateAsync(advertisement, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AdvertisementDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertisementRepository.DeleteByIdAsync(id, cancellationToken);
    }
}