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
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<Advertisement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAll().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    /// <inheritdoc />
    public async Task<Advertisement> UpdateAsync(Advertisement model, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(model, cancellationToken);
        return model;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Advertisement model, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(model, cancellationToken);
    }
}