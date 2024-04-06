using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;
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

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий.</param>
    public AdvertisementRepository(IBaseDbRepository<Advertisement> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await TryGetByIdAsync(id, cancellationToken);
        return advertisement;
    }

    /// <inheritdoc />
    public async Task<List<Advertisement>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize)
    {
        var listOfAdvertisements = await _repository.FindWhereAsync(a => a.IsActive)
            .OrderBy(a => a.Title)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return listOfAdvertisements;
    }

    public async Task<List<Advertisement>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var listOfAdvertisements = await _repository.FindWhereAsync(a => a.UserId == userId)
            .OrderBy(a => a.Title)
            .ToListAsync(cancellationToken);

        return listOfAdvertisements;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<Advertisement> UpdateAsync(Advertisement entity,
        CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(advertisement, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Advertisement> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _repository.FindWhereAsync(a => a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (advertisement == null) throw new AdvertisementNotFoundException(id);

        return advertisement;
    }


    private async Task<Advertisement> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _repository.GetByIdAsync(id, cancellationToken);

        if (advertisement == null) throw new AdvertisementNotFoundException(id);
        return advertisement;
    }
}