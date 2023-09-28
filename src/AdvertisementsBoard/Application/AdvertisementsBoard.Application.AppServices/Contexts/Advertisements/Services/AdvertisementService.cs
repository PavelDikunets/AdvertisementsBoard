using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
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
        var entity = await GetAdvertisementFromRepositoryByIdAsync(id, cancellationToken);

        var dto = new AdvertisementInfoDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive,
            Attachments = entity.Attachments.Select(s => new AttachmentInfoDto
            {
                Url = s.Url
            }).ToList()
        };

        return dto;
    }

    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        var advertisements = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);
        var dto = advertisements.Select(a => new AdvertisementShortInfoDto
        {
            Id = a.Id,
            Title = a.Title,
            Price = a.Price
        }).ToArray();

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        var advertisement = new Advertisement
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            CategoryId = dto.CategoryId
        };

        var result = await _advertisementRepository.CreateAsync(entity, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> UpdateAsync(AdvertisementUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await GetAdvertisementFromRepositoryByIdAsync(dto.Id, cancellationToken);

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.TagNames = dto.TagNames;
        entity.IsActive = dto.IsActive;

        await _advertisementRepository.UpdateAsync(entity, cancellationToken);

        var updateddDto = new AdvertisementInfoDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive,
            Attachments = entity.Attachments.Select(a => new AttachmentInfoDto
            {
                Url = a.Url
            }).ToList()
        };
        return updateddDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetAdvertisementFromRepositoryByIdAsync(id, cancellationToken);
        await _advertisementRepository.DeleteByIdAsync(entity.Id, cancellationToken);
    }


    private async Task<Advertisement> GetAdvertisementFromRepositoryByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var entity = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Объявление с идентификатором {id} не найдено.");

        return entity;
    }
}