using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;
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
        var advertisement = await TryGetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AdvertisementDto>(advertisement);

        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize)
    {
        var listAdvertisements = await _repository.GetAllFiltered(a => true)
            .Where(s => s.IsActive)
            .OrderBy(a => a.Title)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ProjectTo<AdvertisementShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listAdvertisements;
    }

    public async Task<List<AdvertisementShortInfoDto>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var listAdvertisements = await _repository.GetAllFiltered(a => true)
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.Title)
            .ProjectTo<AdvertisementShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listAdvertisements;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementDto dto, CancellationToken cancellationToken)
    {
        var advertisement = _mapper.Map<Advertisement>(dto);

        await _repository.AddAsync(advertisement, cancellationToken);
        return advertisement.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateAsync(AdvertisementDto dto,
        CancellationToken cancellationToken)
    {
        var advertisement = _mapper.Map<Advertisement>(dto);

        await _repository.UpdateAsync(advertisement, cancellationToken);

        var updatedDto = _mapper.Map<AdvertisementUpdatedDto>(advertisement);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advert = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(advert, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AdvertisementDto> FindWhereAsync(Expression<Func<Advertisement, bool>> filter,
        CancellationToken cancellationToken)
    {
        var advertisement = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .Include(s => s.SubCategory)
            .Include(s => s.User)
            .ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (advertisement == null) throw new AdvertisementNotFoundException();

        return advertisement;
    }


    private async Task<Advertisement> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _repository.GetByIdAsync(id, cancellationToken);

        if (advertisement == null) throw new AdvertisementNotFoundException(id);

        return advertisement;
    }
}