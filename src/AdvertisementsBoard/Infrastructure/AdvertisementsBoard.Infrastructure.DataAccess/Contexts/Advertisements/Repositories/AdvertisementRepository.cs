using System.Linq.Expressions;
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
    public async Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize)
    {
        var dto = await _repository.GetAllFiltered(a => true)
            .Where(s => s.IsActive)
            .OrderBy(a => a.Title)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ProjectTo<AdvertisementShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Advertisement>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateAsync(AdvertisementDto dto,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Advertisement>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        var updatedDto = _mapper.Map<AdvertisementUpdatedDto>(entity);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DoesAdvertisementExistWhereAsync(Expression<Func<Advertisement, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.FindAnyAsync(filter, cancellationToken);
        return exists;
    }

    /// <inheritdoc />
    public async Task<AdvertisementDto> GetWhereAsync(Expression<Func<Advertisement, bool>> filter,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .Include(s => s.SubCategory)
            .Include(s => s.User)
            .FirstOrDefaultAsync(cancellationToken);

        var dto = _mapper.Map<AdvertisementDto>(entity);

        return dto;
    }
}