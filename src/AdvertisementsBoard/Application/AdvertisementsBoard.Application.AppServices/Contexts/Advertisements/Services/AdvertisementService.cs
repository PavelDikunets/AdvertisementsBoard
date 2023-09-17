using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
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
    public async Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _advertisementRepository.GetByIdAsync(id, cancellationToken);
        var model = new AdvertisementDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<List<AdvertisementDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        var entities = await _advertisementRepository.GetAllAsync(cancellationToken);
        var result = entities.Select(s => new AdvertisementDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            Price = s.Price,
            TagNames = s.TagNames
        });
        return result.ToList();
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
    public async Task<AdvertisementDto> UpdateAsync(UpdateAdvertisementDto dto, CancellationToken cancellationToken)
    {
        var advertisement = new AdvertisementDto
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            CategoryId = dto.CategoryId
        };

        return advertisement;
    }

    public async Task<string> DeleteByIdAsync(AdvertisementDto dto, CancellationToken cancellationToken)
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
        await _advertisementRepository.DeleteByIdAsync(advertisement, cancellationToken);
        return "Ok";
    }
}