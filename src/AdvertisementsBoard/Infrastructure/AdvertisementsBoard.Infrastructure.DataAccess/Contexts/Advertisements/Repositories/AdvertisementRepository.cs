using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
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
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().Where(s => s.Id == id).Select(a => new AdvertisementInfoDto
        {
            Title = a.Title,
            Description = a.Description,
            Price = a.Price,
            TagNames = a.TagNames,
            IsActive = a.IsActive,
            Attachments = a.Attachments.Select(f => new AttachmentInfoDto
            {
                FileName = f.FileName
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize)
    {
        var allAdvertisements = _repository.GetAllFiltered(a => true);

        return await allAdvertisements
            .OrderBy(a => a.Id)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> UpdateAsync(Advertisement currentEntity,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(currentEntity.Id, cancellationToken);

        entity.Title = currentEntity.Title;
        entity.Description = currentEntity.Description;
        entity.Price = currentEntity.Price;
        entity.IsActive = currentEntity.IsActive;
        entity.TagNames = currentEntity.TagNames;

        await _repository.UpdateAsync(entity, cancellationToken);

        var model = new AdvertisementInfoDto
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
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }
}