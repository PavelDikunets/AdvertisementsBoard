using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly IBaseDbRepository<Advertisement> _repository;

    public AdvertisementRepository(IBaseDbRepository<Advertisement> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        var model = new AdvertisementDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll();

        var models = entities.Select(e => new AdvertisementShortInfoDto
        {
            Title = e.Title,
            Price = e.Price
        });

        return await models.ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementDto> UpdateAsync(Advertisement currentEntity,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(currentEntity.Id, cancellationToken);
        entity.Title = currentEntity.Title;
        entity.Description = currentEntity.Description;
        entity.Price = currentEntity.Price;
        entity.TagNames = currentEntity.TagNames;
        entity.IsActive = currentEntity.IsActive;

        await _repository.UpdateAsync(entity, cancellationToken);

        var model = new AdvertisementDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}