using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
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
    public async Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAll()
            .Where(e => e.Id == id)
            .Include(a => a.Attachments)
            .Include(c => c.Category)
            .FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken,
        int pageNumber,
        int pageSize)
    {
        var allAdvertisements = _repository.GetAllFiltered(a => true);

        return await allAdvertisements
            .OrderBy(a => a.Id)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(c => c.Category)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Advertisement updatedEntity,
        CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }
}