using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementService" />.
    /// </summary>
    /// <param name="advertisementRepository">Репозиторий для работы с объявлениями.</param>
    public AdvertisementService(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        var model = new AdvertisementInfoDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive,
            Attachments = entity.Attachments.Select(s => new AttachmentInfoDto
            {
                FileName = s.FileName
            }).ToList()
        };

        return model;
    }

    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        return await _advertisementRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = new Advertisement
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames
        };

        return await _advertisementRepository.CreateAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> UpdateAsync(ExistingAdvertisementUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = new Advertisement
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            IsActive = dto.IsActive
        };

        return await _advertisementRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _advertisementRepository.DeleteByIdAsync(id, cancellationToken);
    }
}