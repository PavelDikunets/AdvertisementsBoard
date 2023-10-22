using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<Advertisement> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public AdvertisementRepository(IBaseDbRepository<Advertisement> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAll()
            .Where(a => a.Id == id)
            .Include(s => s.SubCategory)
            .ThenInclude(c => c.Category)
            .Include(a => a.Attachments)
            .Include(u => u.User)
            .ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize)
    {
        var dto = await _repository.GetAllFiltered(a => true)
            .Where(s => s.IsActive)
            .OrderBy(a => a.Title)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ProjectTo<AdvertisementShortInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateAsync(Advertisement updatedEntity,
        CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        var dto = _mapper.Map<AdvertisementUpdatedDto>(updatedEntity);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(a => a.Id == id, cancellationToken);
        return result;
    }
}